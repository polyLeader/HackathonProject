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
        //
        // GET: /Statistics/
        private readonly IProblemRepository problemRepository;
        private readonly ISocialRequestRepository socialRequestRepository;
        private readonly IUserProcessor userProcessor;

        public StatisticsController(IProblemRepository repository, ISocialRequestRepository socialRequestRepository, IUserProcessor userProcessor)
        {
            this.problemRepository = repository;
            this.socialRequestRepository = socialRequestRepository;
            this.userProcessor = userProcessor;
        }

        public ActionResult Index()
        {

            return View();
        }

        public void NotDone()
        {
            var list = socialRequestRepository.GetAllNotDone();
            IList < SocialRequestModel > social= list.Select(socialRequestModel => new SocialRequestModel {Flat = socialRequestModel.Flat, House = socialRequestModel.House, Street = socialRequestModel.Street}).ToList();
        }

        public void Done()
        {
            var list = socialRequestRepository.GetAllDone();
            //var deputy = 
            IList<SocialRequestModel> social = list.Select(socialRequestModel => new SocialRequestModel {Flat = socialRequestModel.Flat, House = socialRequestModel.House, Street = socialRequestModel.Street,
                                                                                                         Deputy = new DeputyModel { Name = userProcessor.GetUserByName(socialRequestModel.Deputy.FirstName).FirstName ,
                                                                                                                                    LastName = userProcessor.GetUserByName(socialRequestModel.Deputy.FirstName).LastName,
                                                                                                                                    Party = userProcessor.GetUserByName(socialRequestModel.Deputy.FirstName).Party
                                                                                                         }
            }).ToList();
        }

        public void InProcessByParty(string party)
        {
            var list = socialRequestRepository.GetAllInProcessByParty(party);
            IList<SocialRequestModel> social = list.Select(socialRequestModel => new SocialRequestModel
            {
                Flat = socialRequestModel.Flat,
                House = socialRequestModel.House,
                Street = socialRequestModel.Street,
                Deputy = new DeputyModel
                {
                    Name = userProcessor.GetUserByName(socialRequestModel.Deputy.FirstName).FirstName,
                    LastName = userProcessor.GetUserByName(socialRequestModel.Deputy.FirstName).LastName,
                    Party = userProcessor.GetUserByName(socialRequestModel.Deputy.FirstName).Party
                }
            }).ToList();
        }

        public void DoneByParty(string party)
        {
            var list = socialRequestRepository.GetAllDoneByParty(party);
            IList<SocialRequestModel> social = list.Select(socialRequestModel => new SocialRequestModel
            {
                Flat = socialRequestModel.Flat,
                House = socialRequestModel.House,
                Street = socialRequestModel.Street,
                Deputy = new DeputyModel
                {
                    Name = userProcessor.GetUserByName(socialRequestModel.Deputy.FirstName).FirstName,
                    LastName = userProcessor.GetUserByName(socialRequestModel.Deputy.FirstName).LastName,
                    Party = userProcessor.GetUserByName(socialRequestModel.Deputy.FirstName).Party
                }
            }).ToList();
        }


    }
}
