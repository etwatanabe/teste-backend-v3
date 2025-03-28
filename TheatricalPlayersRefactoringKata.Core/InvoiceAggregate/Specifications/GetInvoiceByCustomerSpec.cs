using Valhalla.Lib.Specification;

namespace TheatricalPlayersRefactoringKata.Core.InvoiceAggregate.Specifications;

public class GetInvoiceByCustomerSpec : Specification<Invoice>
{
    public GetInvoiceByCustomerSpec(string customer)
    {
        Query
            .Where(p => p.Customer == customer)
            .Include(p => p.Performances);
    }
}