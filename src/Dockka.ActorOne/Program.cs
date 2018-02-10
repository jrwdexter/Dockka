using System;
using System.IO;
using Akka.Actor;
using Akka.Configuration;

namespace Dockka.ActorOne
{
    class Program
    {
        static void Main(string[] args)
        {
            // Get actor configuration
            using (var reader = new StreamReader(File.OpenRead("./akka.conf")))
            {
                var config = ConfigurationFactory.ParseString(reader.ReadToEnd());
                // Join the cluster
                using (var system = ActorSystem.Create("dockka-system", config))
                {
                    // Start the actor
                    system.ActorOf<AddPersonActor>("addPerson");

                    // Wait indefinitely
                    Console.ReadKey();
                }
            }
        }
    }
}
