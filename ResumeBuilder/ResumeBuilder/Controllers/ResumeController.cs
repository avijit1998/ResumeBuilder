using ResumeBuilder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ResumeBuilder.Controllers
{
    public class ResumeController : Controller
    {
        // GET: Resume
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Form(User user)
        {
            if (Session["UserID"] != null)
                return View(user);

            return RedirectToAction("Login","Account");
        }

        public ActionResult Preview()
        {
            return PartialView("~/Views/PartialViews/PreviewPartial.cshtml");
        }
    }
}