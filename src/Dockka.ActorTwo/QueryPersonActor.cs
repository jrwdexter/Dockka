using System;
using Akka.Actor;
using Akka.Cluster.Tools.PublishSubscribe;
using Dockka.Config;
using Dockka.Data.Context;
using Dockka.Data.Extensions;
using Dockka.Data.Messages;
using Dockka.Data.Messages.Base;
using Dockka.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace Dockka.ActorTwo
{
    /// <summary>
    /// The second actor in the actor chaing.
    /// Accepts a <see cref="IQueryPersonMessage"/>, queries for the user, and then requests the UI actor to update.
    /// </summary>
    internal class QueryPersonActor : ReceiveActor
    {
        public QueryPersonActor()
        {
            // Due to distributed systems, we have to subscribe to 'queryPerson'
            var mediator = DistributedPubSub.Get(Context.System).Mediator;
            mediator.Tell(new Subscribe("queryPerson", Self));

            ReceiveAsync<IQueryPersonMessage>(async queryMessage =>
            {
                Log.Information("queryPerson received person");
                // Don't do this - did in the interest of time
                // Prefer injected DB Context of some sort.
                var options = new DbContextOptionsBuilder<DockkaContext>()
                    .UseSqlServer(Configuration.DockkaSqlConnection)
                    .WaitForActiveServer(TimeSpan.FromSeconds(300))
                    .Options;
                using (var context = new DockkaContext(options))
                {
                    // Add the person to the DB
                    var person = await context.FindAsync<Person>(queryMessage.Id);

                    // Request the UI actor to update the UI
                    mediator.Tell(new Publish("updateUi", new UpdateUiMessage { Person = person }));
                }
            });
        }
    }
}