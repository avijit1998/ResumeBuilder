using ResumeBuilder.Models;
using ResumeBuilder.Models.ViewModels;
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
                ViewBag.Languages = db.Languages.ToList();
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
        //public ActionResult SaveBasicInformation(User user)
        public ActionResult SaveBasicInformation(AddUserViewModel addUserViewModel)
        {
            var userFromDb = db.Users.FirstOrDefault(u => u.UserID == addUserViewModel.UserID);

            userFromDb.Name = addUserViewModel.Name;
            userFromDb.Gender = addUserViewModel.Gender;
            userFromDb.PhoneNumber = addUserViewModel.PhoneNumber;
            userFromDb.DateOfBirth = addUserViewModel.DateOfBirth;

            if (addUserViewModel.LanguageIds.Any())
            {
                var languages = db.Languages.Where(x => addUserViewModel.LanguageIds.Contains(x.LanguageID)).ToList();
                userFromDb.Languages.AddRange(languages);
            }

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
            term = term.Trim();
            List<string> skills;

            skills = db.Skills.Where(x => x.SkillName.StartsWith(term)).Select(y => y.SkillName).ToList();

            return Json(skills, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SaveUserSkills(AddUserSkillsViewModel addUserSkillsViewModel)
        {
            User user = new User();

            user = db.Users.Where(x => x.UserID == addUserSkillsViewModel.UserID).FirstOrDefault();

            var skillIdsList = db.Skills.Where(x => addUserSkillsViewModel.SkillNames.Contains(x.SkillName)).Select(m=>m.SkillID).ToList();

            List<Skills> refer = new List<Skills>();

            foreach (var item in skillIdsList)
            {
                refer.Add(db.Skills.Where(x => x.SkillID == item).FirstOrDefault());
            }
            
            user.Skills.AddRange(refer);
       
            db.SaveChanges();

            string message = "SUCCESS";

            return Json(new { Message = message, JsonRequestBehavior.AllowGet });

        }
    }
}