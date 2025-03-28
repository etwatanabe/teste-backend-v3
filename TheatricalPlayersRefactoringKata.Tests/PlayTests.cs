using System;
using System.ComponentModel;
using TheatricalPlayersRefactoringKata.Core.PlayAggregate;
using Xunit;

namespace TheatricalPlayersRefactoringKata.Tests;

public class PlayTests
{
    [Fact]
    public void Constructor_ShouldInitializeProperties()
    {
        // Arrange
        var name = "Hamlet";
        var lines = 1500;
        var type = PlayType.Tragedy;

        // Act
        var play = new Play(name, lines, type);

        // Assert
        Assert.Equal(name, play.Name);
        Assert.Equal(lines, play.Lines);
        Assert.Equal(type, play.Type);
    }

    [Fact]
    public void Constructor_ShouldThrowException_WhenNameIsNullOrWhiteSpace()
    {
        // Arrange
        var lines = 1500;
        var type = PlayType.Tragedy;

        // Act & Assert
        var ex = Assert.Throws<ArgumentNullException>(() => new Play(null, lines, type));
        Assert.Contains("Nome vazio. (Parameter 'name')", ex.Message);

        var ex2 = Assert.Throws<ArgumentException>(() => new Play("", lines, type));
        Assert.Contains("Nome vazio. (Parameter 'name')", ex2.Message);

        var ex3 = Assert.Throws<ArgumentException>(() => new Play(" ", lines, type));
        Assert.Contains("Nome vazio. (Parameter 'name')", ex3.Message);
    }

    [Fact]
    public void Constructor_ShouldThrowException_WhenLinesAreNegativeOrZero()
    {
        // Arrange
        var name = "Hamlet";
        var type = PlayType.Tragedy;

        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() => new Play(name, -1, type));
        Assert.Equal("Linhas menor ou igual a zero. (Parameter 'lines')", ex.Message);

        ex = Assert.Throws<ArgumentException>(() => new Play(name, 0, type));
        Assert.Equal("Linhas menor ou igual a zero. (Parameter 'lines')", ex.Message);
    }

    [Fact]
    public void Constructor_ShouldThrowException_WhenTypeIsInvalid()
    {
        // Arrange
        var name = "Hamlet";
        var lines = 1500;

        // Act & Assert
        var ex = Assert.Throws<InvalidEnumArgumentException>(() => new Play(name, lines, (PlayType)999));
        Assert.Contains("Tipo inválido.", ex.Message);
    }
}
