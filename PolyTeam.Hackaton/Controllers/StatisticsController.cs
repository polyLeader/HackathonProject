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

        public StatisticsController(IProblemRepository repository, ISocialRequestRepository socialRequestRepository, IUserProcessor userProcessor)
        {
            this._problemRepository = repository;
            this._socialRequestRepository = socialRequestRepository;
            this._userProcessor = userProcessor;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult NotDone()
        {
            var list = _socialRequestRepository.GetAllNotDone();
            
            // TODO Now not work
            //return this.Json(list.Select(socialRequestModel => new SocialRequestModel {Flat = socialRequestModel.Flat, House = socialRequestModel.House, Street = socialRequestModel.StreetId}).ToList(), JsonRequestBehavior.AllowGet);
            return null;
        }

        [HttpGet]
        public JsonResult Done()
        {
            var list = _socialRequestRepository.GetAllDone();
            
            // TODO now not work
            /*return Json(list.Select(socialRequestModel => new SocialRequestModel {Flat = socialRequestModel.Flat, House = socialRequestModel.House, Street = socialRequestModel.Street,
                                                                                                         Deputy = new DeputyModel { Name = userProcessor.GetUserByName(socialRequestModel.Deputy.FirstName).FirstName ,
                                                                                                                                    LastName = userProcessor.GetUserByName(socialRequestModel.Deputy.FirstName).LastName,
                                                                                                                                    Party = userProcessor.GetUserByName(socialRequestModel.Deputy.FirstName).Party
                                                                                                         }
            }).ToList(),JsonRequestBehavior.AllowGet);*/

            return null;
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
        public  JsonResult InProcessByParty(string party)
        {
            var list = _socialRequestRepository.GetAllInProcessByParty(party);
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
                                            Name =
                                                _userProcessor.GetUserById((int) socialRequestModel.DeputyId).FirstName,
                                            LastName =
                                                _userProcessor.GetUserById((int) socialRequestModel.DeputyId).LastName,
                                            Party = _userProcessor.GetUserById((int) socialRequestModel.DeputyId).Party
                                        }
                            }).ToList(),
                    JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult DoneByParty(string party)
        {
            var list = _socialRequestRepository.GetAllDoneByParty(party);
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
                                            Name = _userProcessor.GetUserById((int) socialRequestModel.DeputyId).FirstName,
                                            LastName = _userProcessor.GetUserById((int) socialRequestModel.DeputyId).LastName,
                                            Party = _userProcessor.GetUserById((int) socialRequestModel.DeputyId).Party
                                        }
                            }).ToList(),
                    JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult AllProblems()
        {

            return Json(_socialRequestRepository.GetAll(), JsonRequestBehavior.AllowGet);
        }
    }
}
