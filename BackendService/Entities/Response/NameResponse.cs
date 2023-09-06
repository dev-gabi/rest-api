using Entities.Interface;

namespace Entities.Response
{
    public class NameResponse : BaseName, IId, IPersonId
    {
        public int Id { get ; set ; }
        public int? PersonId { get ; set ; }
    }
}
