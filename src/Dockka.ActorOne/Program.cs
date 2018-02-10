using System;
using System.IO;
using Akka.Actor;
using Akka.Configuration;
using Dockka.Data.Context;
using Dockka.Data.Messages;
using Dockka.Data.Messages.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Dockka.ActorOne
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var reader = new StreamReader(File.OpenRead("./akka.conf")))
            {
                var config = ConfigurationFactory.ParseString(reader.ReadToEnd());
                using (var system = ActorSystem.Create("dockka-system", config))
                {
                    system.ActorOf<AddService>("addService");
                    Console.ReadKey();
                }
            }
        }
    }

    internal class AddService : ReceiveActor
    {
        public AddService()
        {
            ReceiveAsync<IAddPersonEvent>(async personEvent =>
            {
                // Don't do this - did in the interest of time
                var optionsBuilder = new DbContextOptionsBuilder<DockkaContext>()
                    .UseSqlServer(Configuration.Instance.Settings.GetConnectionString("DockkaSqlServer")).Options;
                using (var context = new DockkaContext(optionsBuilder))
                {
                    var entry = await context.AddAsync(personEvent.Person);
                    await context.SaveChangesAsync();
                    Context.ActorSelection("../queryService").Tell(new QueryPersonEvent { Id = entry.Entity.Id });
                }
            });
        }
    }
}
