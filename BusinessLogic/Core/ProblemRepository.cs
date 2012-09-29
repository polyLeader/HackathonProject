using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using BusinessLogic.Domain;
using BusinessLogic.domen;

namespace BusinessLogic.Core
{
    public class ProblemRepository:IProblemRepository
    {
        private readonly DatabaseContext _databaseContext;

        public ProblemRepository(DatabaseContext databaseContext)
        {
            this._databaseContext = databaseContext;
        }

        public Problem Add(Problem problem)
        {
            this._databaseContext.Problems.Add(problem);
            this._databaseContext.SaveChanges();
            return problem;
        }

        public Problem Update(Problem problem)
        {
            this._databaseContext.Entry(problem).State = EntityState.Modified;
            return problem;
        }

        public void Delete(Problem problem)
        {
            this._databaseContext.Entry(problem).State = EntityState.Deleted;
        }

        public IList<Problem> GetAll()
        {
            return this._databaseContext.Problems.ToArray();
        }
    }
}
