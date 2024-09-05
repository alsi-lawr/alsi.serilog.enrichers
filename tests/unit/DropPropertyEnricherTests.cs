using System.Collections;
using FluentAssertions;
using Serilog.Events;

namespace ALSI.Serilog.Enrichers.UnitTests;

public class DropPropertyEnricherTests
{
    [Fact]
    public void WhenPropertyExists_ThenDropsProperty()
    {
        // Arrange
        var enricher = new DropPropertyEnricher("my_property");
        var myProperty = new LogEventProperty("my_property", new ScalarValue(42));
        var logEvent = new LogEvent(
            DateTimeOffset.Now,
            LogEventLevel.Fatal,
            null,
            MessageTemplate.Empty,
            new List<LogEventProperty>() { myProperty }
        );

        // Act
        enricher.Enrich(logEvent, new LogEventPropertyFactory());

        // Assert
        logEvent.Properties.Should().NotContainKey("my_property");
    }

    [Fact]
    public void WhenPropertyDoesNotExist_ThenContinuesAsUsual()
    {
        // Arrange
        var enricher = new DropPropertyEnricher("my_property");
        var logEvent = new LogEvent(
            DateTimeOffset.Now,
            LogEventLevel.Fatal,
            null,
            MessageTemplate.Empty,
            new List<LogEventProperty>()
        );

        // Act
        var enrichmentAction = () => enricher.Enrich(logEvent, new LogEventPropertyFactory());

        // Assert
        enrichmentAction.Should().NotThrow();
    }

    [Fact]
    public void WhenOtherPropertyAlsoExists_ThenDropsOnlyProperty()
    {
        // Arrange
        var enricher = new DropPropertyEnricher("my_property");
        var myProperty = new LogEventProperty("my_property", new ScalarValue(42));
        var otherProperty = new LogEventProperty("other_property", new ScalarValue(42));
        var logEvent = new LogEvent(
            DateTimeOffset.Now,
            LogEventLevel.Fatal,
            null,
            MessageTemplate.Empty,
            new List<LogEventProperty>() { myProperty, otherProperty }
        );

        // Act
        enricher.Enrich(logEvent, new LogEventPropertyFactory());

        // Assert
        logEvent.Properties.Should().ContainKey("other_property");
    }
}
