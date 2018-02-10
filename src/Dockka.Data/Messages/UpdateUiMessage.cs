using Dockka.Data.Messages.Base;
using Dockka.Data.Models;

namespace Dockka.Data.Messages
{
    public class UpdateUiMessage : IUpdateUiMessage
    {
        public Person Person { get; set; }
    }
}