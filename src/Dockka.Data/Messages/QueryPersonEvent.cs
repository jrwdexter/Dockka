using Dockka.Data.Messages.Base;

namespace Dockka.Data.Messages
{
    public class QueryPersonEvent : IQueryPersonEvent
    {
        public int Id { get; set; }
    }
}