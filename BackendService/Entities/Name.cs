using Entities.Interface;
using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class Name : BaseName, IId, IPersonId
    {
        [Key]
        public int Id { get; set; }

        public virtual  Person? Person { get; set; }
        public int? PersonId    {get;set;}
    }
}
