using Akka.Actor;
using Akka.Cluster.Tools.PublishSubscribe;
using Dockka.Data.Messages;
using Dockka.Data.Models;
using Microsoft.AspNetCore.SignalR;
using Serilog;

namespace Dockka.Api.Hubs
{
    public class PersonsHub : Hub
    {
        public void AddPerson(Person person)
        {
            // Tell our mediator about this new person
            Log.Information($"signalR: Received add person request for Person: (FirstName {person.FirstName}, LastName {person.LastName})");
            Program.Mediator.Tell(new Publish("addPerson", new AddPersonMessage { Person = person }));
        }
    }
}
