using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using BusinessLogic.Domain;

namespace BusinessLogic.Core
{
    public class ProblemRepository : IProblemRepository
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
            _databaseContext.Entry(problem).State = EntityState.Deleted;
        }

        public IList<Problem> GetAll()
        {
            return _databaseContext.Problems.ToArray();
        }

        public Problem GetById(int id)
        {
            return _databaseContext.Problems.FirstOrDefault(x => x.Id == id);
        }

        public Problem GetProblemByName(string someProblem)
        {
            return _databaseContext.Problems.ToList().Single(it => it.Name == someProblem);
        }

        public int GetProblemIdByName(string someProblem)
        {
            return this.GetProblemByName(someProblem).Id;
        }
    }
}
