using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ResumeBuilder.Controllers
{
    public class PublicProfileController : Controller
    {
        // GET: PublicProfile/Index/id
        public ActionResult Index(int id)
        {
            return View();
        }
    }
}