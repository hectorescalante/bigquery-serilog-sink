using AutoFixture;
using AutoFixture.AutoMoq;
using Serilog.Events;
using Serilog.Parsing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Serilog.Sinks.BigQuery.Tests
{
  public class BaseTests
  {
    public BaseTests()
    {
      AutoFixture = new Fixture().Customize(new AutoMoqCustomization());
    }

    public IFixture AutoFixture { get; set; }

    public static IEnumerable<object[]> GetLogEvents()
    {
      yield return new object[]
      {
        new List<LogEvent>()
        {
          new LogEvent(DateTimeOffset.UtcNow, LogEventLevel.Verbose, null, 
            new MessageTemplate("New message from {MachineName}", 
              new List<MessageTemplateToken>()
              {
                { new TextToken("localhost", 17) }
              }
            ),
            new List<LogEventProperty>
            {
              new LogEventProperty("NetworkCards", new StructureValue(new List<LogEventProperty>
              {
                new LogEventProperty("Card0", new ScalarValue("abc:def")),
                new LogEventProperty("Card1", new ScalarValue("ghi:jkl")),
              })),
              new LogEventProperty("MachineName", new ScalarValue("localhost"))
            }
          ),
          new LogEvent(DateTimeOffset.UtcNow, LogEventLevel.Verbose, null, new MessageTemplate("New message from {Environment}", new List<MessageTemplateToken>()),
            new List<LogEventProperty>
            {
                new LogEventProperty("Environment", new ScalarValue("Development"))
            }
          )
        }
      };
    }

  }
}
