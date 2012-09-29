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
            var problem = new Problem {Name = "Водопровод"};
            context.Problems.Add(problem);
            problem = new Problem { Name = "Газопровод" };
            context.Problems.Add(problem);
            problem = new Problem { Name = "Канализация" };
            context.Problems.Add(problem);
            problem = new Problem { Name = "Кровля" };
            context.Problems.Add(problem);
            problem = new Problem { Name = "Електроснабжение" };
            context.Problems.Add(problem);
            problem = new Problem { Name = "Предаварийное состояние здания" };
            context.Problems.Add(problem);
            problem = new Problem { Name = "Лифт" };
            context.Problems.Add(problem);
            base.Seed(context);
        }
    }
}
