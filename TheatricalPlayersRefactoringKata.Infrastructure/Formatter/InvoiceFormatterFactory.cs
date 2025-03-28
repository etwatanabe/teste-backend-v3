using Microsoft.Extensions.DependencyInjection;
using TheatricalPlayersRefactoringKata.Core.Interfaces;

namespace TheatricalPlayersRefactoringKata.Infrastructure.Formatter;

public class InvoiceFormatterFactory : IInvoiceFormatterFactory
{
    private readonly IServiceProvider _serviceProvider;

    public InvoiceFormatterFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public IInvoiceFormatter GetFormatter(string formatType)
    {
        return formatType.ToLower() switch
        {
            "text" => _serviceProvider.GetRequiredService<TextInvoiceFormatter>(),
            "xml" => _serviceProvider.GetRequiredService<XmlInvoiceFormatter>(),
            _ => throw new ArgumentException("Invalid format type", nameof(formatType))
        };
    }
}
