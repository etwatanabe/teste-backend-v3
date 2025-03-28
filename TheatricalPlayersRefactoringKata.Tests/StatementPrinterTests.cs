using ApprovalTests;
using ApprovalTests.Reporters;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System.Collections.Generic;
using System.Linq;
using TheatricalPlayersRefactoringKata.Core.Interfaces;
using TheatricalPlayersRefactoringKata.Core.InvoiceAggregate;
using TheatricalPlayersRefactoringKata.Core.PlayAggregate;
using TheatricalPlayersRefactoringKata.Core.Services;
using TheatricalPlayersRefactoringKata.Infrastructure.Formatter;
using Valhalla.Lib.SharedKernel;
using Xunit;

namespace TheatricalPlayersRefactoringKata.Tests;

public class StatementPrinterTests
{
    private readonly Mock<IRepository<Invoice>> _invoiceRepositoryMock;
    private readonly Mock<IReadRepository<Play>> _playRepositoryMock;
    private readonly InvoiceService _invoiceService;
    private readonly ServiceProvider _serviceProvider;

    public StatementPrinterTests()
    {
        _invoiceRepositoryMock = new Mock<IRepository<Invoice>>();
        _playRepositoryMock = new Mock<IReadRepository<Play>>();
        _invoiceService = new InvoiceService(_invoiceRepositoryMock.Object, _playRepositoryMock.Object);
        _serviceProvider = new ServiceCollection()
                .AddScoped<TextInvoiceFormatter>()
                .AddScoped<XmlInvoiceFormatter>()
                .AddScoped<IInvoiceFormatterFactory, InvoiceFormatterFactory>()
                .BuildServiceProvider();
    }

    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void TestStatementExampleLegacy()
    {
        // Arrange
        var plays = new List<Play>
        {
            { new Play("Hamlet", 4024, PlayType.Tragedy) },
            { new Play("As You Like It", 2670, PlayType.Comedy) },
            { new Play("Othello", 3560, PlayType.Tragedy) }
        };

        var invoice = new Invoice("BigCo");
        invoice.AddPerformance(new Performance("Hamlet", 55));
        invoice.AddPerformance(new Performance("As You Like It", 35));
        invoice.AddPerformance(new Performance("Othello", 40));

        foreach (var performance in invoice.Performances)
        {
            var play = plays.First(x => x.Name == performance.PlayName);
            performance.SetAmountOwed(_invoiceService.CalculateAmount(play, performance));
            performance.SetEarnedCredits(_invoiceService.CalculateCredits(play, performance));
        }

        invoice.Summarize();

        var factory = _serviceProvider.GetRequiredService<IInvoiceFormatterFactory>();
        var formatter = factory.GetFormatter("text");

        // Act
        var result = formatter.Format(invoice);

        // Assert
        Approvals.Verify(result);
    }


    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void TestTextStatementExample()
    {
        // Arrange
        var plays = new List<Play>
            {
                { new Play("Hamlet", 4024, PlayType.Tragedy) },
                { new Play("As You Like It", 2670, PlayType.Comedy) },
                { new Play("Othello", 3560, PlayType.Tragedy) },
                { new Play("Henry V", 3227, PlayType.History) },
                { new Play("King John", 2648, PlayType.History) },
                { new Play("Richard III", 3718, PlayType.History) }
            };

        var invoice = new Invoice("BigCo");
        invoice.AddPerformance(new Performance("Hamlet", 55));
        invoice.AddPerformance(new Performance("As You Like It", 35));
        invoice.AddPerformance(new Performance("Othello", 40));
        invoice.AddPerformance(new Performance("Henry V", 20));
        invoice.AddPerformance(new Performance("King John", 39));
        invoice.AddPerformance(new Performance("Henry V", 20));

        foreach (var performance in invoice.Performances)
        {
            var play = plays.First(x => x.Name == performance.PlayName);
            performance.SetAmountOwed(_invoiceService.CalculateAmount(play, performance));
            performance.SetEarnedCredits(_invoiceService.CalculateCredits(play, performance));
        }

        invoice.Summarize();

        var serviceProvider = new ServiceCollection()
                .AddScoped<TextInvoiceFormatter>()
                .AddScoped<XmlInvoiceFormatter>()
                .AddScoped<IInvoiceFormatterFactory, InvoiceFormatterFactory>()
                .BuildServiceProvider();

        var factory = serviceProvider.GetRequiredService<IInvoiceFormatterFactory>();
        var formatter = factory.GetFormatter("text");

        // Act
        var result = formatter.Format(invoice);


        // Assert
        Approvals.Verify(result);
    }

    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void TestXmlStatementExample()
    {
        // Arrange
        var plays = new List<Play>
            {
                { new Play("Hamlet", 4024, PlayType.Tragedy) },
                { new Play("As You Like It", 2670, PlayType.Comedy) },
                { new Play("Othello", 3560, PlayType.Tragedy) },
                { new Play("Henry V", 3227, PlayType.History) },
                { new Play("King John", 2648, PlayType.History) },
                { new Play("Richard III", 3718, PlayType.History) }
            };

        var invoice = new Invoice("BigCo");
        invoice.AddPerformance(new Performance("Hamlet", 55));
        invoice.AddPerformance(new Performance("As You Like It", 35));
        invoice.AddPerformance(new Performance("Othello", 40));
        invoice.AddPerformance(new Performance("Henry V", 20));
        invoice.AddPerformance(new Performance("King John", 39));
        invoice.AddPerformance(new Performance("Henry V", 20));

        foreach (var performance in invoice.Performances)
        {
            var play = plays.First(x => x.Name == performance.PlayName);
            performance.SetAmountOwed(_invoiceService.CalculateAmount(play, performance));
            performance.SetEarnedCredits(_invoiceService.CalculateCredits(play, performance));
        }

        invoice.Summarize();

        var serviceProvider = new ServiceCollection()
                .AddScoped<TextInvoiceFormatter>()
                .AddScoped<XmlInvoiceFormatter>()
                .AddScoped<IInvoiceFormatterFactory, InvoiceFormatterFactory>()
                .BuildServiceProvider();

        var factory = serviceProvider.GetRequiredService<IInvoiceFormatterFactory>();
        var formatter = factory.GetFormatter("xml");

        // Act
        var result = formatter.Format(invoice);


        // Assert
        Approvals.Verify(result);
    }

}
