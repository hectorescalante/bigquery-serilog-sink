using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Serilog.Sinks.BigQuery.Sample
{
  class Program
  {
    static async Task Main(string[] args)
    {
      var configuration = LoadConfiguration();

      Log.Logger = new LoggerConfiguration()
      .Enrich.FromLogContext()
      .Enrich.WithProperty("Host", Environment.MachineName)
      .WriteTo.Console()
      .WriteTo.BigQuery(configuration.GetSection("BigQuery"), Events.LogEventLevel.Information)
      .CreateLogger();


      Log.Information("{a} {b}!", "Hello", "World");
      Log.Warning("{a} {b}!", "Watch out", "World");
      Log.Error("{a} {b}!", "Told you", "World");

      await Task.Delay(60000);
    }

    private static IConfiguration LoadConfiguration()
    {
      var builder = new ConfigurationBuilder()
          .SetBasePath(Directory.GetCurrentDirectory())
          .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
          .AddEnvironmentVariables();

      return builder.Build();
    }

  }
}
