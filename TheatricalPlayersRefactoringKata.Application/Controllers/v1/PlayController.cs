using MediatR;
using Microsoft.AspNetCore.Mvc;
using TheatricalPlayersRefactoringKata.UseCases.Plays.Commands;
using TheatricalPlayersRefactoringKata.UseCases.Plays.Dtos;
using TheatricalPlayersRefactoringKata.UseCases.Plays.Queries.GetAllPlay;
using TheatricalPlayersRefactoringKata.UseCases.Plays.Queries.GetPlayByName;
using Valhalla.Lib.Result;

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

    /// <summary>
    /// Creates a new play.
    /// </summary>
    /// <param name="command">The command containing the details of the play to create.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The created play.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(Result<PlayDTO>), 201)]
    [ProducesResponseType(typeof(Result), 400)]
    [ProducesResponseType(typeof(Result), 500)]
    public async Task<Result<PlayDTO>> PlayCreate([FromBody] CreatePlayCommand command, CancellationToken cancellationToken)
        => await _mediator.Send(command, cancellationToken);

    /// <summary>
    /// Gets a play by name.
    /// </summary>
    /// <param name="name">The name of the play to retrieve.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The play with the specified name.</returns>
    [HttpGet("{name}")]
    [ProducesResponseType(typeof(Result<PlayDTO>), 200)]
    [ProducesResponseType(typeof(Result), 404)]
    [ProducesResponseType(typeof(Result), 500)]
    public async Task<Result<PlayDTO>> Play([FromRoute] string name, CancellationToken cancellationToken)
        => await _mediator.Send(new GetPlayByNameQuery(name), cancellationToken);

    /// <summary>
    /// Gets all plays.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A list of all plays.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(Result<ListPlayDTO>), 200)]
    [ProducesResponseType(typeof(Result), 404)]
    [ProducesResponseType(typeof(Result), 500)]
    public async Task<Result<ListPlayDTO>> Plays(CancellationToken cancellationToken)
        => await _mediator.Send(new GetAllPlayQuery(), cancellationToken);
}
