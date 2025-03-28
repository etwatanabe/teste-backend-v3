using TheatricalPlayersRefactoringKata.Core.PlayAggregate;

namespace TheatricalPlayersRefactoringKata.UseCases.Plays.Dtos;

public record PlayDTO(Guid Id, string Name, PlayType Type);
