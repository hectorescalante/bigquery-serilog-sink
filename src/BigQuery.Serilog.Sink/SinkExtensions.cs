using BigQuery.Schema.Helper.Core.Abstractions;
using BigQuery.Serilog.Sink.Core;
using BigQuery.Serilog.Sink.Infrastructure;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Configuration;
using Serilog.Events;

namespace BigQuery.Serilog.Sink
{
  public static class SinkExtensions
  {
    public static LoggerConfiguration RabbitMQ(this LoggerSinkConfiguration sinkConfiguration, IConfigurationSection configuration)
    {
      return sinkConfiguration.RabbitMQ(configuration, LogEventLevel.Warning);
    }
    public static LoggerConfiguration RabbitMQ(this LoggerSinkConfiguration sinkConfiguration, IConfigurationSection configuration, LogEventLevel logLevel)
    {
      var sinkOptions = new SinkOptions();
      configuration.Bind(sinkOptions);
      return sinkConfiguration.RabbitMQ(sinkOptions, logLevel);
    }
    public static LoggerConfiguration RabbitMQ(this LoggerSinkConfiguration sinkConfiguration, SinkOptions sinkOptions)
    {
      return sinkConfiguration.RabbitMQ(sinkOptions, LogEventLevel.Warning);
    }
    public static LoggerConfiguration RabbitMQ(this LoggerSinkConfiguration sinkConfiguration, SinkOptions sinkOptions, LogEventLevel logLevel)
    {
      return sinkConfiguration.Sink(new BigQuerySink(sinkOptions, new BigQueryConnection(sinkOptions)), logLevel);
    }

  }
}
