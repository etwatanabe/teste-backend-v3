using TheatricalPlayersRefactoringKata.Core.InvoiceAggregate.Events.Domain;
using Valhalla.Lib.GuardClauses;
using Valhalla.Lib.SharedKernel;

namespace TheatricalPlayersRefactoringKata.Core.InvoiceAggregate;

public class Invoice : EntityBase, IAggregateRoot
{
    protected readonly List<Performance> _performances = [];

    public Invoice(string customer)
    {
        Customer = Guard.Against.NullOrWhiteSpace(customer, nameof(customer), "Invalid customer.");
    }

    public string Customer { get; private set; }
    public decimal TotalAmountOwed { get; private set; } = 0m;
    public int TotalEarnedCredits { get; private set; } = 0;
    public IEnumerable<Performance> Performances => _performances.AsReadOnly();

    public void AddPerformance(Performance performance)
    {
        Guard.Against.Null(performance, nameof(performance), "Performance cannot be null.");

        _performances.Add(performance);

        //var existingPerformance = _performances.FirstOrDefault(x => x.PlayName == performance.PlayName);
        //if (existingPerformance is null)
        //{
        //  _performances.Add(performance);
        //}
        //else
        //{
        //    Guard.Against.InvalidInput(TotalEarnedCredits, nameof(TotalEarnedCredits), x => x == 0, "Invoice already summarized");
        //    Guard.Against.InvalidInput(TotalAmountOwed, nameof(TotalAmountOwed), x => x == 0m, "Invoice already summarized");
        //
        //    existingPerformance.SetAudience(performance.Audience);
        //    //existingPerformance.SetAmountOwed(performance.AmountOwed);
        //    //existingPerformance.SetEarnedCredits(performance.EarnedCredits);
        //}
    }

    public void Summarize()
    {
        Guard.Against.InvalidInput(Performances, nameof(Performances), x => x.Any(), "It is necessary to have recorded performance");
        Guard.Against.InvalidInput(TotalEarnedCredits, nameof(TotalEarnedCredits), x => x == 0, "Invoice already summarized");
        Guard.Against.InvalidInput(TotalAmountOwed, nameof(TotalAmountOwed), x => x == 0m, "Invoice already summarized");

        TotalAmountOwed = Performances.Sum(x => x.AmountOwed);
        TotalEarnedCredits = Performances.Sum(x => x.EarnedCredits);

        RegisterDomainEvent(new SummarizeInvoiceEvent(this));
    }
}

public class Performance(string playName, int audience = 0)
{
    public string PlayName { get; init; } = Guard.Against.NullOrWhiteSpace(playName, nameof(playName), "Play name null or white space.");
    public int Audience { get; private set; } = Guard.Against.Negative(audience, nameof(audience), "Audience cannot be negative");
    public decimal AmountOwed { get; private set; } = 0m;
    public int EarnedCredits { get; private set; } = 0;

    public void SetAudience(int audience)
        => Audience = Guard.Against.Negative(audience, nameof(audience), "Audience cannot be negative");

    public void SetAmountOwed(decimal amount)
        => AmountOwed = Guard.Against.Negative(amount, nameof(amount), "Amount owed cannot be negative");

    public void SetEarnedCredits(int credits)
         => EarnedCredits = Guard.Against.Negative(credits, nameof(credits), "Earned credits cannot be negative");
}
