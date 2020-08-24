using BigQuery.Serilog.Sink.Core.Abstractions;
using Serilog.Events;
using Serilog.Sinks.PeriodicBatching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BigQuery.Serilog.Sink.Core
{
  public class BigQuerySink : PeriodicBatchingSink
  {
    private readonly SinkOptions _sinkOptions;
    private readonly IConnection _connection;

    public BigQuerySink(SinkOptions sinkOptions, IConnection connection) : base(sinkOptions.BatchSizeLimit, new TimeSpan(0, 0, sinkOptions.PeriodSeconds))
    {
      _sinkOptions = sinkOptions;
      _connection = connection;
    }

    protected override async Task EmitBatchAsync(IEnumerable<LogEvent> events)
    {
      if (_sinkOptions.IsEnabled)
      {
        try
        {
          await _connection.CreateLogTableAsync();
          await _connection.InsertLogEventsAsync(events.Select(e => (BigQueryLogEvent)e));
        }
        catch (Exception ex)
        {
          Console.WriteLine(ex);
        }
      }
    }

    protected override void Dispose(bool disposing)
    {
      _connection.Dispose();
      base.Dispose(disposing);
    }

  }
}
