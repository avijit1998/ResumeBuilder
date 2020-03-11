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

        //[HttpPost]
        public ActionResult Form(User user)
        {
            //var summary = user.Summary;
            if (Session["UserID"] != null)

                //var usr = getEntryBYid(Session["UserID"]);
                return View(user);

            return RedirectToAction("Login","Account");
        }

        [HttpPost]
        public ActionResult SaveBasicInfo(User user)
        {
            try
            {
                var usr = db.Users.SingleOrDefault(u => u.UserID == user.UserID);
                usr.Summary = user.Summary;

                //db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Form", "Resume");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return RedirectToAction("Login", "Account");
        }
    }
}