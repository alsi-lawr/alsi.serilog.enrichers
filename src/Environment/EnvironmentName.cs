using Serilog.Core;
using Serilog.Events;

namespace ALSI.Serilog.Enrichers.Environment;

internal sealed record EnvironmentName : CachedProperty
{
    public EnvironmentName(ILogEventPropertyFactory propertyFactory)
        : base(propertyFactory, CreateProperty) { }

    protected override bool ShouldUpdate() => false; // Cached from startup

    private static LogEventProperty CreateProperty(ILogEventPropertyFactory propertyFactory)
    {
        var value = System.Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        if (string.IsNullOrWhiteSpace(value))
        {
            value = System.Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT");
        }

        if (string.IsNullOrWhiteSpace(value))
        {
            value = "Production";
        }

        return propertyFactory.CreateProperty("deployment_environment", value);
    }
}
