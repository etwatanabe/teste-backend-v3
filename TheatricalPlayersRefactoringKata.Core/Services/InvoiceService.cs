using TheatricalPlayersRefactoringKata.Core.Interfaces;
using TheatricalPlayersRefactoringKata.Core.InvoiceAggregate;
using TheatricalPlayersRefactoringKata.Core.InvoiceAggregate.Specifications;
using TheatricalPlayersRefactoringKata.Core.PlayAggregate;
using TheatricalPlayersRefactoringKata.Core.PlayAggregate.Specifications;
using Valhalla.Lib.Result;
using Valhalla.Lib.SharedKernel;

namespace TheatricalPlayersRefactoringKata.Core.Services;

public class InvoiceService : IInvoiceService
{
    private readonly IRepository<Invoice> _invoiceRepository;
    private readonly IReadRepository<Play> _playRepository;

    public InvoiceService(IRepository<Invoice> invoiceRepository, IReadRepository<Play> playRepository)
    {
        _invoiceRepository = invoiceRepository;
        _playRepository = playRepository;
    }

    public async Task<Result<Invoice>> AddPerformanceAsync(string customer, Performance performance, CancellationToken cancellationToken)
    {
        var invoice = await _invoiceRepository.FirstOrDefaultAsync(new GetInvoiceByCustomerSpec(customer), cancellationToken);
        if (invoice is null)
            return Result.NotFound("Customer not found");

        var play = await _playRepository.FirstOrDefaultAsync(new GetPlayByNameSpec(performance.PlayName), cancellationToken);
        if (play is null)
            return Result.NotFound("Play not found");

        invoice.AddPerformance(performance);

        await _invoiceRepository.UpdateAsync(invoice);
        await _invoiceRepository.SaveChangesAsync(cancellationToken);

        return Result.Success(invoice);
    }

    public async Task<Result<Invoice>> SummarizeAsync(string customer, CancellationToken cancellationToken)
    {
        var invoice = await _invoiceRepository.FirstOrDefaultAsync(new GetInvoiceByCustomerSpec(customer), cancellationToken);
        if (invoice is null)
            return Result.NotFound("Customer not found");

        foreach (var performance in invoice.Performances)
        {
            var play = await _playRepository.FirstOrDefaultAsync(new GetPlayByNameSpec(performance.PlayName), cancellationToken);
            if (play is null)
                return Result.NotFound("Play not found");

            performance.SetAmountOwed(CalculateAmount(play, performance));
            performance.SetEarnedCredits(CalculateCredits(play, performance));
        }

        invoice.Summarize();

        //await _invoiceRepository.UpdateAsync(invoice);
        await _invoiceRepository.SaveChangesAsync(cancellationToken);

        return Result.Success(invoice);
    }

    public int CalculateCredits(Play play, Performance performance)
    {
        var audience = performance.Audience;
        var baseCredits = Math.Max(audience - 30, 0);
        var bonus = play.Type == PlayType.Comedy ? audience / 5 : 0;

        return baseCredits + bonus;
    }

    public decimal CalculateAmount(Play play, Performance performance)
    {
        var audience = performance.Audience;
        var lines = Math.Clamp(play.Lines, 1000, 4000);
        var baseAmount = lines / 10m;

        return play.Type switch
        {
            PlayType.Tragedy => CalculateTragedy(baseAmount, audience),
            PlayType.Comedy => CalculateComedy(baseAmount, audience),
            PlayType.History => CalculateHistory(baseAmount, audience),
            _ => throw new ArgumentOutOfRangeException()
        };

        decimal CalculateTragedy(decimal baseAmount, int audience)
        {
            if (audience <= 30)
                return baseAmount;

            return baseAmount + (10m * (audience - 30));
        }
        decimal CalculateComedy(decimal baseAmount, int audience)
        {
            var amount = baseAmount + (3m * audience);
            if (audience > 20)
                amount += 100m + (5m * (audience - 20));

            return amount;
        }
        decimal CalculateHistory(decimal baseAmount, int audience)
        {
            var tragedy = CalculateTragedy(baseAmount, audience);
            var comedy = CalculateComedy(baseAmount, audience);
            return tragedy + comedy;
        }
    }
}
