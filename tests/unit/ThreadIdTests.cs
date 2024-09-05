using ALSI.Serilog.Enrichers.Environment;
using FluentAssertions;
using NSubstitute;
using Serilog.Core;
using Serilog.Events;

namespace ALSI.Serilog.Enrichers.UnitTests;

public class ThreadIdTests
{
    [Fact]
    public void WhenThreadIdChanges_ThenPropertyReturnedChanges()
    {
        // Arrange
        var mockPropertyFactory = Substitute.For<ILogEventPropertyFactory>();
        var threadIdEnricher = new ThreadId(mockPropertyFactory);

        // Act
        var originalProperty = threadIdEnricher.GetUpdated(mockPropertyFactory);
        CachedProperty? outputProperty = null;

        var newThread = new Thread(SetOutput);
        newThread.Start();
        newThread.Join();

        // Assert
        outputProperty.Should().NotBeNull();
        outputProperty.Should().NotBe(originalProperty);
        return;

        void SetOutput()
        {
            outputProperty = threadIdEnricher.GetUpdated(mockPropertyFactory);
        }
    }

    [Fact]
    public void WhenThreadIdUnchanged_ShouldReturnSameProperty()
    {
        // Arrange
        var mockPropertyFactory = Substitute.For<ILogEventPropertyFactory>();
        var threadIdEnricher = new ThreadId(mockPropertyFactory);

        // Act
        var originalProperty = threadIdEnricher.GetUpdated(mockPropertyFactory);
        var outputProperty = threadIdEnricher.GetUpdated(mockPropertyFactory);

        // Assert
        outputProperty.Should().Be(originalProperty);
    }
}
