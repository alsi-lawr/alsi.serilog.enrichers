using Serilog;
using Serilog.Configuration;
using Serilog.Core;
using Serilog.Events;

namespace ALSI.Serilog.Enrichers;

internal class LokiLevelEnricher : ILogEventEnricher
{
    public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
    {
        logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty("level", GetLokiLevelName(logEvent.Level)));
    }

    private static string GetLokiLevelName(LogEventLevel level) =>
        level switch
        {
            LogEventLevel.Debug => "debug",
            LogEventLevel.Information => "info",
            LogEventLevel.Warning => "warn",
            LogEventLevel.Error => "error",
            LogEventLevel.Fatal => "fatal",
            LogEventLevel.Verbose => "trace",
            _ => "info",
        };
}

/// <summary>
/// Extensions for the loki level enricher.
/// </summary>
#pragma warning disable SA1202
public static class LokiLevelEnrichmentExtensions
#pragma warning restore SA1202
{
    /// <summary>
    /// Enriches the log with loki-compliant levels.
    /// </summary>
    /// <param name="enrichmentConfiguration">The enrichment configuration.</param>
    /// <returns>The logger configuration.</returns>
    public static LoggerConfiguration FromLokiLevels(this LoggerEnrichmentConfiguration enrichmentConfiguration) =>
        enrichmentConfiguration.With(new LokiLevelEnricher());
}
