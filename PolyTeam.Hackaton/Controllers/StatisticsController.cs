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
            IList < SocialRequestModel > social= new List<SocialRequestModel>();
            var list = socialRequestRepository.GetAllNotDone();
            foreach (var socialRequestModel in list)
            {
                social.Add(new SocialRequestModel{Flat = socialRequestModel.Flat,House = socialRequestModel.House, Street = socialRequestModel.Street});
            }
        }

    }
}
