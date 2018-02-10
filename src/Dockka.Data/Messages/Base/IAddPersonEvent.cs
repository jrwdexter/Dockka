using Dockka.Data.Models;

namespace Dockka.Data.Messages.Base
{
    public interface IAddPersonEvent
    {
        Person Person { get; set; }
    }
}