using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PolyTeam.Hackaton.Models;

namespace PolyTeam.Hackaton.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View(new LogOnModel());
        }
    }
}
