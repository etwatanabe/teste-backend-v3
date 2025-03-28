using TheatricalPlayersRefactoringKata.Core.InvoiceAggregate;

namespace TheatricalPlayersRefactoringKata.Core.Interfaces;

public interface IInvoiceFormatter
{
    string Format(Invoice invoice);
}
