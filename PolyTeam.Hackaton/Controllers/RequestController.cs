using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLogic.Core;
using PolyTeam.Hackaton.Models;
using BusinessLogic.Domain;

namespace PolyTeam.Hackaton.Controllers
{
    public class RequestController : Controller
    {

        private readonly ProblemRepository problemRepository;
        private readonly SocialRequestRepository socialRequestRepository;

        public RequestController(ProblemRepository repository, SocialRequestRepository socialRequestRepository)
        {
            this.problemRepository = repository;
            this.socialRequestRepository = socialRequestRepository;
        }
        //
        // GET: /Request/

        public ActionResult Index()
        {
            var model = new SocialRequestModel();

            return View(model);
        }

        public ActionResult Submit(SocialRequestModel request)
        {
            var domain = new SocialRequest();
            domain.Problem = this.problemRepository.GetById(request.ProblemId);
            domain.Flat = request.Flat;
            domain.House = request.House;
            //domain.User = request.User;
            domain.Street = request.Street;
            this.socialRequestRepository.Add(domain);
            return View("Index");
        }
    }
}
