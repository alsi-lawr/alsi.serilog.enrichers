using Serilog.Core;
using Serilog.Events;

namespace ALSI.Serilog.Enrichers.UnitTests;

public class ConcreteLogEventPropertyValue(object? value) : LogEventPropertyValue
{
    public object? Value { get; } = value;

    public override void Render(TextWriter output, string? format = null, IFormatProvider? formatProvider = null)
    {
    }
}

public class LogEventPropertyFactory : ILogEventPropertyFactory
{
    public LogEventProperty CreateProperty(string name, object? value, bool destructureObjects = false) =>
        new(name, new ConcreteLogEventPropertyValue(value));
}