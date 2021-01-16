using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AuthExample.Models
{
    public class SessionModel
    {
        [Key]
        public int Id { get; set; }
        public string SessionId { get; set; }
        public int UserId { get; set; }
        public DateTime Created { get; set; }
        public DateTime Expired { get; set; }
    }
}
