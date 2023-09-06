using Entities.Interface;

namespace Entities.NamesSa
{
    public class SAPutNameRequest: BaseName, IId
    {
        public int Id { get; set; }
    }
}
