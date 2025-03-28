using System;
using TheatricalPlayersRefactoringKata.Core.InvoiceAggregate;
using Xunit;

namespace TheatricalPlayersRefactoringKata.Tests;

public class InvoiceTests
{
    [Fact]
    public void Constructor_ShouldInitializeProperties()
    {
        // Arrange
        var customer = "John Doe";

        // Act
        var invoice = new Invoice(customer);

        // Assert
        Assert.Equal(customer, invoice.Customer);
        Assert.Equal(0m, invoice.TotalAmountOwed);
        Assert.Equal(0, invoice.TotalEarnedCredits);
        Assert.Empty(invoice.Performances);
    }

    [Fact]
    public void AddPerformance_ShouldAddPerformance_WhenPerformanceDoesNotExist()
    {
        // Arrange
        var invoice = new Invoice("John Doe");
        var performance = new Performance("Hamlet", 55);

        // Act
        invoice.AddPerformance(performance);

        // Assert
        Assert.Contains(performance, invoice.Performances);
    }

    [Fact]
    public void Summarize_ShouldCalculateTotalAmountOwedAndTotalEarnedCredits()
    {
        // Arrange
        var invoice = new Invoice("John Doe");
        var performance1 = new Performance("Hamlet", 55);
        var performance2 = new Performance("Othello", 35);
        performance1.SetAmountOwed(500m);
        performance1.SetEarnedCredits(10);
        performance2.SetAmountOwed(300m);
        performance2.SetEarnedCredits(5);
        invoice.AddPerformance(performance1);
        invoice.AddPerformance(performance2);

        // Act
        invoice.Summarize();

        // Assert
        Assert.Equal(800m, invoice.TotalAmountOwed);
        Assert.Equal(15, invoice.TotalEarnedCredits);
    }

    [Fact]
    public void Summarize_ShouldThrowException_WhenNoPerformances()
    {
        // Arrange
        var invoice = new Invoice("John Doe");

        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() => invoice.Summarize());
        Assert.Contains("It is necessary to have recorded performance", ex.Message);
    }

    [Fact]
    public void Summarize_ShouldThrowException_WhenInvoiceAlreadySummarized()
    {
        // Arrange
        var invoice = new Invoice("John Doe");
        var performance = new Performance("Hamlet", 55);
        performance.SetAmountOwed(500m);
        performance.SetEarnedCredits(10);
        invoice.AddPerformance(performance);
        invoice.Summarize();

        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() => invoice.Summarize());
        Assert.Contains("Invoice already summarized", ex.Message);
    }
}
