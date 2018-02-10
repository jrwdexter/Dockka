using Dockka.Data.Messages.Base;

namespace Dockka.Data.Messages
{
    public class QueryPersonMessage : IQueryPersonMessage
    {
        public int Id { get; set; }
    }
}