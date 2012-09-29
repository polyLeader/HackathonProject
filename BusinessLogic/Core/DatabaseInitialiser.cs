using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using BusinessLogic.Domain;

namespace BusinessLogic.Core
{
    public class DatabaseInitialiser:DropCreateDatabaseIfModelChanges<DatabaseContext>
    {
        protected override void Seed(DatabaseContext context)
        {
            var problem = new Problem {Name = "Kanalison"};
            var user = new User
                           {
                               LastName = "sdf",
                               Name = "Pisun",
                               Flat = 2,
                               Hash = "493945",
                               House = "1",
                               Party = "Udar",
                               Password = "YaSok",
                               PhoneNumber = "+380000000",
                               RoleId = 2,
                               Street = "Gitler"
                           };
            context.Problems.Add(problem);
            context.Users.Add(user);
            context.SocialRequests.Add(new SocialRequest
                                           {Flat = "5", House = "2", Problem = problem, Street = "Her", User = user});
            base.Seed(context);
        }
    }
}
