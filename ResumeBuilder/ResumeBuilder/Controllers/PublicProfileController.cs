using ResumeBuilder.Models;
using ResumeBuilder.ViewModels;
using System.Linq;
using System.Web.Mvc;
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
            try
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
                if (_uiModel.EducationStatus == 1)
                {
                    _uiModel.EducationList = (from user in _context.EducationalDetails.ToList()
                                              select new EducationUIModel
                                              {
                                                  CourseName = _context.Courses.FirstOrDefault(x => x.CourseId == user.CourseId).CourseName,
                                                  CGPAOrPercentage = user.CGPAOrPercentage,
                                                  Board = user.Board,
                                                  Stream = user.Stream,
                                                  TotalPercentorCGPAValue = user.TotalPercentorCGPAValue,
                                                  PassingYear = user.PassingYear
                                              }).OrderByDescending(x => x.PassingYear).ToList();
                }

                // Skills
                _uiModel.SkillStatus = _context.settings.FirstOrDefault(a => a.UserID == id).setSkills;
                if (_uiModel.SkillStatus == 1)
                {
                    //_uiModel.SkillList = _context.Users.FirstOrDefault(x => x.UserID == id).Skills.Select(a => a.SkillName).ToList();
                }

                // Project Details
                _uiModel.ProjectStatus = _context.settings.FirstOrDefault(a => a.UserID == id).setProject;
                if (_uiModel.ProjectStatus == 1)
                {
                    _uiModel.ProjectList = (from user in _context.Projects.Where(x => x.UserId == id)
                                            select new ProjectUIModel
                                            {
                                                Title = user.Title,
                                                Description = user.Description,
                                                Duration = user.Duration
                                            }).ToList();
                }

                // Work Ex.
                _uiModel.WorkExStatus = _context.settings.FirstOrDefault(a => a.UserID == id).setWorkex;
                if (_uiModel.WorkExStatus == 1)
                {
                    _uiModel.WorkExList = (from user in _context.WorkExperiences.Where(x => x.UserID == id)
                                            select new WorkExUIModel
                                            {
                                                OrganizationName = user.OrganizationName,
                                                StartMonth = user.StartMonth,
                                                StartYear = user.StartYear,
                                                EndMonth = user.EndMonth,
                                                EndYear = user.EndYear,
                                                Role = user.Role,
                                                CurrentlyWorking = user.CurrentlyWorking
                                            }).ToList();
                }

                // Languages 
                _uiModel.LanguageStatus = 1;
                if (_uiModel.LanguageStatus == 1)
                {
                    _uiModel.Languages = _context.Users.FirstOrDefault(b => b.UserID == id).Languages.Select(a => a.Language).ToList();
                }
            }
            catch (Exception)
            {
                _uiModel.ErrorMsg = "Unexpected error occured, try again...";                
            }
            return View(_uiModel);
        }
    }
}