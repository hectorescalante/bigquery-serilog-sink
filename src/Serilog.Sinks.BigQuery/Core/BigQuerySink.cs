using Serilog.Sinks.BigQuery.Core.Abstractions;
using Serilog.Events;
using Serilog.Sinks.PeriodicBatching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Serilog.Sinks.BigQuery.Core
{
  public class BigQuerySink : PeriodicBatchingSink
  {
    private readonly BigQuerySinkOptions _sinkOptions;
    private readonly IConnection _connection;

    public BigQuerySink(BigQuerySinkOptions sinkOptions, IConnection connection) : base(sinkOptions.BatchSizeLimit, new TimeSpan(0, 0, sinkOptions.PeriodSeconds))
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
