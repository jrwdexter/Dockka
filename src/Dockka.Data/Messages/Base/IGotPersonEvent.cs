using Dockka.Data.Models;

namespace Dockka.Data.Messages.Base
{
    public interface IUpdateUiMessage
    {
        Person Person { get; set; }
    }
}