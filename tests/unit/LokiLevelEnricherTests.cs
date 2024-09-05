using System.Collections;
using FluentAssertions;
using Serilog.Events;

namespace ALSI.Serilog.Enrichers.UnitTests;

public class LokiLevelEnricherTests
{
    [Fact]
    public void WhenEnrichingDebugLog_ThenEnrichesDebugLog()
    {
        // Arrange
        var enricher = new LokiLevelEnricher();
        var logEvent = new LogEvent(DateTimeOffset.Now, LogEventLevel.Debug, null, MessageTemplate.Empty, []);
        
        // Act
        enricher.Enrich(logEvent, new LogEventPropertyFactory());
        
        // Assert
        logEvent.Properties.Should().ContainKey("level");
        logEvent.Properties["level"].Should().BeOfType<ConcreteLogEventPropertyValue>();
        ((ConcreteLogEventPropertyValue)logEvent.Properties["level"]).Value.Should().Be("debug");
    }
    
    [Fact]
    public void WhenEnrichingVerboseLog_ThenEnrichesVerboseLog()
    {
        // Arrange
        var enricher = new LokiLevelEnricher();
        var logEvent = new LogEvent(DateTimeOffset.Now, LogEventLevel.Verbose, null, MessageTemplate.Empty, []);
        
        // Act
        enricher.Enrich(logEvent, new LogEventPropertyFactory());
        
        // Assert
        logEvent.Properties.Should().ContainKey("level");
        logEvent.Properties["level"].Should().BeOfType<ConcreteLogEventPropertyValue>();
        ((ConcreteLogEventPropertyValue)logEvent.Properties["level"]).Value.Should().Be("trace");
    }
    
    [Fact]
    public void WhenEnrichingInformationLog_ThenEnrichesInformationLog()
    {
        // Arrange
        var enricher = new LokiLevelEnricher();
        var logEvent = new LogEvent(DateTimeOffset.Now, LogEventLevel.Information, null, MessageTemplate.Empty, []);
        
        // Act
        enricher.Enrich(logEvent, new LogEventPropertyFactory());
        
        // Assert
        logEvent.Properties.Should().ContainKey("level");
        logEvent.Properties["level"].Should().BeOfType<ConcreteLogEventPropertyValue>();
        ((ConcreteLogEventPropertyValue)logEvent.Properties["level"]).Value.Should().Be("info");
    }
    
    [Fact]
    public void WhenEnrichingWarningLog_ThenEnrichesWarningLog()
    {
        // Arrange
        var enricher = new LokiLevelEnricher();
        var logEvent = new LogEvent(DateTimeOffset.Now, LogEventLevel.Warning, null, MessageTemplate.Empty, []);
        
        // Act
        enricher.Enrich(logEvent, new LogEventPropertyFactory());
        
        // Assert
        logEvent.Properties.Should().ContainKey("level");
        logEvent.Properties["level"].Should().BeOfType<ConcreteLogEventPropertyValue>();
        ((ConcreteLogEventPropertyValue)logEvent.Properties["level"]).Value.Should().Be("warn");
    }
    
    [Fact]
    public void WhenEnrichingErrorLog_ThenEnrichesErrorLog()
    {
        // Arrange
        var enricher = new LokiLevelEnricher();
        var logEvent = new LogEvent(DateTimeOffset.Now, LogEventLevel.Error, null, MessageTemplate.Empty, []);
        
        // Act
        enricher.Enrich(logEvent, new LogEventPropertyFactory());
        
        // Assert
        logEvent.Properties.Should().ContainKey("level");
        logEvent.Properties["level"].Should().BeOfType<ConcreteLogEventPropertyValue>();
        ((ConcreteLogEventPropertyValue)logEvent.Properties["level"]).Value.Should().Be("error");
    }
    
    [Fact]
    public void WhenEnrichingFatalLog_ThenEnrichesFatalLog()
    {
        // Arrange
        var enricher = new LokiLevelEnricher();
        var logEvent = new LogEvent(DateTimeOffset.Now, LogEventLevel.Fatal, null, MessageTemplate.Empty, []);
        
        // Act
        enricher.Enrich(logEvent, new LogEventPropertyFactory());
        
        // Assert
        logEvent.Properties.Should().ContainKey("level");
        logEvent.Properties["level"].Should().BeOfType<ConcreteLogEventPropertyValue>();
        ((ConcreteLogEventPropertyValue)logEvent.Properties["level"]).Value.Should().Be("fatal");
    }
}