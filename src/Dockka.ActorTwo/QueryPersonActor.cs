using Akka.Actor;
using Dockka.Data.Context;
using Dockka.Data.Messages;
using Dockka.Data.Messages.Base;
using Dockka.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

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
            ReceiveAsync<IQueryPersonMessage>(async queryMessage =>
            {
                // Don't do this - did in the interest of time
                // Prefer injected DB Context of some sort.
                var optionsBuilder = new DbContextOptionsBuilder<DockkaContext>()
                    .UseSqlServer(Configuration.Instance.Settings.GetConnectionString("DockkaSqlServer")).Options;
                using (var context = new DockkaContext(optionsBuilder))
                {
                    // Add the person to the DB
                    var person = await context.FindAsync<Person>(queryMessage.Id);

                    // Request the UI actor to update the UI
                    Context.ActorSelection("../updateUi").Tell(new UpdateUiMessage { Person = person });
                }
            });
        }
    }
}