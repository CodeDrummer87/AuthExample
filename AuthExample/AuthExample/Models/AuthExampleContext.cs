using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthExample.Models
{
    public class AuthExampleContext : DbContext
    {
        public DbSet<LoginModel> AuthData { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<SessionModel> Sessions { get; set; }

        public AuthExampleContext(DbContextOptions<AuthExampleContext> options) : base(options)
        { }
    }
}
