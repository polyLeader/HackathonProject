using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Web;
using System.Web.Mvc;
using BusinessLogic.Core;
using PolyTeam.Hackaton.Models;
using BusinessLogic.Domain;

namespace PolyTeam.Hackaton.Controllers
{
    //[Authorize(Roles = "Deputy")]
    public class RequestController : Controller
    {

        private readonly IProblemRepository problemRepository;
        private readonly ISocialRequestRepository socialRequestRepository;
        private readonly IUserProcessor userProcessor;

        public RequestController(IProblemRepository repository, ISocialRequestRepository socialRequestRepository, IUserProcessor userProcessor)
        {
            this.problemRepository = repository;
            this.socialRequestRepository = socialRequestRepository;
            this.userProcessor = userProcessor;
        }
        //
        // GET: /Request/

        public ActionResult Index()
        {            
             var model = new SocialRequestModel();

            var newList = new List<SelectListItem>();
            var list = problemRepository.GetAll();

            foreach (var currentProblem in list)
            {
                newList.Add(new SelectListItem
                                          {
                                              Value = Convert.ToString(currentProblem.Id),
                                              Text = currentProblem.Name
                                          });
            }

            model.ProblemList = newList;
            return View("Index", model);

        }
        public ActionResult Submit(SocialRequestModel request)
        {
            var user = userProcessor.GetUserByName(User.Identity.Name);

            var domain = new SocialRequest
                             {
                                 Problem = this.problemRepository.GetById(request.ProblemId),
                                 Flat = request.Flat,
                                 House = request.House,
                                 User = user,
                                 Street = request.Street
                             };

            this.socialRequestRepository.Add(domain);

            return RedirectToAction("Index", "Statistics");
        }

        [Authorize(Roles = "Deputy")]
        public void SetDeputyToProblem(string someProblem)
        {
            var deputy = userProcessor.GetUserByName(User.Identity.Name);

            var problem = problemRepository.GetProblemByName(someProblem);

            var social = new SocialRequest()
                             {
                                 Deputy = deputy,
                                 Problem = problem
                             };

        }

    }
}