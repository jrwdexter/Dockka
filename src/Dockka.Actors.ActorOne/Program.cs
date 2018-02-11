using System;
using System.IO;
using System.Threading;
using Akka.Actor;
using Akka.Configuration;
using Akka.DI.AutoFac;
using Akka.DI.Core;
using Autofac;
using Dockka.Actors.Common;
using Dockka.Logging;

namespace Dockka.Actors.ActorOne
{
    class Program
    {
        static void Main(string[] args)
        {
            LogSetup.ConfigureLogger();

            var akkaConfFile = Environment.GetEnvironmentVariable("AKKA_CONF_FILENAME");
            // Get actor configuration
            using (var reader = new StreamReader(File.OpenRead(akkaConfFile ?? "akka.conf")))
            {
                var config = ConfigurationFactory.ParseString(reader.ReadToEnd());

                // Join the cluster
                using (var system = ActorSystem.Create("dockka-system", config))
                {
                    var container = DependencyInjection.ConfigureDependencyInjection();
                    container.RegisterType<AddPersonActor>();
                    var propsResolver = new AutoFacDependencyResolver(container.Build(), system);

                    // Start the actor
                    system.ActorOf(system.DI().Props<AddPersonActor>(), "addPerson");

                    // Wait indefinitely
                    if (string.IsNullOrEmpty(akkaConfFile))
                        Console.ReadKey();
                    else
                        Thread.Sleep(Timeout.Infinite);
                }
            }
        }
    }
}
