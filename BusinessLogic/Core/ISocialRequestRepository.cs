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
        IList<SocialRequest> GetById(int Id, string typeId);
        IList<SocialRequest> GetAllNotDoneOrDone(bool ? done);
        IList<SocialRequest> GetAllDoneOrInProcessByParty(string party,bool done);
        int CounterAllRequests();
        int CounterAllDoneNoteInProcessRequests(bool? done);
        int CounterAllDoneOrInprocessRequestsByParty(string party,bool done);
    }
}
