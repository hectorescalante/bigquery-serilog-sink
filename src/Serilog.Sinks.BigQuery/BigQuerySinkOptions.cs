using BigQuery.Schema.Helper.Core;

namespace Serilog.Sinks.BigQuery
{
  public class BigQuerySinkOptions : BigQuerySchemaHelperOptions
  {
    public int BatchSizeLimit { get; set; } = 200;
    public int PeriodSeconds { get; set; } = 2;
  }
}
