using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class BaseName
    {
        [Required]
        [MaxLength(30, ErrorMessage = "max length is 30")]
        public string? FirstName { get; set; }

        [Required]
        [MaxLength(30, ErrorMessage = "max length is 30")]
        public string? LastName { get; set; }

        public string? NickName { get; set; }
    }
}
