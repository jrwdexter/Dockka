using Akka.Actor;
using Dockka.Data.Context;
using Dockka.Data.Messages;
using Dockka.Data.Messages.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

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
            ReceiveAsync<IAddPersonMessage>(async personEvent =>
            {
                // Don't do this - did in the interest of time
                // Prefer injected DB Context of some sort.
                var optionsBuilder = new DbContextOptionsBuilder<DockkaContext>()
                    .UseSqlServer(Configuration.Instance.Settings.GetConnectionString("DockkaSqlServer")).Options;
                using (var context = new DockkaContext(optionsBuilder))
                {
                    // Add the person to the DB
                    var entry = await context.AddAsync(personEvent.Person);
                    await context.SaveChangesAsync();

                    // And request the query actor to get that person
                    Context.ActorSelection("../queryPerson").Tell(new QueryPersonMessage { Id = entry.Entity.Id });
                }
            });
        }
    }
}