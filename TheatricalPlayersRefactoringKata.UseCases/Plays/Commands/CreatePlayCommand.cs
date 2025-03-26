using TheatricalPlayersRefactoringKata.UseCases.Plays.Dtos;
using TheatricalPlayersRefactoringKata.Core.PlayAggregate;
using Valhalla.Lib.Result;
using Valhalla.Lib.SharedKernel;

namespace TheatricalPlayersRefactoringKata.UseCases.Plays.Commands;

public record CreatePlayCommand(string Name, int Lines, PlayType Type) : ICommand<Result<PlayDTO>>;
