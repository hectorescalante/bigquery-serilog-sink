using Serilog.Events;
using System;
using System.Collections.Generic;

namespace Serilog.Sinks.BigQuery.Core
{
  public class BigQueryLogEvent
  {
    public DateTimeOffset Time { get; set; }
    public int LevelNumber { get; set; }
    public string LevelName { get; set; }
    public string Message { get; set; }
    public string Template { get; set; }
    public List<BigQueryLogEventProperty> Properties { get; set; }
    public string ExceptionStackTrace { get; set; }


    public static implicit operator BigQueryLogEvent(LogEvent logEvent)
    {
      var instance = new BigQueryLogEvent()
      {
        Time = logEvent.Timestamp,
        LevelNumber = (int)logEvent.Level,
        LevelName = logEvent.Level.ToString(),
        Template = logEvent.MessageTemplate.Text,
        Message = logEvent.RenderMessage(),
        Properties = new List<BigQueryLogEventProperty>(),
        ExceptionStackTrace = logEvent.Exception?.StackTrace
      };

      foreach (var property in logEvent.Properties)
      {
        AddProperties(instance.Properties, property.Key, property.Value);
      }

      return instance;
    }

    private static void AddProperties(List<BigQueryLogEventProperty> properties, string propertyName, LogEventPropertyValue propertyValue)
    {
      switch (propertyValue)
      {
        case ScalarValue value:
          var stringValue = value.Value != null ? value.Value.ToString() : string.Empty;
          properties.Add(new BigQueryLogEventProperty() { Name = propertyName, Value = stringValue });
          break;
        case SequenceValue value:
          properties.Add(new BigQueryLogEventProperty() { Name = propertyName, Value = string.Join("|", value.Elements) });
          break;
        case StructureValue value:
          foreach (var childProperty in value.Properties)
            AddProperties(properties, $"{propertyName}.{childProperty.Name}", childProperty.Value);
          break;
        case DictionaryValue dictionaryValue:
          foreach (var childProperty in dictionaryValue.Elements)
            AddProperties(properties, $"{propertyName}.{childProperty.Key}", childProperty.Value);
          break;
      }
    }
  }

  public class BigQueryLogEventProperty
  {
    public string Name { get; set; }
    public string Value { get; set; }
  }
}
