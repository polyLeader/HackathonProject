using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;

namespace BusinessLogic.Domain
{
    public class DatabaseContext:DbContext
    {
        public DbSet<Problem> Problems { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<SocialRequest> SocialRequests { get; set; }
        public DbSet<Street> Streets { get; set; }
        public DbSet<Roles> Roles { get; set; }
    }
}
