using Microsoft.Extensions.Configuration;
using Serilog.Configuration;
using Serilog.Events;
using Serilog.Sinks.BigQuery.Core;
using Serilog.Sinks.BigQuery.Infrastructure;

namespace Serilog.Sinks.BigQuery
{
  public static class BigQuerySinkExtensions
  {
    public static LoggerConfiguration BigQuery(this LoggerSinkConfiguration sinkConfiguration, IConfigurationSection configuration)
    {
      return sinkConfiguration.BigQuery(configuration, LogEventLevel.Warning);
    }
    public static LoggerConfiguration BigQuery(this LoggerSinkConfiguration sinkConfiguration, IConfigurationSection configuration, LogEventLevel logLevel)
    {
      var sinkOptions = new BigQuerySinkOptions();
      configuration.Bind(sinkOptions);
      return sinkConfiguration.BigQuery(sinkOptions, logLevel);
    }
    public static LoggerConfiguration BigQuery(this LoggerSinkConfiguration sinkConfiguration, BigQuerySinkOptions sinkOptions)
    {
      return sinkConfiguration.BigQuery(sinkOptions, LogEventLevel.Warning);
    }
    public static LoggerConfiguration BigQuery(this LoggerSinkConfiguration sinkConfiguration, BigQuerySinkOptions sinkOptions, LogEventLevel logLevel)
    {
      return sinkConfiguration.Sink(new BigQuerySink(sinkOptions, new BigQueryConnection(sinkOptions)), logLevel);
    }

  }
}
