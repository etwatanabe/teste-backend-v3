using TheatricalPlayersRefactoringKata.Core.InvoiceAggregate;

namespace TheatricalPlayersRefactoringKata.UseCases.Invoices.Dtos;

public record InvoiceDTO(string Customer, IEnumerable<Performance> Performances);
