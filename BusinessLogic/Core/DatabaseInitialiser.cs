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
            base.Seed(context);
        }
    }
}
