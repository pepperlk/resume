using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LP_Resume.Controllers
{
    public class HomeController : Controller
    {
        private Repos.IResumeRepo _resRepo;
        public HomeController(Repos.IResumeRepo resRepo)
        {
            _resRepo = resRepo;
        }
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View(_resRepo.GetResume());
        }
    }
}
