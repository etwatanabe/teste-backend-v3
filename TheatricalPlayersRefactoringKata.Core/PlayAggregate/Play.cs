namespace TheatricalPlayersRefactoringKata.Core.PlayAggregate;

public class Play
{
    protected readonly List<Performance> performances = [];

    public Play(string name, int lines, PlayType type)
    {
        Name = name;
        Lines = lines;
        Type = type;
    }

    public string Name { get; private set; }
    public int Lines { get; private set; }
    public PlayType Type { get; private set; }
    public IEnumerable<Performance> Performance => performances.AsReadOnly();
}

public class Performance
{
    public int Audience { get; private set; }
}

public enum PlayType
{
    Tragedy,
    Comedy,
    History
}
