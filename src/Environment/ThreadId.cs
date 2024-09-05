using Serilog.Core;
using Serilog.Events;

namespace ALSI.Serilog.Enrichers.Environment;

internal sealed record ThreadId : CachedProperty
{
    /// <param name="propertyFactory">The property factory.</param>
    public ThreadId(ILogEventPropertyFactory propertyFactory)
        : base(propertyFactory, CreateProperty) { }

    /// <inheritdoc/>
    protected override bool ShouldUpdate() =>
        (int)((ScalarValue)Property.Value).Value! != System.Environment.CurrentManagedThreadId; // Updates on new thread.

    private static LogEventProperty CreateProperty(ILogEventPropertyFactory propertyFactory) =>
        new("thread_id", new ScalarValue(System.Environment.CurrentManagedThreadId));
}
