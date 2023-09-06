using Entities.Interface;
using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class Person : IId
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public virtual Name? Name { get; set; }

        [Required]
        public int? Age { get; set; }
    }
}
