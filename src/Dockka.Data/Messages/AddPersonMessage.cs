using Dockka.Data.Messages.Base;
using Dockka.Data.Models;

namespace Dockka.Data.Messages
{
    public class AddPersonMessage : IAddPersonMessage
    {
        public Person Person { get; set; }
    }
}