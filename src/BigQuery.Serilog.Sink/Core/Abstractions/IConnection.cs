using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BigQuery.Serilog.Sink.Core.Abstractions
{
  public interface IConnection : IDisposable
  {
    Task CreateLogTableAsync();
    Task InsertLogEventsAsync(IEnumerable<BigQueryLogEvent> bigQueryLogEvents);
  }
}
