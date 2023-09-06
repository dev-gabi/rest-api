
using Entities;
using Entities.Interface;
using Entities.NamesSa;

public class PutNameRequest : BaseName
{
    public SAPutNameRequest ConvertToSaPutNameRequest(int id)
    {
        return new SAPutNameRequest()
        {
            Id = id,
            FirstName = FirstName,
            LastName = LastName,
            NickName = NickName
        };
    }
}

