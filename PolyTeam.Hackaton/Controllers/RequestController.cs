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

        private readonly IProblemRepository _problemRepository;
        private readonly ISocialRequestRepository _socialRequestRepository;
        private readonly IUserProcessor _userProcessor;
        private readonly IStreetRepository _streetRepository;

        public RequestController(IProblemRepository repository, ISocialRequestRepository socialRequestRepository, IUserProcessor userProcessor, IStreetRepository streetRepository)
        {
            _problemRepository = repository;
            _socialRequestRepository = socialRequestRepository;
            _userProcessor = userProcessor;
            _streetRepository = streetRepository;
        }
        //
        // GET: /Request/

        public ActionResult Index()
        {            
             var model = new SocialRequestModel();

            var newList = new List<SelectListItem>();
            var list = _problemRepository.GetAll();

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
            var userId = _userProcessor.GetUserIdByName(User.Identity.Name);
            var streetId = _streetRepository.GetIdByName(request.Street);

            var domain = new SocialRequest
                             {
                                 ProblemId = request.ProblemId,
                                 Flat = request.Flat,
                                 House = request.House,
                                 UserId = userId,
                                 StreetId = streetId
                             };

            _socialRequestRepository.Add(domain);

            return RedirectToAction("Index", "Statistics");
        }

        [Authorize(Roles = "Deputy")]
        public void SetDeputyToProblem(string someProblem)
        {
            var deputyId = _userProcessor.GetUserIdByName(User.Identity.Name);

            var problem = _problemRepository.GetProblemByName(someProblem);

            var social = new SocialRequest()
                             {
                                 DeputyId = deputyId,
                                 ProblemId = problem.Id
                             };

        }

    }
}