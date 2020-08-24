using BigQuery.Schema.Helper.Core;
using BigQuery.Serilog.Sink.Core;
using BigQuery.Serilog.Sink.Core.Abstractions;
using Google.Cloud.BigQuery.V2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BigQuery.Serilog.Sink.Infrastructure
{
  internal class BigQueryConnection : IConnection
  {
    private readonly SinkOptions _sinkOptions;
    public BigQueryConnection(SinkOptions sinkOptions) =>
      _sinkOptions = sinkOptions;

    public static BigQueryDataset Dataset { get; set; }
    public static BigQueryTable Table { get; set; }
    public static BigQueryClient Client { get; set; }
    public Type LogType { get; set; } = typeof(BigQueryLogEvent);

    public async Task CreateLogTableAsync()
    {
      if (Client == null)
        Client = BigQueryClient.Create(_sinkOptions.ProjectId);

      if (Dataset == null)
        Dataset = await Client.GetOrCreateDatasetAsync(_sinkOptions.DatasetName);

      if (Table == null)
      {
        var schemaBuilder = BigQuerySchemaBuilder.GetSchemaBuilder(LogType, "> ");
        var createTableOptions = new CreateTableOptions() { TimePartitioning = TimePartition.CreateDailyPartitioning(expiration: null) };
        Table = await Client.GetOrCreateTableAsync(_sinkOptions.DatasetName, _sinkOptions.GetTableName(LogType.Name), schemaBuilder.Build(), createOptions: createTableOptions);
      }
    }

    public async Task InsertLogEventsAsync(IEnumerable<BigQueryLogEvent> bigQueryLogEvents)
    {
      var bigQueryRows = bigQueryLogEvents.Select(log => BigQueryRowAdapter.GetRowFromEntity(log, LogType, ""));
      var insertOptions = new InsertOptions() { AllowUnknownFields = true };

      await Client.InsertRowsAsync(_sinkOptions.DatasetName, _sinkOptions.GetTableName(LogType.Name), bigQueryRows, insertOptions);
    }

    public void Dispose()
    {
      Client.Dispose();
    }
  }
}
