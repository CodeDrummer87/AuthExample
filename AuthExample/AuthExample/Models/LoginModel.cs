using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AuthExample.Models
{
    public class LoginModel
    {
        [Key]
        public int LoginId { get; set; }

        [Required]
        public string Email { get; set; }
        
        [Required]
        public string Password { get; set; }
        public byte[] Salt { get; set; }
    }
}
