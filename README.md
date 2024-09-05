# ALSI.Serilog.Enrichers

[![NuGet Version](https://img.shields.io/nuget/v/ALSI.Serilog.Enrichers.svg?style=flat)](https://www.nuget.org/packages/ALSI.Serilog.Enrichers/)
[![Build Status](https://github.com/alsi-lawr/alsi.serilog.enrichers/actions/workflows/deploy-nuget.yml/badge.svg)](https://github.com/alsi-lawr/alsi.serilog.enrichers/actions)
[![Downloads](https://img.shields.io/nuget/dt/ALSI.Serilog.Enrichers.svg?logo=nuget&logoSize=auto)](https://www.nuget.org/packages/ALSI.Serilog.Enrichers)
[![codecov](https://codecov.io/gh/alsi-lawr/alsi.serilog.enrichers/graph/badge.svg)](https://codecov.io/gh/alsi-lawr/alsi.serilog.enrichers)

**ALSI.Serilog.Enrichers** is a .NET library that provides 3 enrichers
- **Environment**: enriches the log with the environment name and thread id that logged the message
- **LokiLevel**: enriches the log with a "level" that [Loki](https://grafana.com/oss/loki/) can natively support
- **Drop**: enriches the log by dropping unwanted properties from the log message.

## Getting Started

### Installation

Install the NuGet package using the .NET CLI:

```bash
dotnet add package ALSI.Serilog.Enrichers
```

Or via the NuGet Package Manager:

```bash
Install-Package ALSI.Serilog.Enrichers
```

### Usage

#### Basic Example

```csharp
using ALSI.Serilog.Enrichers;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .Enrich.FromEnvironment()
    .Enrich.FromLokiLevels()
    .Enrich.ByDropping("my_property")
    ...
```

## Contributing

We welcome contributions! Feel free to open an issue or submit a pull request on GitHub.

### Building Locally

Clone the repository:

```bash
git clone https://github.com/alsi-lawr/alsi.serilog.enrichers.git
cd ALSI.Serilog.Enrichers
```

Build the project:

```bash
dotnet build
```

Run tests:

```bash
dotnet test
```
