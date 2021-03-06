﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using BusinessLogic.Domain;
using BusinessLogic.Core;
using ParseHelpers;

namespace BusinessLogic.Core
{
    public class DatabaseInitializer : DropCreateDatabaseIfModelChanges<DatabaseContext>
    {
        private readonly ICryptoProvider _cryptoProvider;
        public DatabaseInitializer(ICryptoProvider cryptoProvider)
        {
            _cryptoProvider = cryptoProvider;
        }

        public DateTime RandomDay()
        {
            var start = new DateTime(2000, 1, 1);
            var gen = new Random();

            var range = ((TimeSpan)(DateTime.Today - start)).Days;
            return start.AddDays(gen.Next(range));
        }

        protected override void Seed(DatabaseContext context)
        {
            var problem = new Problem {Name = "Водопровід"};
            context.Problems.Add(problem);
            problem = new Problem { Name = "Газопровід" };
            context.Problems.Add(problem);
            problem = new Problem { Name = "Каналізація" };
            context.Problems.Add(problem);
            problem = new Problem { Name = "Покрівля" };
            context.Problems.Add(problem);
            problem = new Problem { Name = "Електропостачання" };
            context.Problems.Add(problem);
            problem = new Problem { Name = "Предаварійний стан будівлі" };
            context.Problems.Add(problem);
            problem = new Problem { Name = "Ліфт" };
            context.Problems.Add(problem);

            var streets = Parser.GetStreets(null); // Get streets from Dropbox server

            foreach (var street in streets)
            {
                context.Streets.Add(new Street{Lang = street.Lang, Name = street.Name});
            }

            var user = new User();

            var deputies = Parser.GetDeputies(null); // Get deputies from Dropbox server

            foreach (var deputy in deputies)
            {
                user.Street = null;
                user.House = null;
                user.Flat = null;
                user.FirstName = deputy.FirstName;
                user.LastName = deputy.LastName;
                user.SecondName = deputy.SecondName;
                user.Party = deputy.Party;
                user.RoleId = 1;
                user.Hash = _cryptoProvider.EncryptString(_cryptoProvider.GenerateCode(8));
                user.Login = _cryptoProvider.GenerateDeputyLogin(deputy.FirstName, deputy.LastName);

                context.Users.Add(user);
            }

            var role = new Roles {Id = 1, Name = "Deputy"};
            context.Roles.Add(role);

            role = new Roles {Id = 2, Name = "User"};
            context.Roles.Add(role);

            context.SaveChanges();

            // TODO Must be deleted - begin
            var deput = new User
                            {
                                FirstName = "Dima",
                                Flat = null,
                                House = null,
                                Street = null,
                                LastName = "Beseda",
                                Login = "Dimka",
                                RoleId = 1,
                                SecondName = "Gennadievich",
                                Party = "Партия регионов",
                                Hash = _cryptoProvider.CreateHash("12345678"),
                            };

            context.Users.Add(deput);

            context.SaveChanges();
            
            var random = new Random();

            for (var i = 0; i < 100; i++ )
            {
                var rand = random.Next(0, context.Streets.Count() - 1);

                var street = context.Streets.FirstOrDefault(x => x.Id == rand).Name;

                user.Street = street;
                user.House = null;
                user.Flat = null;
                user.FirstName = _cryptoProvider.GenerateCode(10);
                user.LastName = _cryptoProvider.GenerateCode(8);
                user.SecondName = _cryptoProvider.GenerateCode(9);
                user.Party = null;
                user.RoleId = 2;
                user.Hash = _cryptoProvider.EncryptString(_cryptoProvider.GenerateCode(8));
                user.Login = _cryptoProvider.GenerateCode(6);
                context.Users.Add(user);
            }

            context.SaveChanges();

            for (var i = 0; i < 200; i++)
            {
                var social = new SocialRequest();

                if (i%3 == 0) social.Done = true;
                else if (i%2 == 0) social.Done = false;
                else social.Done = null;

                social.House = random.Next(0, 60).ToString();

                social.StreetId = random.Next(0, context.Streets.Count() - 1);

                social.ProblemId = random.Next(0, context.Problems.Count() - 1);

                var allDeputies = context.Users.Where(x => x.RoleId == 1).ToList();

                social.DeputyId = allDeputies[random.Next(0, allDeputies.Count - 1)].Id;

                social.CreatingDate = RandomDay();
                if (social.Done == true) social.FinishDate = social.CreatingDate.AddDays(random.Next(0, 40));

                var allUsers = context.Users.Where(x => x.RoleId == 2).ToList();

                social.UserId = allDeputies[random.Next(0, allUsers.Count - 1)].Id;

                context.SocialRequests.Add(social);
            }

            context.SaveChanges();

             // TODO Must be deleted - end

            base.Seed(context); // Don't delete
        }
    }
}
