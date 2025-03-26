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
    public IEnumerable<Performance> Performances => _performances.AsReadOnly();

    public void AddPerformance(Performance performance)
    {
        _performances.Add(performance);
    }
}

public class Performance
{
    public Performance(Guid playID, int audience)
    {
        PlayID = Guard.Against.Null(playID, nameof(playID), "Null ID.");
        Audience = Guard.Against.Negative(audience, nameof(audience), "Negative audience.");
    }

    public Guid PlayID { get; init; }
    public int Audience { get; init; }
}