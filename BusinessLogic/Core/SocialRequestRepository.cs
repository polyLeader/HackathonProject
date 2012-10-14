﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
﻿using System.Web.Mvc;
﻿using BusinessLogic.Domain;
using BusinessLogic.Core;
using System.Web;

namespace BusinessLogic.Core
{
    public class SocialRequestRepository : ISocialRequestRepository
    {
        private readonly DatabaseContext _databaseContext;
        private readonly IUserProcessor _userProcessor;

        public SocialRequestRepository(DatabaseContext databaseContext, IUserProcessor userProcessor)
        {
            _databaseContext = databaseContext;
            _userProcessor = userProcessor;
        }

        public SocialRequest Add(SocialRequest socialRequest)
        {
            _databaseContext.SocialRequests.Add(socialRequest);
            return socialRequest;
        }

        public SocialRequest Update(SocialRequest socialRequest)
        {
            _databaseContext.Entry(socialRequest).State = EntityState.Modified;
            return socialRequest;
        }

        public void Delete(SocialRequest socialRequest)
        {
            _databaseContext.Entry(socialRequest).State = EntityState.Deleted; 
        }

        public IList<SocialRequest> GetAll()
        {
            return _databaseContext.SocialRequests.ToArray();
        }

        public IList<SocialRequest> GetById(int Id,string typeId)
        {
            if (typeId == "Street")
                return _databaseContext.SocialRequests.Where(x => x.StreetId == Id).ToArray();
            if (typeId == "User")
                return _databaseContext.SocialRequests.Where(x => x.UserId == Id).ToArray();
            return _databaseContext.SocialRequests.Where(x => x.ProblemId == Id).ToArray();
        }

        public IList<SocialRequest> GetAllNotDoneOrDone(bool done)
        {
            if (done)
                return _databaseContext.SocialRequests.Where(x => x.Done == true).ToArray();
            return _databaseContext.SocialRequests.Where(x => x.Done == false).ToArray();   
        }

        public IList<SocialRequest> GetAllDoneOrInProcessByParty(string party, bool done)
        {
            // Шукаем всех депутов с заданой партией
            var deputies = _databaseContext.Users.Where(x => (x.Party == party && x.RoleId == 1));

            var all = new List<SocialRequest>();

            // Выбираются все запросы, где указан депутат с запрошенной партией
            // и помещается в окончательный список
            if (!done)
            foreach (var list in deputies.Select(deputy => _databaseContext.SocialRequests.Where(x => (x.Done == false && x.DeputyId == deputy.Id)).ToList()))
            {
                all.AddRange(list);
            }
            else
            {
                foreach (var list in deputies.Select(deputy => _databaseContext.SocialRequests.Where(x => (x.Done == true && x.DeputyId == deputy.Id)).ToList()))
                {
                    all.AddRange(list);
                }

            }

            return all;
        }

        public int CounterAllRequests()
        {
            return _databaseContext.SocialRequests.Count();
        }

        public int CounterAllDoneNoteInProcessRequests(bool? done)
        {   if (done == true)
                return _databaseContext.SocialRequests.Where(x => x.Done == true).ToArray().Count();
            if (done == false)
                return _databaseContext.SocialRequests.Where(x => x.Done == false).ToArray().Count();
            return _databaseContext.SocialRequests.Where(x => x.Done == null).ToArray().Count();
        }

        public int CounterAllDoneOrInprocessRequestsByParty(string party, bool done)
        {
            return this.GetAllDoneOrInProcessByParty(party,done).Count;
        }

    }
}