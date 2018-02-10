using Dockka.Data.Models;

namespace Dockka.Data.Messages.Base
{
    public interface IAddPersonMessage
    {
        Person Person { get; set; }
    }
}