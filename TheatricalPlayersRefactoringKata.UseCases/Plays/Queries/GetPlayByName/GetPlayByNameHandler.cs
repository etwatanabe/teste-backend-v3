using TheatricalPlayersRefactoringKata.Core.PlayAggregate;
using TheatricalPlayersRefactoringKata.Core.PlayAggregate.Specifications;
using TheatricalPlayersRefactoringKata.UseCases.Plays.Dtos;
using TheatricalPlayersRefactoringKata.UseCases.Plays.Extensions;
using Valhalla.Lib.Result;
using Valhalla.Lib.SharedKernel;

namespace TheatricalPlayersRefactoringKata.UseCases.Plays.Queries.GetPlayByName;

public class GetPlayByNameHandler : IQueryHandler<GetPlayByNameQuery, Result<PlayDTO>>
{
    private readonly IReadRepository<Play> _playRepository;

    public GetPlayByNameHandler(IReadRepository<Play> playRepository)
    {
        _playRepository = playRepository;
    }

    public async Task<Result<PlayDTO>> Handle(GetPlayByNameQuery request, CancellationToken cancellationToken)
    {
        var play = await _playRepository.FirstOrDefaultAsync(new GetPlayByNameSpec(request.Name), cancellationToken);
        if (play is null)
            return Result.NotFound();

        return Result.Success(play.ParseDTO());
    }
}
