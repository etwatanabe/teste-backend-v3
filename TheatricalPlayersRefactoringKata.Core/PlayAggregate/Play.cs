using Valhalla.Lib.GuardClauses;
using Valhalla.Lib.SharedKernel;

namespace TheatricalPlayersRefactoringKata.Core.PlayAggregate;

public class Play : EntityBase, IAggregateRoot
{
    public Play(string name, int lines, PlayType type)
    {
        Name = Guard.Against.NullOrWhiteSpace(name, nameof(name), "Nome vazio.");
        Lines = Guard.Against.NegativeOrZero(lines, nameof(lines), "Linhas menor ou igual a zero.");
        Type = Guard.Against.EnumOutOfRange(type, nameof(type), "Tipo inválido.");
    }

    public string Name { get; private set; }
    public int Lines { get; private set; }
    public PlayType Type { get; private set; }
}

public enum PlayType
{
    Tragedy,
    Comedy,
    History
}
