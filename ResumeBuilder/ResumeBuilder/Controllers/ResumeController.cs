using ResumeBuilder.Models;
using ResumeBuilder.ViewModels;
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
        private PublicProfileViewModel _uiModel;
        public ResumeController()
        {
            _uiModel = new PublicProfileViewModel();
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

        public ActionResult Preview()
        {
            return PartialView("~/Views/PartialViews/PreviewPartial.cshtml");
        }

        public ActionResult PreviewUser(int id)
        {
            // User Name
            _uiModel.Name = db.Users.FirstOrDefault(a => a.UserID == id).Name;

            // User Role
            _uiModel.UserRole = "Web Developer";

            // User Phone
            _uiModel.PhoneNumber = db.Users.FirstOrDefault(a => a.UserID == id).PhoneNumber;

            // User E-mail
            _uiModel.Email = db.Users.FirstOrDefault(a => a.UserID == id).Username;

            //User Linkedin Link
            _uiModel.LinkedinLink = "https://www.linkedin.com/user";

            // User Summary
            _uiModel.Summary = "Oh, I misunderstood the problem. ResumeBuilder ResumeBuilder Setting a padding on, ResumeBuilder ResumeBuilder bin the padding won't help you.";

            //Education Details
            ViewBag.education = 1;
            var data = db.Users.Where(m=>m.UserID==id).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
            //return Json("Success", JsonRequestBehavior.AllowGet);

            //return View(_uiModel);
            
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

        [HttpPost]
        public ActionResult SaveWorkExperience(WorkExperience workExperience)
        {
            db.WorkExperiences.Add(workExperience);
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

        public ActionResult LogOff()
        {
            if (Session["UserID"] != null)
            {
                Session.Remove("UserID");
                Session.RemoveAll();
            }

            return RedirectToAction("Login", "Account");
        }
    }
}