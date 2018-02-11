using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Akka.Actor;
using Akka.Cluster.Tools.PublishSubscribe;
using Dockka.Api.Hubs;
using Dockka.Data.Messages;
using Dockka.Data.Messages.Base;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Sockets;
using Serilog;

namespace Dockka.Api.Actors
{
    /// <summary>
    /// The actor that takes a message and signals to the <see cref="PersonsHub"/> to update the UI.
    /// This actor is set as our seed node, but as a singleton running on the UI image, this is a bad idea.
    /// </summary>
    public class UpdateUiActor : ReceiveActor
    {
        public UpdateUiActor(IHubContext<PersonsHub> personsHubContext)
        {
            Program.Mediator.Tell(new Subscribe("updateUi", Self));

            ReceiveAsync<IUpdateUiMessage>(async message =>
                {
                    Log.Information($"updateUi received person: (FirstName {message.Person.FirstName}, LastName {message.Person.LastName}).");
                    // Receive a UI update message and tell all clients of PersonsHub of a new person added.
                    await personsHubContext.Clients.All.InvokeAsync("personUpdate", message.Person);
                });
        }
    }
}
