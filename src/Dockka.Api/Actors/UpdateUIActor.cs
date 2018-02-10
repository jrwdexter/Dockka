using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Akka.Actor;

namespace Dockka.Api.Actors
{
    public class UpdateUiActor : ReceiveActor
    {
        public UpdateUiActor()
        {
            OnReceive<();
        }
    }
}
