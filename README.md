# Serilog.Sinks.BigQuery

[![Build status](https://dev.azure.com/hectorescalante/Github%20Projects/_apis/build/status/Serilog.Sinks.BigQuery)](https://dev.azure.com/hectorescalante/Github%20Projects/_build/latest?definitionId=6)

## Usage

```
Log.Logger = new LoggerConfiguration()
  ...
  .WriteTo.BigQuery("my-gcp-project-id")
  .CreateLogger();
```

Also with IConfiguration

```
Log.Logger = new LoggerConfiguration()
  ...
  .WriteTo.BigQuery(configuration.GetSection("BigQuery"))
  .CreateLogger();
```
appsettings.json
```
{
  "BigQuery": {
    "ProjectId": "my-gcp-project-id",
    "DatasetName": "mydataset"
  }
}
```

## Log structure

The BigQuery table has the following structure.

| Field name  | Type    |
| ----------  | ----    |
| Time 	      | STRING  |
| LevelNumber |	INTEGER |
| LevelName 	| STRING 	|
| Message 	  | STRING 	|
| Template 	  | STRING 	|
| Properties 	| RECORD (REPEATED) |
| Properties. Name  |	STRING |
| Properties. Value | 	STRING |	
| ExceptionStackTrace |	STRING |

>The properties name column could have "." for nested properties in the form of "RootProperty.ChildProperty".

## Google Application Credentials

Required Roles/Permissions:
- BigQuery Data Editor
- BigQuery User
