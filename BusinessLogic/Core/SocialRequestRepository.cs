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

        public IList<SocialRequest> GetByStreetId(int streetId)
        {
            return _databaseContext.SocialRequests.Where(x => x.StreetId == streetId).ToArray();
        }

        public IList<SocialRequest> GetByUserId(int userId)
        {
            return _databaseContext.SocialRequests.Where(x => x.UserId == userId).ToArray();
        }

        public IList<SocialRequest> GetByProblemId(int problemId)
        {
            return _databaseContext.SocialRequests.Where(x => x.ProblemId == problemId).ToArray();
        }

        public IList<SocialRequest> GetAllNotDone()
        {
            return _databaseContext.SocialRequests.Where(x => x.Done == false).ToArray();
        }

        public IList<SocialRequest> GetAllDone()
        {
            return _databaseContext.SocialRequests.Where(x => x.Done == true).ToArray();
        }

        public IList<SocialRequest> GetAllDoneByParty(string party)
        {
            // Шукаем всех депутов с заданой партией
            var deputies = _databaseContext.Users.Where(x => (x.Party == party && x.RoleId == 1));

            var allDone = new List<SocialRequest>();

            // Выбираются все запросы, где указан депутат с запрошенной партией
            // и помещается в окончательный список
            foreach (var list in deputies.Select(deputy => _databaseContext.SocialRequests.Where(x => (x.Done == true && x.DeputyId == deputy.Id)).ToList()))
            {
                allDone.AddRange(list);
            }

            return allDone;
        }

        public IList<SocialRequest> GetAllInProcessByParty(string party)
        {
            // Шукаем всех депутов с заданой партией
            var deputies = _databaseContext.Users.Where(x => (x.Party == party && x.RoleId == 1));

            var allDone = new List<SocialRequest>();

            // Выбираются все запросы, где указан депутат с запрошенной партией
            // и помещается в окончательный список
            foreach (var list in deputies.Select(deputy => _databaseContext.SocialRequests.Where(x => (x.Done == false && x.DeputyId == deputy.Id)).ToList()))
            {
                allDone.AddRange(list);
            }

            return allDone;
        }

        public int CounterAllRequests()
        {
            return _databaseContext.SocialRequests.Count();
        }

        public int CounterAllDoneRequests()
        {
            return _databaseContext.SocialRequests.Where(x => x.Done == true).ToArray().Count();
        }

        public int CounterAllInProcessRequests()
        {
            return _databaseContext.SocialRequests.Where(x => x.Done == false).ToArray().Count();
        }

        public int CounterAllNotInProcessRequests()
        {
            return _databaseContext.SocialRequests.Where(x => x.Done == null).ToArray().Count();
        }

        public int CounterAllDoneRequestsByParty(string party)
        {
            return this.GetAllDoneByParty(party).Count;
        }

        public int CounterAllInprocessRequestsByParty(string party)
        {
            return this.GetAllInProcessByParty(party).Count;
        } 

    }
}