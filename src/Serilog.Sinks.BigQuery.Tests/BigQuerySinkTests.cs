using AutoFixture;
using Moq;
using Serilog.Events;
using Serilog.Sinks.BigQuery.Core;
using Serilog.Sinks.BigQuery.Core.Abstractions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Serilog.Sinks.BigQuery.Tests
{
  public class BigQuerySinkTests : BaseTests
  {

    [Theory]
    [MemberData(nameof(GetLogEvents))]
    public async Task TestEmitBatchAsync_WithEnabledTrue_ShouldInsertLogEvents(IEnumerable<LogEvent> logEvents)
    {
      //Arrange
      AutoFixture.Customize<BigQuerySinkOptions>(opt => opt.With(p => p.IsEnabled, true));
      var connection = AutoFixture.Freeze<Mock<IConnection>>();

      //Act
      var sut = AutoFixture.Create<SinkTest>();
      await sut.TestEmitBatchAsync(logEvents);

      //Assert
      connection.Verify(mock => mock.CreateLogTableAsync(), Times.Once);
      connection.Verify(mock => mock.InsertLogEventsAsync(It.IsAny<IEnumerable<BigQueryLogEvent>>()), Times.Once);
    }
  }

  public class SinkTest : BigQuerySink
  {

    public SinkTest(BigQuerySinkOptions sinkOptions, IConnection connection) : base(sinkOptions, connection) { }

    public async Task TestEmitBatchAsync(IEnumerable<LogEvent> events) => await base.EmitBatchAsync(events);
  }

}
