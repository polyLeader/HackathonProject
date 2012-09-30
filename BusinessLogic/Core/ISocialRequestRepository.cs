using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessLogic.Domain;

namespace BusinessLogic.Core
{
    public interface ISocialRequestRepository
    {
        SocialRequest Add(SocialRequest socialRequest);
        SocialRequest Update(SocialRequest socialRequest);
        void Delete(SocialRequest socialRequest);
        IList<SocialRequest> GetAll();
        IList<SocialRequest> GetByStreet(string street);
        IList<SocialRequest> GetByUser(User user);
        IList<SocialRequest> GetByProblem(Problem problem);
        IList<SocialRequest> GetAllNotDone();
        IList<SocialRequest> GetAllDone();
        IList<SocialRequest> GetAllDoneByParty(string party);
        IList<SocialRequest> GetAllInProcessByParty(string party);
    }
}
