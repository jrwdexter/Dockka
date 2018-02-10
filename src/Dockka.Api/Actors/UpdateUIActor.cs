using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Akka.Actor;
using Dockka.Api.Hubs;
using Dockka.Data.Messages;
using Dockka.Data.Messages.Base;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Sockets;

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
            ReceiveAsync<IUpdateUiMessage>(async message =>
                {
                    // Receive a UI update message and tell all clients of PersonsHub of a new person added.
                    await personsHubContext.Clients.All.InvokeAsync("personUpdate", message.Person);
                });
        }
    }
}
