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
            _uiModel.UserRole = _context.Users.FirstOrDefault(a => a.UserID == id).UserRole;
            
            // User Phone
            _uiModel.PhoneNumber = _context.Users.FirstOrDefault(a => a.UserID == id).PhoneNumber;

            // User E-mail
            _uiModel.Email = _context.Users.FirstOrDefault(a => a.UserID == id).Username;

            //User Linkedin Link
            _uiModel.LinkedinLink = "https://www.linkedin.com/user";

            // User Summary
            _uiModel.Summary = _context.Users.FirstOrDefault(a => a.UserID == id).Summary;
            
            // Education Details
            _uiModel.EducationStatus = _context.settings.FirstOrDefault(a => a.UserID == id).setEducation;
            if(_uiModel.EducationStatus == 1)
            {
                try
                {
                    _uiModel.EducationList = (from user in _context.EducationalDetails.ToList()
                                              select new EducationUIModel 
                                              {
                                                  CourseName = _context.Courses.FirstOrDefault(x => x.CourseId == user.CourseId).CourseName,
                                                  CGPAOrPercentage =user.CGPAOrPercentageValue,
                                                  PassingYear = user.PassingYear
                                              }).ToList();
                }
                catch (Exception)
                {
                    
                    
                }
            }

            // Skills
            _uiModel.SkillStatus = _context.settings.FirstOrDefault(a => a.UserID == id).setSkills;
            if(_uiModel.SkillStatus == 1)
            {
                try
                {
                    _uiModel.SkillList = _context.Users.Where(x => x.UserID == id).Select(a => a.Skills.Select(b => b.SkillName)).ToList();
                }
                catch (Exception)
                {
                    
                    
                }
            }

            // Project Details
            _uiModel.ProjectStatus = _context.settings.FirstOrDefault(a => a.UserID == id).setProject;
            if (_uiModel.ProjectStatus == 1)
            {
                _uiModel.ProjectList = new List<ProjectUIModel>()
                {
                    new ProjectUIModel{ Title = "Random Title", Description ="Random Desc.", StartDate = new DateTime(1993, 12, 12) , EndDate = new DateTime(1994, 12, 24) },
                    new ProjectUIModel{ Title = "Random Title", Description ="Random Desc.", StartDate = new DateTime(1993, 12, 12) , EndDate = new DateTime(1994, 12, 24) },
                    new ProjectUIModel{ Title = "Random Title", Description ="Random Desc.", StartDate = new DateTime(1993, 12, 12) , EndDate = new DateTime(1994, 12, 24) },
                    new ProjectUIModel{ Title = "Random Title", Description ="Random Desc.", StartDate = new DateTime(1993, 12, 12) , EndDate = new DateTime(1994, 12, 24) }
                }; 
            }

            // Work Ex.
            _uiModel.WorkExStatus = _context.settings.FirstOrDefault(a => a.UserID == id).setWorkex;
            if (_uiModel.WorkExStatus == 1)
            {
                _uiModel.WorkExList = new List<WorkExUIModel>()
                {
                    new WorkExUIModel{ OrganizationName = "Mindfire Solutions", Role = "Developer", StartDate = new DateTime(1993, 12, 12) , EndDate = new DateTime(1994, 12, 24) },
                    new WorkExUIModel{ OrganizationName = "Mindfire Solutions", Role = "Developer", StartDate = new DateTime(1993, 12, 12) , EndDate = new DateTime(1994, 12, 24) },
                    new WorkExUIModel{ OrganizationName = "Mindfire Solutions", Role = "Developer", StartDate = new DateTime(1993, 12, 12) , EndDate = new DateTime(1994, 12, 24) },
                    new WorkExUIModel{ OrganizationName = "Mindfire Solutions", Role = "Developer", StartDate = new DateTime(1993, 12, 12) , EndDate = new DateTime(1994, 12, 24) }
                }; 
            }

            // Languages 
            _uiModel.LanguageStatus = 1;
            if(_uiModel.LanguageStatus == 1)
            {
                try
                {
                    _uiModel.Languages = _context.Users.Where(b => b.UserID == id).Select(x => x.Languages.Select(a => a.Language)).ToList();
                }
                catch (Exception)
                {
                    
                    
                }
            }
            //return View(_uiModel);
            var data = _context.Users.Include("Skills").Where(x => x.UserID == id).Select(a => a.Skills.Select(b => b.SkillName)).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}