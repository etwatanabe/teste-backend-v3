using TheatricalPlayersRefactoringKata.Core.PlayAggregate;
using TheatricalPlayersRefactoringKata.UseCases.Plays.Dtos;

namespace TheatricalPlayersRefactoringKata.UseCases.Plays.Extensions;

public static class PlayExtensions
{
    public static PlayDTO ParseDTO(this Play play)
        => new(play.Id, play.Name, play.Type);

    public static ListPlayDTO ParseDTO(this List<Play> plays)
      => new(plays.Select(play => play.ParseDTO()).ToList());
}