using TheatricalPlayersRefactoringKata.UseCases.Plays.Dtos;
using Valhalla.Lib.Result;
using Valhalla.Lib.SharedKernel;

namespace TheatricalPlayersRefactoringKata.UseCases.Plays.Queries;

public record GetPlayByNameQuery(string Name) : IQuery<Result<PlayDTO>>;
