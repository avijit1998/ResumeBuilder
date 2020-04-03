using HiQPdf;
using ResumeBuilder.Helpers;
using ResumeBuilder.Models;
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
            var session = Session["UserID"];
            int id = (Int32)session;

            try
            {
                var user = db.UserDetails.Include("EducationalDetails").Include("Projects")
                             .Include("Login").Include("Languages").Include("Skills")
                             .Include("WorkExperiences").FirstOrDefault(x => x.UserID == id);

                ViewBag.Languages = db.Languages.ToList();

                ViewBag.Courses = db.Courses.ToList();

                ViewBag.AlreadyDoneCourses = db.EducationalDetails.Where(userId => userId.UserID == id).Select(courseId => courseId.CourseID).ToList();

                var langIds = user.Languages.Select(x => x.LanguageID).ToList();
                if (user != null && langIds != null)
                {
                    AllInformationVM allinfo = new AllInformationVM();
                    {
                        allinfo.UserID = id;
                        allinfo.Name = user.Name == null ? "" : user.Name;
                        allinfo.Gender = user.Gender == null ? "" : user.Gender;
                        allinfo.PhoneNumber = user.Phone == null ? "" : user.Phone;
                        allinfo.DateOfBirth = user.DateOfBirth;
                        allinfo.Summary = user.Summary == null ? "" : user.Summary;
                        allinfo.Languages = user.Languages == null ? new List<Language>() : user.Languages;
                        allinfo.WorkExperiences = user.WorkExperiences == null ? new List<WorkExperience>() : user.WorkExperiences;
                        allinfo.Projects = user.Projects == null ? new List<Project>() : user.Projects;
                        allinfo.Skills = user.Skills == null ? new List<Skill>() : user.Skills;
                        allinfo.EducationalDetail = user.EducationalDetails == null ? new List<EducationalDetails>() : user.EducationalDetails;
                        allinfo.LanguageIds = langIds.Count() == 0 ? new List<int>() : langIds;
                        allinfo.IsAdmin = user.IsAdmin;
                    }
                    return PartialView(allinfo);
                }
                else
                {
                    return new HttpNotFoundResult();
                }
            }
            catch (Exception)
            {
                return HttpNotFound();
            }
        }

        public JsonResult GetSkill(string term)
        {
            term = term.Trim();
            List<string> skills = new List<string>();

            try
            {
                skills = db.Skills.Where(x => x.SkillName.Contains(term)).Select(y => y.SkillName).ToList();
                return Json(skills, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json("Not Found", JsonRequestBehavior.AllowGet);
            }
        }
    }
}

