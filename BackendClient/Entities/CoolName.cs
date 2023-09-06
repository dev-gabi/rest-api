using Entities.Interface;

namespace Entities
{
    public class CoolName : Name, IId
    {
        public string? Cool { get; set; }
        public int Id { get; set ; }

        public CoolName(int id, string? nickName, string? firstName, string? lastName, int? personId)
        {
            if (!string.IsNullOrWhiteSpace(nickName))
            {
                Cool = $"Cool {nickName}";
            }
            else
            {
                Cool = "Not cool! no nickname was provided";
            }
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            NickName = nickName;
            PersonId = personId;
        }
    }
}
