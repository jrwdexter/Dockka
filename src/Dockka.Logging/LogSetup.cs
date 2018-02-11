using System;
using Serilog;
using Serilog.Events;

namespace Dockka.Logging
{
    public static class LogSetup
    {
        public static void ConfigureLogger()
        {
            Serilog.Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .WriteTo.ColoredConsole()
                .WriteTo.Http("http://logstash:2120")
                .CreateLogger();
        }
    }
}
