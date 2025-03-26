using TheatricalPlayersRefactoringKata.Core.PlayAggregate;

namespace TheatricalPlayersRefactoringKata.UseCases.Plays.Dtos;

public record PlayDTO(string Name, int Lines, PlayType Type);