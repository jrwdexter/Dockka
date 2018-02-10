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
    public class UpdateUiActor : ReceiveActor
    {
        public UpdateUiActor(IHubContext<PersonsHub> personsHubContext)
        {
            ReceiveAsync<IUpdateUiMessage>(async message =>
                {
                    await personsHubContext.Clients.All.InvokeAsync("personUpdate", message.Person);
                });
        }
    }
}
