using System;
using Akka.Actor;
using Akka.Cluster.Tools.PublishSubscribe;
using Dockka.Config;
using Dockka.Data.Context;
using Dockka.Data.Extensions;
using Dockka.Data.Messages;
using Dockka.Data.Messages.Base;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Dockka.ActorOne
{
    /// <summary>
    /// The first actor in our actor chain.
    /// Accepts a <see cref="IAddPersonMessage"/>, then updates the database, and sends a message to another actor to get that person.
    /// Ignore the fact that this is bad design.
    /// </summary>
    internal class AddPersonActor : ReceiveActor
    {
        public AddPersonActor()
        {
            // Due to distributed systems, we have to subscribe to 'queryPerson'
            var mediator = DistributedPubSub.Get(Context.System).Mediator;
            mediator.Tell(new Subscribe("addPerson", Self));

            ReceiveAsync<IAddPersonMessage>(async personEvent =>
            {
                Log.Information($"addPerson received person: (FirstName {personEvent.Person.FirstName}, LastName {personEvent.Person.LastName}).");
                // Don't do this - did in the interest of time
                // Prefer injected DB Context of some sort.
                var options = new DbContextOptionsBuilder<DockkaContext>()
                    .UseSqlServer(Configuration.DockkaSqlConnection)
                    .WaitForActiveServer(TimeSpan.FromSeconds(300))
                    .Options;
                using (var context = new DockkaContext(options))
                {
                    // Add the person to the DB
                    var entry = await context.AddAsync(personEvent.Person);
                    await context.SaveChangesAsync();

                    // And request the query actor to get that person
                    mediator.Tell(new Publish("queryPerson", new QueryPersonMessage { Id = entry.Entity.Id }));
                }
            });
        }
    }
}