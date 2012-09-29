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

        private readonly ProblemRepository repository;

        public RequestController (ProblemRepository repository)
        {
            this.repository = repository;
        } 

        public ActionResult Submit(SocialRequestModel request)
        {
            var domain = new SocialRequest();
           // this.repository = ProblemRepository.GetById(request.ProblemId);
            domain.Flat = request.Flat;
            domain.House = request.House;
            //domain.User = request.User;
            //domain.Problem = 
            return View();

        }
    }
}
