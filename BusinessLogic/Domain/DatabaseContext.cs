using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using BusinessLogic.domen;

namespace BusinessLogic.Domain
{
    public class DatabaseContext:DbContext
    {
        public DbSet<Problem> Problems { get; set; }
        public DbSet<Requster> Requsters { get; set; }
        public DbSet<Deputy> Deputies { get; set; }
        public DbSet<SocialRequest> SocialRequests { get; set; }
    }
}
