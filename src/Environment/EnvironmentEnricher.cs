using Serilog;
using Serilog.Configuration;
using Serilog.Core;
using Serilog.Events;

namespace ALSI.Serilog.Enrichers.Environment;

internal sealed class EnvironmentEnricher : ILogEventEnricher
{
    private readonly Type[] _propertyTypes = { typeof(EnvironmentName), typeof(ThreadId) };
    private readonly Dictionary<Type, CachedProperty> _cachedProperties = new();

    public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
    {
        ProcessUpdated(propertyFactory);
        LogAll(logEvent);
    }

    private void ProcessUpdated(ILogEventPropertyFactory propertyFactory)
    {
        foreach (var type in _propertyTypes)
        {
            if (type.BaseType != typeof(CachedProperty))
            {
                continue;
            }

            if (!_cachedProperties.TryGetValue(type, out var cachedProperty))
            {
                _cachedProperties[type] =
                    Activator.CreateInstance(type, propertyFactory) as CachedProperty
                    ?? throw new InvalidOperationException("Base types must be cached property");
                continue;
            }

            _cachedProperties[type] = cachedProperty.GetUpdated(propertyFactory);
        }
    }

    private void LogAll(LogEvent logEvent)
    {
        foreach (var (_, property) in _cachedProperties)
        {
            logEvent.AddPropertyIfAbsent(property.Property);
        }
    }
}

/// <summary>
/// Environment enrichment extensions.
/// </summary>
#pragma warning disable SA1202
public static class EnvironmentEnrichmentExtensions
#pragma warning restore SA1202
{
    /// <summary>
    /// Enriches log events with the following:
    ///     - FromEnvironment property containing the value of the
    ///       ASPNETCORE_ENVIRONMENT or DOTNET_ENVIRONMENT environment variable.
    ///     - Thread ID of the current running process.
    /// </summary>
    /// <param name="enrichmentConfiguration">The enrichment configuration.</param>
    /// <returns>The logger configuration.</returns>
    public static LoggerConfiguration FromEnvironment(this LoggerEnrichmentConfiguration enrichmentConfiguration) =>
        enrichmentConfiguration.With(new EnvironmentEnricher());
}
