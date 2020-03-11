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
        
        public ActionResult Form()

        {
            //var summary = user.Summary;
            if (Session["UserID"] != null)
            {
               
                int id;
                var re = Int32.TryParse(Session["UserID"] as String, out id);
                var user = dbContext.Users.Where(m => m.UserID == id).FirstOrDefault();
                return View(user);                               
            }

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