using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using BusinessLogic.Domain;

namespace BusinessLogic.Core
{
    public class SocialRequestRepository:ISocialRequestRepository
    {
        private readonly DatabaseContext _databaseContext;

        public SocialRequestRepository(DatabaseContext databaseContext)
        {
            this._databaseContext = databaseContext;
        }

        public SocialRequest Add(SocialRequest socialRequest)
        {
            this._databaseContext.SocialRequests.Add(socialRequest);
            return socialRequest;
        }

        public SocialRequest Update(SocialRequest socialRequest)
        {
            this._databaseContext.Entry(socialRequest).State = EntityState.Modified;
            return socialRequest;
        }

        public void Delete(SocialRequest socialRequest)
        {
            this._databaseContext.Entry(socialRequest).State = EntityState.Deleted; 
        }

        public IList<SocialRequest> GetAll()
        {
            return this._databaseContext.SocialRequests.ToArray();
        }

        public IList<SocialRequest> GetByStreet(string street)
        {
            return this._databaseContext.SocialRequests.Where(x => x.Street == street).ToArray();
        }

        public IList<SocialRequest> GetByUser(User user)
        {
            return this._databaseContext.SocialRequests.Where(x => x.User == user).ToArray();
        }

        public IList<SocialRequest> GetByProblem(Problem problem)
        {
            return this._databaseContext.SocialRequests.Where(x => x.Problem == problem).ToArray();
        }

        public IList<SocialRequest> GetAllNotDone()
        {
            return this._databaseContext.SocialRequests.Where(x => x.Done == null).ToArray();
        }

        public IList<SocialRequest> GetAllDone()
        {
            return this._databaseContext.SocialRequests.Where(x => x.Done == true).ToArray();
        }

        public IList<SocialRequest> GetAllDoneByParty(string party)
        {
            return this._databaseContext.SocialRequests.Where(x => (x.Done == true && x.User.Party == party)).ToArray();
        }

        public IList<SocialRequest> GetAllInProcessByParty(string party)
        {
            return this._databaseContext.SocialRequests.Where(x => (x.Done == false && x.User.Party == party)).ToArray();
        }

        public int CounterAllRequests()
        {
            return this._databaseContext.SocialRequests.ToArray().Count();
        }

        public int CounterAllDoneRequests()
        {
            return this._databaseContext.SocialRequests.Where(x => x.Done == true).ToArray().Count();
        }

        public int ConterAllInProcessRequests()
        {
            return this._databaseContext.SocialRequests.Where(x => x.Done == false).ToArray().Count();
        }

        public int CounterAllNotInProcessRequests()
        {
            return this._databaseContext.SocialRequests.Where(x => x.Done == null).ToArray().Count();
        }

        public int CounterAllDoneRequestsByParty(string party)
        {
            return this._databaseContext.SocialRequests.Where(x => (x.Done == true && x.User.Party == party)).ToArray().Count();
        }

        public int CounterAllInprocessRequestsByParty(string party)
        {
            return this._databaseContext.SocialRequests.Where(x => (x.Done == false && x.User.Party == party)).ToArray().Count();
        }
    }
}
