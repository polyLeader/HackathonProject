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
            return this._databaseContext.SocialRequests.Where(x => x.Done == false).ToArray();
        }

        public IList<SocialRequest> GetAllDone()
        {
            return this._databaseContext.SocialRequests.Where(x => x.Done == true).ToArray();
        }
    }
}