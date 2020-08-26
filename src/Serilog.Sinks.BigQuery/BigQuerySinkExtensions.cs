using Microsoft.Extensions.Configuration;
using Serilog.Configuration;
using Serilog.Events;
using Serilog.Sinks.BigQuery.Core;
using Serilog.Sinks.BigQuery.Infrastructure;

namespace Serilog.Sinks.BigQuery
{
  public static class BigQuerySinkExtensions
  {
    public static LoggerConfiguration BigQuery(this LoggerSinkConfiguration sinkConfiguration, string projectId, string datasetName = "logs", LogEventLevel logLevel = LogEventLevel.Warning)
    {
      var sinkOptions = new BigQuerySinkOptions() { ProjectId = projectId, DatasetName = datasetName };
      return sinkConfiguration.BigQuery(sinkOptions, logLevel);
    }
    public static LoggerConfiguration BigQuery(this LoggerSinkConfiguration sinkConfiguration, IConfigurationSection configuration, LogEventLevel logLevel = LogEventLevel.Warning)
    {
      var sinkOptions = new BigQuerySinkOptions();
      configuration.Bind(sinkOptions);
      return sinkConfiguration.BigQuery(sinkOptions, logLevel);
    }
    public static LoggerConfiguration BigQuery(this LoggerSinkConfiguration sinkConfiguration, BigQuerySinkOptions sinkOptions, LogEventLevel logLevel = LogEventLevel.Warning)
    {
      return sinkConfiguration.Sink(new BigQuerySink(sinkOptions, new BigQueryConnection(sinkOptions)), logLevel);
    }

  }
}
