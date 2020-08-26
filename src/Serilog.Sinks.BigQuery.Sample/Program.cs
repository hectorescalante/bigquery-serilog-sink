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
      .MinimumLevel.Verbose()
      .Enrich.FromLogContext()
      .Enrich.WithProperty("Host", Environment.MachineName)
      .WriteTo.Console()
      .WriteTo.BigQuery(configuration.GetSection("BigQuery"), Events.LogEventLevel.Debug)
      .CreateLogger();


      Log.Debug("{a:l} {b}!", "How are you", "World");
      Log.Information("{a:l} {b:l}!", "Hello", "World");
      Log.Warning("{a} {b:l}!", "Watch out", "World");
      Log.Error("{a} {b}!", "Told you", "World");

      try
      {
        throw new Exception("This exception was planned.");
      }
      catch (Exception ex)
      {
        Log.Fatal(ex, "Managed exception: {message}", ex.Message);
      }

      await Task.Delay(60000);

      Log.CloseAndFlush();
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
