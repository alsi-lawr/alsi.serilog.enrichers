using ALSI.Serilog.Enrichers.Environment;
using FluentAssertions;
using NSubstitute;
using Serilog.Core;
using Serilog.Events;

namespace ALSI.Serilog.Enrichers.UnitTests;

public class EnvironmentNameTests
{
    private static void ClearEnvironmentVariables()
    {
        System.Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", null);
        System.Environment.SetEnvironmentVariable("DOTNET_ENVIRONMENT", null);
    }

    [Fact]
    public void WhenASPNetCoreEnvironmentNameIsSet_ThenTheEnvironmentNameIsSet()
    {
        // Arrange
        ClearEnvironmentVariables();
        System.Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "test");
        var propertyFactory = new LogEventPropertyFactory();
        var environmentNameProperty = new EnvironmentName(propertyFactory);

        // Act
        var updatedProperty = environmentNameProperty.GetUpdated(propertyFactory);

        // Assert
        updatedProperty.Property.Value.Should().BeOfType<ConcreteLogEventPropertyValue>();
        ((ConcreteLogEventPropertyValue)updatedProperty.Property.Value).Value.Should().Be("test");
        updatedProperty.Should().Be(environmentNameProperty);
    }

    [Fact]
    public void WhenDotnetEnvironmentNameIsSet_ThenTheEnvironmentNameIsSet()
    {
        // Arrange
        ClearEnvironmentVariables();
        System.Environment.SetEnvironmentVariable("DOTNET_ENVIRONMENT", "test");
        var propertyFactory = new LogEventPropertyFactory();
        var environmentNameProperty = new EnvironmentName(propertyFactory);

        // Act
        var updatedProperty = environmentNameProperty.GetUpdated(propertyFactory);

        // Assert
        updatedProperty.Property.Value.Should().BeOfType<ConcreteLogEventPropertyValue>();
        ((ConcreteLogEventPropertyValue)updatedProperty.Property.Value).Value.Should().Be("test");
        updatedProperty.Should().Be(environmentNameProperty);
    }

    [Fact]
    public void WhenNeitherEnvironmentNameIsSet_ThenTheEnvironmentNameIsProduction()
    {
        // Arrange
        ClearEnvironmentVariables();
        var propertyFactory = new LogEventPropertyFactory();
        var environmentNameProperty = new EnvironmentName(propertyFactory);

        // Act
        var updatedProperty = environmentNameProperty.GetUpdated(propertyFactory);

        // Assert
        updatedProperty.Property.Value.Should().BeOfType<ConcreteLogEventPropertyValue>();
        ((ConcreteLogEventPropertyValue)updatedProperty.Property.Value).Value.Should().Be("Production");
        updatedProperty.Should().Be(environmentNameProperty);
    }

    [Fact]
    public void WhenEnvironmentNameChanged_ShouldReturnSameProperty()
    {
        // Arrange
        System.Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "test");
        var propertyFactory = new LogEventPropertyFactory();
        var environmentNameProperty = new EnvironmentName(propertyFactory);
        System.Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "updated");

        // Act
        var updatedProperty = environmentNameProperty.GetUpdated(propertyFactory);

        // Assert
        updatedProperty.Property.Value.Should().BeOfType<ConcreteLogEventPropertyValue>();
        ((ConcreteLogEventPropertyValue)updatedProperty.Property.Value).Value.Should().Be("test");
        updatedProperty.Should().Be(environmentNameProperty);
    }
}
