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
                var user = db.Users.Where(m => m.UserID == id).FirstOrDefault();
                return View(user);                               
            }
            return RedirectToAction("Login","Account");
        }

        [HttpPost]
        public ActionResult SaveSummary(User user)
        {
            var userFromDb=db.Users.FirstOrDefault(u=>u.UserID==user.UserID);
            
            userFromDb.Summary = user.Summary;
            db.SaveChanges();
            
            string message = "SUCCESS";
            
            return Json(new { Message = message, JsonRequestBehavior.AllowGet });
        }

        [HttpPost]
        public ActionResult SaveBasicInformation(User user)
        {
            var userFromDb = db.Users.FirstOrDefault(u => u.UserID == user.UserID);
            
            userFromDb.Name = user.Name;
            userFromDb.Gender = user.Gender;
            userFromDb.PhoneNumber = user.PhoneNumber;
            userFromDb.DateOfBirth = user.DateOfBirth;

            db.SaveChanges();
            
            string message = "SUCCESS";
            
            return Json(new { Message = message, JsonRequestBehavior.AllowGet });
        }

        [HttpPost]
        public ActionResult SaveProjectDetails(Project project)
        {
            db.Projects.Add(project);
            db.SaveChanges();

            string message = "SUCCESS";

            return Json(new { Message = message, JsonRequestBehavior.AllowGet });
        }
        //public ActionResult SaveBasicInfo(User user)
        //{
        //    try
        //    {
        //        var usr = db.Users.SingleOrDefault(u => u.UserID == user.UserID);
        //        usr.Summary = user.Summary;

        //        //db.Users.Add(user);
        //        db.SaveChanges();
        //        return RedirectToAction("Form", "Resume");
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e);
        //    }
        //    return RedirectToAction("Login", "Account");
        //}
    }
}