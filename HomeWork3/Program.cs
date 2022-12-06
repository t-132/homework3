using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.IO;
using Serilog.Sinks.SpectreConsole;
using Npgsql;
using HomeWork3.DataAccess;

namespace HomeWork3
{
   class Program
   {
      public static void Main(string[] args)
      {
         try
         {
            var builder = new ConfigurationBuilder();
            BuildConfig(builder);
            var config = builder.Build();

            Log.Logger = new LoggerConfiguration()
               .ReadFrom.Configuration(config)
               .Enrich.FromLogContext()
               .WriteTo.SpectreConsole()
               .CreateLogger();

            Log.Logger.Information("Starting...");

            var loggerFactory = LoggerFactory.Create(logging => { logging.AddSerilog(Log.Logger); });
            var s = new NpgsqlDataSourceBuilder(config.GetValue<string>("ConnectionString"))
                .UseLoggerFactory(loggerFactory)
                .Build();

            DataSourceBuilder.src = s;

            var host = Host.CreateDefaultBuilder()
               .ConfigureServices((ctx, srv) =>
                   {
                      srv.AddTransient<IAppService, AppService>();
                      srv.AddSingleton<IDataSourceBuilder, DataSourceBuilder>();
                      srv.AddTransient<IAppRepo, AppRepo>();
                   })               
               .UseSerilog()
               .Build();

            var svc = ActivatorUtilities.CreateInstance<AppService>(host.Services);
            svc.Run();
            
         }
         catch (Exception ex)
         {
            Log.Fatal(ex.Message);
         }
         finally
         {
            Log.Information("Stop");
         }
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
