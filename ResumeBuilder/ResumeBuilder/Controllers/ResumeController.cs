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

                ViewBag.Courses = db.Courses.ToList();

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

        [HttpPost]
        public ActionResult SaveWorkExperience(WorkExperience workExperience)
        {
            db.WorkExperiences.Add(workExperience);
            db.SaveChanges();

            string message = "SUCCESS";

            return Json(new { Message = message, JsonRequestBehavior.AllowGet });
        }

        [HttpPost]
        public ActionResult SaveEducationalDetails(EducationalDetails educationalDetails)
        {
            db.EducationalDetails.Add(educationalDetails);
            db.SaveChanges();

            string message = "SUCCESS";

            return Json(new { Message = message, JsonRequestBehavior.AllowGet });
        }

        public ActionResult LogOff()
        {
            if (Session["UserID"] != null)
            {
                Session.Remove("UserID");
                Session.RemoveAll();
            }

            return RedirectToAction("Login", "Account");
        }

        public JsonResult GetSkill(string term)
        {
            List<string> skills;

            skills = db.Skills.Where(x => x.Skill.StartsWith(term)).Select(y => y.Skill).ToList();

            return Json(skills, JsonRequestBehavior.AllowGet);
        }
    }
}