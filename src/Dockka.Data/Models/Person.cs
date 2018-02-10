using Dockka.Data.Models.Base;

namespace Dockka.Data.Models
{
    public class Person : ISqlEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
