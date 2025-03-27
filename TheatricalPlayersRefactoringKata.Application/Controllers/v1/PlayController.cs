using Microsoft.AspNetCore.Mvc;
using TheatricalPlayersRefactoringKata.UseCases.Plays.Commands;
using TheatricalPlayersRefactoringKata.UseCases.Plays.Dtos;
using Valhalla.Lib.Result;
using MediatR;
using TheatricalPlayersRefactoringKata.UseCases.Plays.Queries;

namespace TheatricalPlayersRefactoringKata.Application.Controllers.v1;

[ApiController]
[Route("api/v1/play")]
public class PlayController : ControllerBase
{
    private readonly ILogger<PlayController> _logger;
    private readonly IMediator _mediator;

    public PlayController(ILogger<PlayController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [HttpPost]
    [ProducesResponseType(typeof(Result<PlayDTO>), 201)]
    [ProducesResponseType(typeof(Result), 400)]
    public async Task<Result<PlayDTO>> PlayCreate([FromBody] CreatePlayCommand command, CancellationToken cancellationToken)
    {
        return await _mediator.Send(command, cancellationToken);
    }

    [HttpGet]
    [ProducesResponseType(typeof(Result<PlayDTO>), 200)]
    [ProducesResponseType(typeof(Result), 404)]
    public async Task<Result<PlayDTO>> Play([FromQuery] GetPlayByNameQuery command, CancellationToken cancellationToken)
    {
        return await _mediator.Send(command, cancellationToken);
    }
}
