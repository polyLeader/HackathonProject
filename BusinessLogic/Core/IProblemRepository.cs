using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessLogic.Domain;

namespace BusinessLogic.Core
{
    public interface IProblemRepository
    {
        Problem Add(Problem problem);
        Problem Update(Problem problem);
        void Delete(Problem problem);
        IList<Problem> GetAll();
        Problem GetById(int id);
        Problem GetProblemByName(string someProblem);
        int GetProblemIdByName(string someProblem);
    }
}
