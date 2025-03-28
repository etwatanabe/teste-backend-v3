using TheatricalPlayersRefactoringKata.UseCases.Plays.Dtos;
using Valhalla.Lib.Result;
using Valhalla.Lib.SharedKernel;

namespace TheatricalPlayersRefactoringKata.UseCases.Plays.Queries.GetAllPlay;

public record GetAllPlayQuery() : IQuery<Result<ListPlayDTO>>;
