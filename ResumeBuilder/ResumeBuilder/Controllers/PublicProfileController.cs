using ResumeBuilder.Models;
using ResumeBuilder.ViewModels;
using System.Linq;
using System.Web.Mvc;
using System.Collections.Generic;
using System;

namespace ResumeBuilder.Controllers
{
    public class PublicProfileController : Controller
    {
        private ResumeBuilderConnection _context;
        private PublicProfileViewModel _uiModel;

        public PublicProfileController()
        {
            _context = new ResumeBuilderConnection();
            _uiModel = new PublicProfileViewModel();
        }

        // GET: PublicProfile/Index/id
        public ActionResult Index(int id)
        {
            // User Name
            _uiModel.Name = _context.Users.FirstOrDefault(a => a.UserID == id).Name;

            // User Role
            _uiModel.UserRole = "Web Developer";
            
            // User Phone
            _uiModel.PhoneNumber = _context.Users.FirstOrDefault(a => a.UserID == id).PhoneNumber;

            // User E-mail
            _uiModel.Email = _context.Users.FirstOrDefault(a => a.UserID == id).Username;

            //User Linkedin Link
            _uiModel.LinkedinLink = "https://www.linkedin.com/user";

            // User Summary
            _uiModel.Summary = "Oh, I misunderstood the problem. ResumeBuilder ResumeBuilder Setting a padding on, ResumeBuilder ResumeBuilder bin the padding won't help you.";
            
            // Education Details
            _uiModel.EducationStatus = true;
            _uiModel.EducationList = new List<EducationUIModel>()
            {
                new EducationUIModel{ CourseName = "+2", PassingYear = 2014, CGPAOrPercentage = 90 },
                new EducationUIModel{ CourseName = "+2", PassingYear = 2014, CGPAOrPercentage = 70 },
                new EducationUIModel{ CourseName = "+2", PassingYear = 2014, CGPAOrPercentage = 95 },
                new EducationUIModel{ CourseName = "+2", PassingYear = 2014, CGPAOrPercentage = 95 },
                new EducationUIModel{ CourseName = "+2", PassingYear = 2014, CGPAOrPercentage = 80 }
            };

            // Skills
            _uiModel.SkillStatus = true;
            _uiModel.SkillList = new List<string>()
            {
                "HTML",
                "CSS",
                "JavaScript",
                "C#"
            };

            // Project Details
            _uiModel.ProjectStatus = true;
            _uiModel.ProjectList = new List<ProjectUIModel>()
            {
                new ProjectUIModel{ Title = "Random Title", Description ="Random Desc.", StartDate = new DateTime(1993, 12, 12) , EndDate = new DateTime(1994, 12, 24) },
                new ProjectUIModel{ Title = "Random Title", Description ="Random Desc.", StartDate = new DateTime(1993, 12, 12) , EndDate = new DateTime(1994, 12, 24) },
                new ProjectUIModel{ Title = "Random Title", Description ="Random Desc.", StartDate = new DateTime(1993, 12, 12) , EndDate = new DateTime(1994, 12, 24) },
                new ProjectUIModel{ Title = "Random Title", Description ="Random Desc.", StartDate = new DateTime(1993, 12, 12) , EndDate = new DateTime(1994, 12, 24) }
            };

            // Work Ex.
            _uiModel.WorkExStatus = true;
            _uiModel.WorkExList = new List<WorkExUIModel>()
            {
                new WorkExUIModel{ OrganizationName = "Mindfire Solutions", Role = "Developer", StartDate = new DateTime(1993, 12, 12) , EndDate = new DateTime(1994, 12, 24) },
                new WorkExUIModel{ OrganizationName = "Mindfire Solutions", Role = "Developer", StartDate = new DateTime(1993, 12, 12) , EndDate = new DateTime(1994, 12, 24) },
                new WorkExUIModel{ OrganizationName = "Mindfire Solutions", Role = "Developer", StartDate = new DateTime(1993, 12, 12) , EndDate = new DateTime(1994, 12, 24) },
                new WorkExUIModel{ OrganizationName = "Mindfire Solutions", Role = "Developer", StartDate = new DateTime(1993, 12, 12) , EndDate = new DateTime(1994, 12, 24) }
            };

            // Languages 
            _uiModel.Languages = new List<string>()
            {
                "English",
                "Hindi",
                "Odia",
                "Bengoli"
            };
            
            return View(_uiModel);
            //var data = _context.Users.ToList();return Json(_uiModel, JsonRequestBehavior.AllowGet);
        }
    }
}