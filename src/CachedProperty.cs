using Serilog.Core;
using Serilog.Events;

namespace ALSI.Serilog.Enrichers;

/// <summary>
/// Enriches the log event with a cached property.
/// </summary>
internal abstract record CachedProperty
{
    internal LogEventProperty Property { get; private init; }

    private readonly CreateProperty _createProperty;

    internal CachedProperty(ILogEventPropertyFactory propertyFactory, CreateProperty createProperty)
    {
        _createProperty = createProperty;
        Property = _createProperty(propertyFactory);
    }

    public CachedProperty GetUpdated(ILogEventPropertyFactory propertyFactory)
    {
        if (ShouldUpdate())
        {
            return this with { Property = _createProperty(propertyFactory) };
        }

        return this;
    }

    protected abstract bool ShouldUpdate();
}

internal delegate LogEventProperty CreateProperty(ILogEventPropertyFactory propertyFactory);
