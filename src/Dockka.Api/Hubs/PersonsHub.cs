using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dockka.Data.Messages;
using Dockka.Data.Models;
using Microsoft.AspNetCore.SignalR;

namespace Dockka.Api.Hubs
{
    public class PersonsHub : Hub
    {
        public void AddPerson(Person person)
        {
            var addActor =
                Program.AkkaSystem.ActorSelection("akka.tcp://dockka-system@dockka.api:9000/user/addPerson");
            addActor.Tell(new AddPersonMessage { Person = person });
        }
    }
}
