using Entities.Interface;

namespace Entities.Request
{
    public class UpdateNameReq : BaseName,  INameConvert, IId
    {
        public int Id { get; set; }

        public Name ConvertToName()
        {
            return new Name()
            {
                FirstName = FirstName,
                LastName = LastName,
                NickName = NickName,
                Id = Id,
            };
        }
    }
}
