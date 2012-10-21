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
        IList<SocialRequest> GetByStreetId(int streetId);
        IList<SocialRequest> GetByUserId(int userId);
        IList<SocialRequest> GetByProblemId(int problemId);
        IList<SocialRequest> GetAllNotDone();
        IList<SocialRequest> GetAllDone();
        IList<SocialRequest> GetAllDoneByParty(string party);
        IList<SocialRequest> GetAllInProcessByParty(string party);
        int CounterAllRequests();
        int CounterAllDoneRequests();
        int CounterAllInProcessRequests();
        int CounterAllNotInProcessRequests();
        int CounterAllDoneRequestsByParty(string party);
        int CounterAllInprocessRequestsByParty(string party);
    }
}
