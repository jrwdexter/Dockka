using System;
using System.IO;
using System.Threading;
using Akka.Actor;
using Akka.Configuration;
using Dockka.Data.Messages;
using Serilog;

namespace Dockka.ActorTwo
{
    class Program
    {
        static void Main(string[] args)
        {
            var logger = new LoggerConfiguration()
                .WriteTo.ColoredConsole()
                .WriteTo.Http("logstash:2120")
                .MinimumLevel.Information()
                .CreateLogger();
            Log.Logger = logger;
            var akkaConfFile = Environment.GetEnvironmentVariable("AKKA_CONF_FILENAME");
            // Get actor configuration
            using (var reader = new StreamReader(File.OpenRead(akkaConfFile ?? "akka.conf")))
            {
                var config = ConfigurationFactory.ParseString(reader.ReadToEnd());
                // Join the cluster
                using (var system = ActorSystem.Create("dockka-system", config))
                {
                    // Start the actor
                    var actor = system.ActorOf<QueryPersonActor>("queryPerson");

                    // Wait indefinitely
                    if(string.IsNullOrEmpty(akkaConfFile))
                        Console.ReadKey();
                    else
                        Thread.Sleep(Timeout.Infinite);
                }
            }
        }
    }
}
