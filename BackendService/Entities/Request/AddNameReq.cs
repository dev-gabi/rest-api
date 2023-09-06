using Entities.Interface;

namespace Entities.Request
{
    public class AddNameReq : BaseName, INameConvert
    {
        public Name ConvertToName()
        {
            return new Name()
            {
                FirstName = FirstName,
                LastName = LastName,
                NickName = NickName
            };
        }
    }
}
