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
        private ResumeBuilderConnection db;
        public ResumeController()
        {
            db = new ResumeBuilderConnection();
        }
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

        [HttpPost]
        public ActionResult SaveBasicInformation(User user)
        {
            var userFromDb=db.Users.FirstOrDefault(u=>u.UserID==user.UserID);
            userFromDb.Summary = user.Summary;
            db.SaveChanges();
            string message = "SUCCESS";
            return Json(new { Message = message, JsonRequestBehavior.AllowGet });
        }
    }
}