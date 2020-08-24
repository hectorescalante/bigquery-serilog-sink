using BigQuery.Schema.Helper.Core;

namespace BigQuery.Serilog.Sink
{
  public class SinkOptions : BigQuerySchemaHelperOptions
  {
    public int BatchSizeLimit { get; set; } = 200;
    public int PeriodSeconds { get; set; } = 2;
  }
}
