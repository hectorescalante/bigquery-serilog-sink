using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Serilog.Sinks.BigQuery.Core.Abstractions
{
  public interface IConnection : IDisposable
  {
    Task CreateLogTableAsync();
    Task InsertLogEventsAsync(IEnumerable<BigQueryLogEvent> bigQueryLogEvents);
  }
}
