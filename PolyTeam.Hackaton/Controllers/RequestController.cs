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
        //
        // GET: /Request/

        public ActionResult Index()
        {
            return View();
        }

        private readonly ProblemRepository problemRepository;

        public RequestController (ProblemRepository repository)
        {
            this.problemRepository = repository;
        }

        private readonly SocialRequestRepository requestRepository;

        public RequestController(SocialRequestRepository repository)
        {
            this.requestRepository = repository;
        } 


        public ActionResult Submit(SocialRequestModel request)
        {
            var domain = new SocialRequest();
            domain.Problem = this.problemRepository.GetById(request.ProblemId);
            domain.Flat = request.Flat;
            domain.House = request.House;
            //domain.User = request.User;
            domain.Street = request.Street;
            this.requestRepository.Add(domain);
            return View();
        }
    }
}
