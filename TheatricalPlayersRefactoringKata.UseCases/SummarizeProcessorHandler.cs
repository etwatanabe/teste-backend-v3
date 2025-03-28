using System.Text;
using TheatricalPlayersRefactoringKata.Core.Interfaces;
using TheatricalPlayersRefactoringKata.Core.InvoiceAggregate.Events.Domain;

namespace TheatricalPlayersRefactoringKata.UseCases;

public class SummarizeProcessorHandler : IEventHandler<SummarizeInvoiceEvent>
{
    private readonly IInvoiceFormatterFactory _invoiceFormatterFactory;

    public SummarizeProcessorHandler(IInvoiceFormatterFactory invoiceFormatterFactory)
    {
        _invoiceFormatterFactory = invoiceFormatterFactory;
    }

    public async Task HandleAsync(SummarizeInvoiceEvent message, CancellationToken cancellationToken)
    {
        var formatter = _invoiceFormatterFactory.GetFormatter("xml");
        var result = formatter.Format(message.Invoice);

        var folder = Path.Combine("extract");
        Directory.CreateDirectory(folder);
        var filename = $"invoice-{message.Invoice.Customer}-{DateTime.Now:yyyy-MM-dd-HH-mm-ss}.xml";
        var path = Path.Combine(folder, filename);

        await File.WriteAllTextAsync(path, result, Encoding.UTF8, cancellationToken);
    }
}
