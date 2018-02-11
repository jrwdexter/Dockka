using Akka.Actor;
using Akka.Cluster.Tools.PublishSubscribe;
using Dockka.Data.Context;
using Dockka.Data.Messages;
using Dockka.Data.Messages.Base;
using Dockka.Data.Models;
using Serilog;

namespace Dockka.Actors.ActorTwo
{
    /// <summary>
    /// The second actor in the actor chaing.
    /// Accepts a <see cref="IQueryPersonMessage"/>, queries for the user, and then requests the UI actor to update.
    /// </summary>
    internal class QueryPersonActor : ReceiveActor
    {
        public QueryPersonActor(DockkaContext context)
        {
            // Due to distributed systems, we have to subscribe to 'queryPerson'
            var mediator = DistributedPubSub.Get(Context.System).Mediator;
            mediator.Tell(new Subscribe("queryPerson", Self));

            ReceiveAsync<IQueryPersonMessage>(async queryMessage =>
            {
                Log.Information("queryPerson received person");

                // Get the person from the database
                var person = await context.FindAsync<Person>(queryMessage.Id);

                // Request the UI actor to update the UI
                mediator.Tell(new Publish("updateUi", new UpdateUiMessage { Person = person }));
            });
        }
    }
}