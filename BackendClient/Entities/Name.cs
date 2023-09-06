using Entities.Interface;

namespace Entities
{
    public class Name: BaseName, IId
    {
        public int? PersonId { get; set; }
        public int Id { get ; set; }
    }
}
