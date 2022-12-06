using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace HomeWork3
{
   public class AppService : IAppService
   {
      private readonly ILogger<AppService> _log;
      private readonly IConfiguration _config;

      public AppService(ILogger<AppService> log, IConfiguration config)
      {
         _log = log;
         _config = config;
      }

      public void Run()
      {
         for (int i = 0; i <= _config.GetValue<int>("count"); i++)
         {
            _log.LogInformation("Im live {count}", i);
         }
      }
   }
}
