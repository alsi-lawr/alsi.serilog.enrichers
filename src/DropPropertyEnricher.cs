using Serilog;
using Serilog.Configuration;
using Serilog.Core;
using Serilog.Events;

namespace ALSI.Serilog.Enrichers;

internal class DropPropertyEnricher : ILogEventEnricher
{
    private readonly string _propertyToRemove;

    internal DropPropertyEnricher(string propertyToRemove)
    {
        _propertyToRemove = propertyToRemove;
    }

    public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
    {
        logEvent.RemovePropertyIfPresent(_propertyToRemove);
    }
}

/// <summary>
/// Drop property extensions.
/// </summary>
#pragma warning disable SA1202
public static class DropPropertyEnrichmentExtensions
#pragma warning restore SA1202
{
    /// <summary>
    /// Drops the specified property from the log.
    /// </summary>
    /// <param name="enrichmentConfiguration">The enrichment configuration.</param>
    /// <param name="propertyToRemove">The property to remove.</param>
    /// <returns>The logger configuration.</returns>
    public static LoggerConfiguration ByDropping(
        this LoggerEnrichmentConfiguration enrichmentConfiguration,
        string propertyToRemove
    ) => enrichmentConfiguration.With(new DropPropertyEnricher(propertyToRemove));
}
