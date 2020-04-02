using HiQPdf;
using ResumeBuilder.Helpers;
using ResumeBuilder.Models;
using ResumeBuilder.Models.ViewModels;
using ResumeBuilder.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;


namespace ResumeBuilder.Controllers
{
    [AuthorizeIfSessionExists]
    public class ResumeController : Controller
    {
        private ResumeBuilderDBContext db;
        public ResumeController()
        {
            db = new ResumeBuilderDBContext();
        }

        // GET: Resume
        public ActionResult Index()
        {
            if (Session.Count != 0)
                return View();
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        public ActionResult ShowData()
        {
            if (Session["UserID"] != null)
            {
                
                var session = Session["UserID"];
                int id = (Int32)session;
                var user = db.UserDetails.Include("EducationalDetails").Include("Projects")
                    .Include("Login").Include("Languages").Include("Skills")
                    .Include("WorkExperiences").FirstOrDefault(x => x.UserID == id);

                ViewBag.Languages = db.Languages.ToList();

                ViewBag.Courses = db.Courses.ToList();

                ViewBag.AlreadyDoneCourses = db.EducationalDetails.Where(userId=>userId.UserID == id).Select(courseId => courseId.CourseID).ToList();

                var langIds = user.Languages.Select(x => x.LanguageID).ToList();
                if (user != null)
                {
                    AllInformation allinfo = new AllInformation();
                    {
                        allinfo.UserID = id;
                        allinfo.Name = user.Name==null ? "Your name" : user.Name;
                        allinfo.Gender = user.Gender==null ? "Your gender" :user.Gender;
                        allinfo.PhoneNumber = user.Phone==null? "Your phone number" : user.Phone;
                        allinfo.DateOfBirth = user.DateOfBirth;
                        allinfo.Summary = user.Summary==null? "Your profile summary" : user.Summary;
                        allinfo.Languages = user.Languages==null? new List<Language>() :  user.Languages;
                        allinfo.WorkExperiences = user.WorkExperiences==null? new List<WorkExperience>() : user .WorkExperiences;
                        allinfo.Projects = user.Projects==null ? new List<Project>() : user.Projects ;
                        allinfo.Skills = user.Skills==null ? new List<Skill>() : user.Skills;
                        allinfo.EducationalDetail = user.EducationalDetails==null?new List<EducationalDetails>() : user.EducationalDetails;
                        allinfo.LanguageIds = langIds.Count()==0 ? new List<int>() : langIds;
                    }
                    return PartialView(allinfo);
                }
                else
                {
                    return new HttpNotFoundResult();
                }
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        public ActionResult SetUserSettingStatus()
        {
           
            var session = Session["UserID"];
            int id = (Int32)session;

            var userSavedSettings = db.Settings.SingleOrDefault(user => user.UserID == id);
            SettingsDetailsVM userSettingsStatus = new SettingsDetailsVM();
            userSettingsStatus.EducationalDetailsStatus = userSavedSettings.EducationalDetailsStatus;
            userSettingsStatus.LanguagesStatus = userSavedSettings.LanguagesStatus;
            userSettingsStatus.ProjectDetailsStatus = userSavedSettings.ProjectDetailsStatus;
            userSettingsStatus.SkillsDetailsStatus = userSavedSettings.SkillsDetailsStatus;
            userSettingsStatus.WorkExperienceStatus = userSavedSettings.WorkExperienceStatus;

            return Json(userSettingsStatus, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeleteProject(int id)
        {
            var proj = db.Projects.FirstOrDefault(x => x.ProjectID == id);
            if (proj != null)
            {
                db.Projects.Remove(proj);
                db.SaveChanges();
                return Json("Successfully Deleted");
            }
            else
            {
                return HttpNotFound();
            }

        }

        [HttpPost]
        public ActionResult DeleteWorkExperience(int id)
        {
            var workEx = db.WorkExperiences.FirstOrDefault(x => x.WorkExperienceID == id);
            if (workEx != null)
            {
                db.WorkExperiences.Remove(workEx);
                db.SaveChanges();
                return Json("Successfully Deleted");
            }
            else
            {
                return HttpNotFound();
            }
        }


        [HttpPost]
        public ActionResult DeleteEducation(int educationId)
        {
            var educationDetails = db.EducationalDetails.FirstOrDefault(x => x.EducationalDetailsID == educationId);
            if (educationDetails != null)
            {
                db.EducationalDetails.Remove(educationDetails);
                db.SaveChanges();
                return Json("Successfully Deleted");
            }
            else
            {
                return HttpNotFound();
            }

        }

        [HttpPost]
        public ActionResult DeleteSkill(int userID, int skillID)
        {
            var user = db.UserDetails.FirstOrDefault(x => x.UserID == userID);
            var skill = db.Skills.FirstOrDefault(x => x.SkillID == skillID);

            if (user != null && skill != null)
            {
                user.Skills.Remove(skill);

                db.SaveChanges();
                return Json("Successfully Deleted");
            }
            else
            {
                return HttpNotFound();
            }
        }

        public JsonResult GetSkill(string term)
        {
            term = term.Trim();
            List<string> skills;

            skills = db.Skills.Where(x => x.SkillName.Contains(term)).Select(y => y.SkillName).ToList();

            return Json(skills, JsonRequestBehavior.AllowGet);
        }       
    }
}

