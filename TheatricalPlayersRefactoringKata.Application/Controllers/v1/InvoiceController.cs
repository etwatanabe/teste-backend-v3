using MediatR;
using Microsoft.AspNetCore.Mvc;
using TheatricalPlayersRefactoringKata.UseCases.Invoices.Commands;
using TheatricalPlayersRefactoringKata.UseCases.Invoices.Dtos;
using TheatricalPlayersRefactoringKata.UseCases.Invoices.Queries;
using Valhalla.Lib.Result;

namespace TheatricalPlayersRefactoringKata.Application.Controllers.v1;

[ApiController]
[Route("api/v1/invoice")]
public class InvoiceController : ControllerBase
{
    private readonly ILogger<InvoiceController> _logger;
    private readonly IMediator _mediator;

    public InvoiceController(ILogger<InvoiceController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [HttpPost]
    [ProducesResponseType(typeof(Result<InvoiceDTO>), 201)]
    [ProducesResponseType(typeof(Result), 400)]
    public async Task<Result<InvoiceDTO>> InvoiceCreate([FromBody] CreateInvoiceCommand command, CancellationToken cancellationToken)
    {
        return await _mediator.Send(command, cancellationToken);
    }

    [HttpGet]
    [ProducesResponseType(typeof(Result<InvoiceDTO>), 200)]
    [ProducesResponseType(typeof(Result), 404)]
    public async Task<Result<InvoiceDTO>> Invoice([FromQuery] GetInvoiceByCustomerQuery command, CancellationToken cancellationToken)
    {
        return await _mediator.Send(command, cancellationToken);
    }
}
