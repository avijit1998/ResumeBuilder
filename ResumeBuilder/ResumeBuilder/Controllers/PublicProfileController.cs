using ResumeBuilder.Models;
using ResumeBuilder.ViewModels;
using System.Linq;
using System.Web.Mvc;
using System;

//namespace ResumeBuilder.Controllers
//{
//    public class PublicProfileController : Controller
//    {
//        private ResumeBuilderDBContext _context;
//        private PublicProfileViewModel _uiModel;

//        public PublicProfileController()
//        {
//            _context = new ResumeBuilderDBContext();
//            _uiModel = new PublicProfileViewModel();
//        }

//        // GET: PublicProfile/Index/id
//        public ActionResult Index(int id)
//        {
//            try
//            {
//                // User Details
//                var userData = _context.Users.FirstOrDefault(a => a.UserID == id);
//                // User Name
//                _uiModel.Name = userData.Name;

//                // User Role
//                _uiModel.UserRole = userData.UserRole;

//                // User Phone
//                _uiModel.PhoneNumber = userData.PhoneNumber;

//                // User E-mail
//                _uiModel.Email = userData.Username;

//                // User Summary
//                _uiModel.Summary = userData.Summary;
                
//                // Education Details
//                _uiModel.EducationStatus = _context.settings.FirstOrDefault(a => a.UserID == id).setEducation;
//                if (_uiModel.EducationStatus == 0)
//                {
//                    _uiModel.EducationList = (from user in _context.EducationalDetails.Where(x => x.UserId == id)
//                                              select new EducationUIModel
//                                              {
//                                                  CourseName = (_context.Courses.FirstOrDefault(x => x.CourseId == user.CourseId).CourseName == "10"
//                                                               || _context.Courses.FirstOrDefault(x => x.CourseId == user.CourseId).CourseName == "12")?
//                                                               _context.Courses.FirstOrDefault(x => x.CourseId == user.CourseId).CourseName + " TH" :
//                                                               _context.Courses.FirstOrDefault(x => x.CourseId == user.CourseId).CourseName,
//                                                  CGPAOrPercentage = user.CGPAOrPercentage,
//                                                  Board = user.Board,
//                                                  Stream = (user.Stream == null)? "" : "(" + user.Stream + ")",
//                                                  TotalPercentorCGPAValue = user.TotalPercentorCGPAValue,
//                                                  PassingYear = user.PassingYear
//                                              }).OrderByDescending(x => x.PassingYear).ToList();
//                }

//                // Skills
//                _uiModel.SkillStatus = _context.settings.FirstOrDefault(a => a.UserID == id).setSkills;
//                if (_uiModel.SkillStatus == 0)
//                {
//                    _uiModel.SkillList = userData.Skills.Select(a => a.SkillName).ToList();
//                }

//                // Project Details
//                _uiModel.ProjectStatus = _context.settings.FirstOrDefault(a => a.UserID == id).setProject;
                
//                    _uiModel.ProjectList = (from user in _context.Projects.Where(x => x.UserId == id)
//                                            select new ProjectUIModel
//                                            {
//                                                Title = user.Title,
//                                                Description = user.Description,
//                                                projectRole = user.ProjectRole,
//                                                Duration = user.Duration
//                                            }).ToList();

//                // Work Ex.
//                _uiModel.WorkExStatus = _context.settings.FirstOrDefault(a => a.UserID == id).setWorkex;
                
//                    _uiModel.WorkExList = (from user in _context.WorkExperiences.Where(x => x.UserID == id)
//                                           select new WorkExUIModel
//                                           {
//                                               OrganizationName = user.OrganizationName,
//                                               StartMonth = (user.StartMonth <= 9)? "0" + user.StartMonth : user.StartMonth.ToString(),
//                                               StartYear = user.StartYear,
//                                               EndMonth = (user.EndMonth <= 9)? "0" + user.EndMonth : user.EndMonth.ToString(),
//                                               EndYear = user.EndYear,
//                                               Role = user.Role,
//                                               CurrentlyWorking = user.CurrentlyWorking
//                                           }).OrderByDescending(x => x.StartYear).ToList();

//                // Languages 
//                _uiModel.LanguageStatus = _context.settings.FirstOrDefault(a => a.UserID == id).setContact;
//                if (_uiModel.LanguageStatus == 0)
//                {
//                    _uiModel.Languages = userData.Languages.Select(a => a.Language).ToList();
//                }
//            }
//            catch (Exception)
//            { 
//                _uiModel.ErrorMsg = "Unexpected error occured, try again...";                
//            }
//            return View(_uiModel);
//        }
//    }
//}