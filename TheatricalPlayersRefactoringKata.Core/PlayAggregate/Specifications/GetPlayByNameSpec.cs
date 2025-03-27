using Valhalla.Lib.Specification;

namespace TheatricalPlayersRefactoringKata.Core.PlayAggregate.Specifications;

public class GetPlayByNameSpec : Specification<Play>
{
    public GetPlayByNameSpec(string name)
    {
        Query
            .Where(p => p.Name == name);   
    }
}