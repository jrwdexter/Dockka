using Dockka.Data.Messages.Base;
using Dockka.Data.Models;

namespace Dockka.Data.Messages
{
    public class AddPersonMessage : IAddPersonEvent
    {
        public Person Person { get; set; }
    }
}