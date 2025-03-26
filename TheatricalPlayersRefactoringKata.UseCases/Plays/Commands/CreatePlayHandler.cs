using Valhalla.Lib.Result;
using Valhalla.Lib.SharedKernel;

namespace TheatricalPlayersRefactoringKata.UseCases.Plays.Commands;

public class CreatePlayHandler() : ICommandHandler<CreatePlayCommand, Result>
{
    public Task<Result> Handle(CreatePlayCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}