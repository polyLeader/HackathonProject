using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLogic.Core;
using PolyTeam.Hackaton.Models;

namespace PolyTeam.Hackaton.Controllers
{
    public class StatisticsController : Controller
    {
        public struct AllProblemsCount
        {
            public string Name;
            public int Count;
        }

        //
        // GET: /Statistics/
        private readonly IProblemRepository _problemRepository;
        private readonly ISocialRequestRepository _socialRequestRepository;
        private readonly IUserProcessor _userProcessor;
        private readonly IStreetRepository _streetRepository;
        private readonly IUserRepository _userRepository;

        public StatisticsController(IProblemRepository repository, ISocialRequestRepository socialRequestRepository, IUserProcessor userProcessor,IUserRepository userRepository)
        {
            this._problemRepository = repository;
            this._socialRequestRepository = socialRequestRepository;
            this._userProcessor = userProcessor;
            this._userRepository = userRepository;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult NotDoneOrDone(bool done)
        {
            var list = _socialRequestRepository.GetAllNotDoneOrDone(done);
            
            if (!done)
                return this.Json(list.Select(socialRequestModel => new SocialRequestModel { Flat = socialRequestModel.Flat, House = socialRequestModel.House, Street = _streetRepository.GetById(socialRequestModel.StreetId).Name }).ToList(), JsonRequestBehavior.AllowGet);
            return Json(list.Select(socialRequestModel => new SocialRequestModel
            {
                Flat = socialRequestModel.Flat,
                House = socialRequestModel.House,
                Street = _streetRepository.GetById(socialRequestModel.StreetId).Name,
                Deputy = new DeputyModel
                {
                    FirstName = _userProcessor.GetUserById((int)socialRequestModel.DeputyId).FirstName,
                    LastName = _userProcessor.GetUserById((int)socialRequestModel.DeputyId).LastName,
                    Party = _userProcessor.GetUserById((int)socialRequestModel.DeputyId).Party
                }
            }).ToList(), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult AllProblemsStat()
        {
            var problemRepo = _problemRepository.GetAll();
            var socialRepo = _socialRequestRepository.GetAll();

            var problemsList = (from problem in problemRepo let counter = socialRepo.Count(socialRequest => problem.Id == socialRequest.ProblemId) select new AllProblemsCount {Name = problem.Name, Count = counter}).ToList();
            
            return Json(problemsList, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public  JsonResult DoneOrInProcessByParty(string party,bool done)
        {
            var list = _socialRequestRepository.GetAllDoneOrInProcessByParty(party,done);
            return
                Json(
                    list.Select(
                        socialRequestModel =>
                        new SocialRequestModel
                            {
                                Flat = socialRequestModel.Flat,
                                House = socialRequestModel.House,
                                Street = _streetRepository.GetNameById(socialRequestModel.StreetId),
                                Deputy =
                                    new DeputyModel
                                        {
                                            FirstName =
                                                _userProcessor.GetUserById((int) socialRequestModel.DeputyId).FirstName,
                                            LastName =
                                                _userProcessor.GetUserById((int) socialRequestModel.DeputyId).LastName,
                                            Party = _userProcessor.GetUserById((int) socialRequestModel.DeputyId).Party
                                        }
                            }).ToList(),
                    JsonRequestBehavior.AllowGet);
        }

        public JsonResult AllStatByParty()
        {
            var listUser = _userRepository.GetAll();
            var listParty = new List<string>();
            foreach (var tParty in listUser)
            {
                if (tParty.RoleId == 1)// депутат
                
                    if (!listParty.Contains(tParty.Party))
                        listParty.Add(tParty.Party);
            }

            return
                Json(listParty.Select(party => new PartyModel{Name = party,Done = _socialRequestRepository.CounterAllDoneOrInprocessRequestsByParty(party,true),InProcess = _socialRequestRepository.CounterAllDoneOrInprocessRequestsByParty(party,false)}).ToList(),JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult AllProblems()
        {

            return Json(_socialRequestRepository.GetAll(), JsonRequestBehavior.AllowGet);
        }
    }
}
