using Moq;
using System.Threading;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Core.InvoiceAggregate;
using TheatricalPlayersRefactoringKata.Core.PlayAggregate;
using TheatricalPlayersRefactoringKata.Core.Services;
using Valhalla.Lib.SharedKernel;
using Valhalla.Lib.Specification;
using Xunit;

namespace TheatricalPlayersRefactoringKata.Tests;

public class InvoiceServiceTests
{
    private readonly Mock<IRepository<Invoice>> _invoiceRepositoryMock;
    private readonly Mock<IReadRepository<Play>> _playRepositoryMock;
    private readonly InvoiceService _invoiceService;

    public InvoiceServiceTests()
    {
        _invoiceRepositoryMock = new Mock<IRepository<Invoice>>();
        _playRepositoryMock = new Mock<IReadRepository<Play>>();
        _invoiceService = new InvoiceService(_invoiceRepositoryMock.Object, _playRepositoryMock.Object);
    }

    [Fact]
    public async Task AddOrUpdatePerformanceAsync_ShouldReturnNotFound_WhenCustomerDoesNotExist()
    {
        // Arrange
        var customer = "NonExistentCustomer";
        var performance = new Performance("Hamlet", 55);
        _invoiceRepositoryMock.Setup(repo => repo.FirstOrDefaultAsync(It.IsAny<ISpecification<Invoice>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Invoice)null);

        // Act
        var result = await _invoiceService.AddPerformanceAsync(customer, performance, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Contains("Customer not found", result.Errors);
    }

    [Fact]
    public async Task AddOrUpdatePerformanceAsync_ShouldReturnNotFound_WhenPlayDoesNotExist()
    {
        // Arrange
        var customer = "ExistingCustomer";
        var performance = new Performance("NonExistentPlay", 55);
        var invoice = new Invoice(customer);
        _invoiceRepositoryMock.Setup(repo => repo.FirstOrDefaultAsync(It.IsAny<ISpecification<Invoice>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(invoice);
        _playRepositoryMock.Setup(repo => repo.FirstOrDefaultAsync(It.IsAny<ISpecification<Play>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Play)null);

        // Act
        var result = await _invoiceService.AddPerformanceAsync(customer, performance, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Contains("Play not found", result.Errors);
    }

    [Fact]
    public async Task AddOrUpdatePerformanceAsync_ShouldAddPerformance_WhenCustomerAndPlayExist()
    {
        // Arrange
        var customer = "ExistingCustomer";
        var performance = new Performance("Hamlet", 55);
        var invoice = new Invoice(customer);
        var play = new Play("Hamlet", 1500, PlayType.Tragedy);
        _invoiceRepositoryMock.Setup(repo => repo.FirstOrDefaultAsync(It.IsAny<ISpecification<Invoice>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(invoice);
        _playRepositoryMock.Setup(repo => repo.FirstOrDefaultAsync(It.IsAny<ISpecification<Play>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(play);

        _invoiceRepositoryMock.Setup(repo => repo.UpdateAsync(invoice, It.IsAny<CancellationToken>())).Returns(Task.FromResult(invoice));
        _invoiceRepositoryMock.Setup(repo => repo.SaveChangesAsync(It.IsAny<CancellationToken>())).Returns(Task.FromResult(0));

        // Act
        var result = await _invoiceService.AddPerformanceAsync(customer, performance, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Contains(performance, invoice.Performances);
    }

    [Fact]
    public async Task SummarizeAsync_ShouldReturnNotFound_WhenCustomerDoesNotExist()
    {
        // Arrange
        var customer = "NonExistentCustomer";
        _invoiceRepositoryMock.Setup(repo => repo.FirstOrDefaultAsync(It.IsAny<ISpecification<Invoice>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Invoice)null);

        // Act
        var result = await _invoiceService.SummarizeAsync(customer, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Contains("Customer not found", result.Errors);
    }

    [Fact]
    public async Task SummarizeAsync_ShouldReturnNotFound_WhenPlayDoesNotExist()
    {
        // Arrange
        var customer = "ExistingCustomer";
        var performance = new Performance("NonExistentPlay", 55);
        var invoice = new Invoice(customer);
        invoice.AddPerformance(performance);
        _invoiceRepositoryMock.Setup(repo => repo.FirstOrDefaultAsync(It.IsAny<ISpecification<Invoice>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(invoice);
        _playRepositoryMock.Setup(repo => repo.FirstOrDefaultAsync(It.IsAny<ISpecification<Play>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Play)null);

        // Act
        var result = await _invoiceService.SummarizeAsync(customer, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Contains("Play not found", result.Errors);
    }

    [Fact]
    public async Task SummarizeAsync_ShouldSummarizeInvoice_WhenCustomerAndPlayExist()
    {
        // Arrange
        var customer = "ExistingCustomer";
        var performance = new Performance("Hamlet", 55);
        var invoice = new Invoice(customer);
        var play = new Play("Hamlet", 1500, PlayType.Tragedy);
        invoice.AddPerformance(performance);
        _invoiceRepositoryMock.Setup(repo => repo.FirstOrDefaultAsync(It.IsAny<ISpecification<Invoice>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(invoice);
        _playRepositoryMock.Setup(repo => repo.FirstOrDefaultAsync(It.IsAny<ISpecification<Play>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(play);
        _invoiceRepositoryMock.Setup(repo => repo.SaveChangesAsync(It.IsAny<CancellationToken>())).Returns(Task.FromResult(0));

        // Act
        var result = await _invoiceService.SummarizeAsync(customer, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(invoice, result.Value);
    }
}
