using TheatricalPlayersRefactoringKata.Core.PlayAggregate;
using TheatricalPlayersRefactoringKata.UseCases.Plays.Dtos;

namespace TheatricalPlayersRefactoringKata.UseCases.Plays.Extensions;

public static class PlayExtensions
{
    public static PlayDTO ParseDTO(this Play play) 
        => new(play.Name, play.Lines, play.Type);
}