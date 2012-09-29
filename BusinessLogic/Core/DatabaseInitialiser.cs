using System;
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
    public class DatabaseInitialiser:DropCreateDatabaseIfModelChanges<DatabaseContext>
    {
        private readonly ICryptoProvider _cryptoProvider;
        public DatabaseInitialiser(ICryptoProvider cryptoProvider)
        {
            _cryptoProvider = cryptoProvider;
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
            context.Users.Add(new User { FirstName = "asdf", Password = "7815696ecbf1c96e6894b779456d330e" });
            context.SaveChanges();

            var street = new Street();

            var streets = Parser.GetStreets("donetsk.osm");

            foreach (var street1 in streets)
            {
                street.Lang = street1.Lang;
                street.Name = street1.Name;
                context.Streets.Add(street);
            }

            var user = new User();

            var deputies = Parser.GetDeputies("deputies.list");

            foreach (var deputy in deputies)
            {
                user.Street = null;
                user.House = null;
                user.Flat = null;
                user.FirstName = deputy.FirstName;
                user.LastName = deputy.LastName;
                user.SecondName = deputy.SecondName;
                user.Party = deputy.Party;
                user.Password = _cryptoProvider.GenerateCode(8);
                user.Login = Parser.GenerateDeputyLogin(deputy.FirstName, deputy.LastName);

                context.Users.Add(user);
            }

            context.SaveChanges();

            base.Seed(context);
        }
    }
}
