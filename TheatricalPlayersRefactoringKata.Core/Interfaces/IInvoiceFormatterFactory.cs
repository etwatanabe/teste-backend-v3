namespace TheatricalPlayersRefactoringKata.Core.Interfaces
{
    public interface IInvoiceFormatterFactory
    {
        IInvoiceFormatter GetFormatter(string formatType);
    }
}