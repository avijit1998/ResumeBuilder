using ResumeBuilder.Models;
using ResumeBuilder.Models.ViewModels;
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

                ViewBag.Courses = db.Courses.ToList();
                ViewBag.Languages = db.Languages.ToList();
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
        
        public ActionResult DisplayDetails( int[] finalresult)
        {
            int id=1;
            if (Session["UserID"] != null)
            {
                
                var re = Int32.TryParse(Session["UserID"] as String, out id);
            }
            var ob = db.settings.SingleOrDefault(user => user.UserSettingId == id);
            ob.setWorkex = finalresult[0];
            ob.setProject = finalresult[1];
            ob.setEducation = finalresult[2];
            ob.setSkills = finalresult[3];
            ob.setContact = finalresult[4];
            TryUpdateModel(ob);
            db.SaveChanges();
            return Json("success");
        }

        public ActionResult settingsValue()
        {
            int id = 1;
            if (Session["UserID"] != null)
            {

                var re = Int32.TryParse(Session["UserID"] as String, out id);
            }
            var ob = db.settings.SingleOrDefault(user => user.UserSettingId == id);
            UserSetting ob1 = new UserSetting();
            ob1.setWorkex = ob.setWorkex;
            ob1.UserSettingId = ob.UserSettingId;
            ob1.setSkills = ob.setSkills;
            ob1.setProject = ob.setProject;
            ob1.setEducation = ob.setEducation;
            ob1.setContact = ob.setContact;

            return Json(ob1, JsonRequestBehavior.AllowGet);
        }

        
    }
}