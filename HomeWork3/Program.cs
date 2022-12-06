using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.IO;
using Serilog.Sinks.SpectreConsole;

namespace HomeWork3
{
   class Program
   {
      public static void Main(string[] args)
      {
         var builder = new ConfigurationBuilder();
         BuildConfig(builder);

         Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(builder.Build())
            .Enrich.FromLogContext()
            .WriteTo.SpectreConsole()
            .CreateLogger();

         Log.Logger.Information("Starting...");
         
         var host = Host.CreateDefaultBuilder()            
            .ConfigureServices((ctx, srv) =>
                {
                   srv.AddTransient<IAppService, AppService>();
                   srv.
                })
            .UseSerilog()
            .Build();
         var svc = ActivatorUtilities.CreateInstance<AppService>(host.Services);
         svc.Run();
      }

      public static void BuildConfig(IConfigurationBuilder builder)
      {
         builder.SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
           .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ENVIRONMENT") ?? "Production"}.json", optional: true)
           .AddEnvironmentVariables();
      }
   }
}
