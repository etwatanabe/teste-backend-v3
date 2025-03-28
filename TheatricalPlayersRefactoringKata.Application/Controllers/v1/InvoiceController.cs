using MediatR;
using Microsoft.AspNetCore.Mvc;
using TheatricalPlayersRefactoringKata.UseCases.Invoices.Commands.AddPerformanceInvoice;
using TheatricalPlayersRefactoringKata.UseCases.Invoices.Commands.CreateInvoice;
using TheatricalPlayersRefactoringKata.UseCases.Invoices.Commands.SummarizeInvoice;
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

    /// <summary>
    /// Creates a new invoice.
    /// </summary>
    /// <param name="command">The command containing the details of the invoice to create.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The created invoice.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(Result<InvoiceDTO>), 201)]
    [ProducesResponseType(typeof(Result), 400)]
    public async Task<Result<InvoiceDTO>> InvoiceCreate([FromBody] CreateInvoiceCommand command, CancellationToken cancellationToken)
        => await _mediator.Send(command, cancellationToken);

    /// <summary>
    /// Gets an invoice by customer.
    /// </summary>
    /// <param name="command">The query containing the customer details.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The invoice for the specified customer.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(Result<InvoiceDTO>), 200)]
    [ProducesResponseType(typeof(Result), 404)]
    public async Task<Result<InvoiceDTO>> Invoice([FromQuery] GetInvoiceByCustomerQuery command, CancellationToken cancellationToken)
        => await _mediator.Send(command, cancellationToken);

    /// <summary>
    /// Adds a performance to an existing invoice.
    /// </summary>
    /// <param name="command">The command containing the performance details to add.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The updated invoice.</returns>
    [HttpPatch]
    [ProducesResponseType(typeof(Result<InvoiceDTO>), 200)]
    [ProducesResponseType(typeof(Result), 404)]
    public async Task<Result<InvoiceDTO>> AddPerformance([FromBody] AddPerformanceInvoiceCommand command, CancellationToken cancellationToken)
        => await _mediator.Send(command, cancellationToken);

    /// <summary>
    /// Summarizes an invoice.
    /// </summary>
    /// <param name="command">The command containing the details of the invoice to summarize.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The summarized invoice.</returns>
    [HttpPost("summarize")]
    [ProducesResponseType(typeof(Result<InvoiceDTO>), 200)]
    [ProducesResponseType(typeof(Result), 404)]
    public async Task<Result<InvoiceDTO>> Summarize([FromBody] SummarizeInvoiceCommand command, CancellationToken cancellationToken)
        => await _mediator.Send(command, cancellationToken);
}
