using Serilog.Core;
using Serilog.Events;

namespace ALSI.Serilog.Enrichers.UnitTests;

public class ConcreteLogEventPropertyValue : LogEventPropertyValue
{
    public ConcreteLogEventPropertyValue(object? value)
    {
        Value = value;
    }

    public object? Value { get; init; }

    public override void Render(TextWriter output, string? format = null, IFormatProvider? formatProvider = null) { }
}

public class LogEventPropertyFactory : ILogEventPropertyFactory
{
    public LogEventProperty CreateProperty(string name, object? value, bool destructureObjects = false) =>
        new(name, new ConcreteLogEventPropertyValue(value));
}
