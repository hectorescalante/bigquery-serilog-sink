using Serilog.Events;
using Serilog.Sinks.BigQuery.Core;
using System.Collections.Generic;
using Xunit;

namespace Serilog.Sinks.BigQuery.Tests
{
  public class BigQueryLogEventTests : BaseTests
  {
    [Theory]
    [MemberData(nameof(GetLogEvents))]
    public void TestImplicitOperator_WithSerilogEvents_ShouldReturnBigQueryLogEvents(IEnumerable<LogEvent> logEvents)
    {
      //Arrange
      foreach (var log in logEvents)
      {
        //Act
        var bqLog = (BigQueryLogEvent)log;

        //Assert
        Assert.IsType<BigQueryLogEvent>(bqLog);
      }

    }
  }
}
