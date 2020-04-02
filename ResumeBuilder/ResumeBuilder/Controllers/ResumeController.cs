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

        [HttpPost]
        public ActionResult SaveBasicInformation(UserInfoVM userInfoVM)
        {
            if (!ModelState.IsValid)
            {
                return null;
            }
            try
            {
                var userFromDb = db.UserDetails.FirstOrDefault(u => u.UserID == userInfoVM.UserID);

                if (userFromDb == null)
                {
                    return null;
                }
                else
                {
                    userFromDb.Name = userInfoVM.Name;
                    userFromDb.Gender = userInfoVM.Gender;
                    userFromDb.Phone = userInfoVM.PhoneNumber;
                    userFromDb.DateOfBirth = userInfoVM.DateOfBirth;
                    userFromDb.Summary = userInfoVM.Summary;

                    if (userInfoVM.LanguageIds.Any())
                    {
                        var languages = db.Languages.Where(x => userInfoVM.LanguageIds.Contains(x.LanguageID)).ToList();
                        if (languages == null)
                        {
                            return null;
                        }
                        userFromDb.Languages.AddRange(languages);
                    }

                    db.SaveChanges();

                    return Json("Success", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        [HttpPost]
        public ActionResult SaveProjectDetails(ProjectInfoVM projectInfoVM)
        {
            var session = Session["UserID"];
            int id = (Int32)session;
            projectInfoVM.UserID = id;
            try{
                if(projectInfoVM.ProjectID==0)
                {
                    db.Projects.Add(new Project
                    {
                        UserID = projectInfoVM.UserID,
                        ProjectTitle = projectInfoVM.ProjectTitle,
                        DurationInMonth = projectInfoVM.DurationInMonth,
                        ProjectRole = projectInfoVM.ProjectRole,
                        Description = projectInfoVM.Description                        
                    });

                    db.SaveChanges();
                    
                }
                else
                {
                    if (ModelState.IsValid)
                    {
                        var projFromDb = db.Projects.FirstOrDefault(x => x.ProjectID == projectInfoVM.ProjectID);
                        if (projFromDb != null)
                        {
                            projFromDb.UserID = projectInfoVM.UserID;
                            projFromDb.ProjectID = projectInfoVM.ProjectID;
                            projFromDb.ProjectTitle = projectInfoVM.ProjectTitle;
                            projFromDb.ProjectRole = projectInfoVM.ProjectRole;
                            projFromDb.DurationInMonth = projectInfoVM.DurationInMonth;
                            projFromDb.Description = projectInfoVM.Description;

                            db.SaveChanges();
                        }

                        else
                        {
                            return HttpNotFound();
                        }


                    }

                }
                return Json("Success", JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return null;
            }
        }

        [HttpPost]
        public ActionResult SaveWorkExperience(WorkExperienceVM workExperienceVM)
        {
            var session = Session["UserID"];
            int id = (Int32)session;
            workExperienceVM.UserID = id;

            try{
                if(workExperienceVM.WorkExperienceID==0)
                {
                    db.WorkExperiences.Add(new WorkExperience
                    {
                        UserID = workExperienceVM.UserID,
                        StartMonth = workExperienceVM.StartMonth,
                        StartYear = workExperienceVM.StartYear,
                        EndMonth = workExperienceVM.EndMonth,
                        EndYear = workExperienceVM.EndYear,
                        OrganizationName = workExperienceVM.OrganizationName,
                        Designation = workExperienceVM.Designation,
                        IsCurrentlyWorking = workExperienceVM.IsCurrentlyWorking,                       
                    });

                    db.SaveChanges();
                    
                }
                else
                {
                    if (ModelState.IsValid)
                    {
                        var workExFromDb = db.WorkExperiences.FirstOrDefault(x => x.WorkExperienceID == workExperienceVM.WorkExperienceID);
                        if (workExFromDb != null)
                        {
                            workExFromDb.UserID = workExperienceVM.UserID;
                            workExFromDb.StartMonth = workExperienceVM.StartMonth;
                            workExFromDb.StartYear = workExperienceVM.StartYear;
                            workExFromDb.EndMonth = workExperienceVM.EndMonth;
                            workExFromDb.EndYear = workExperienceVM.EndYear;
                            workExFromDb.OrganizationName = workExperienceVM.OrganizationName;
                            workExFromDb.Designation = workExperienceVM.Designation;
                            workExFromDb.IsCurrentlyWorking = workExperienceVM.IsCurrentlyWorking;   

                            db.SaveChanges();
                          }

                        else
                        {
                            return HttpNotFound();
                        }

                    }

                }
                return Json("Success", JsonRequestBehavior.AllowGet);
            }

            catch (Exception)
            {
                return null;
            }
        }

        [HttpPost]
        public ActionResult SaveEducationalDetails(EducationalDetailsVM educationalDetailsVM)
        {
                  
            try
            {
                if (educationalDetailsVM.EducationalDetailsID == 0)
                {
                    db.EducationalDetails.Add(new EducationalDetails
                    {
                        UserID = educationalDetailsVM.UserID,
                        CourseID = educationalDetailsVM.CourseID,
                        BoardOrUniversity = educationalDetailsVM.BoardOrUniversity,
                        PassingYear = educationalDetailsVM.PassingYear,
                        Stream = educationalDetailsVM.Stream,
                        CGPAOrPercentage = educationalDetailsVM.CGPAOrPercentage,
                        TotalPercentageOrCGPAValue = educationalDetailsVM.TotalPercentageOrCGPAValue
                    });
                    
                }
                else
                {
                    if (!ModelState.IsValid)
                    {
                        var errors = ModelState.Values.SelectMany(v => v.Errors);
                        return HttpNotFound();
                    }
              
                    var educationalDetails = db.EducationalDetails.FirstOrDefault(id => id.EducationalDetailsID == educationalDetailsVM.EducationalDetailsID);

                    if (educationalDetails == null)
                    {
                        return HttpNotFound();
                    }

                    educationalDetails.UserID = educationalDetailsVM.UserID;
                    educationalDetails.CourseID = educationalDetailsVM.CourseID;
                    educationalDetails.BoardOrUniversity = educationalDetails.BoardOrUniversity;
                    educationalDetails.PassingYear = educationalDetails.PassingYear;
                    educationalDetails.Stream = educationalDetailsVM.Stream;
                    educationalDetails.CGPAOrPercentage = educationalDetailsVM.CGPAOrPercentage;
                    educationalDetails.TotalPercentageOrCGPAValue = educationalDetailsVM.TotalPercentageOrCGPAValue;
                }

                db.SaveChanges();
                return Json("SUCCESS", JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return HttpNotFound();
            }
        }

        [HttpPost]
        public ActionResult SaveUserSkills(SkillsVM skillsVM)
        {
            if (!ModelState.IsValid)
            {
                return null;
            }
            try
            {
                UserDetails user = db.UserDetails.Where(x => x.UserID == skillsVM.UserID).FirstOrDefault();
                if (user == null)
                {
                    return null;
                }

                var skillIdsList = db.Skills.Where(x => skillsVM.SkillNames.Contains(x.SkillName)).Select(m => m.SkillID).ToList();
                
                List<Skill> refer = new List<Skill>();
                
                foreach (var item in skillIdsList)
                {
                    refer.Add(db.Skills.Where(x => x.SkillID == item).FirstOrDefault());
                }

                user.Skills.AddRange(refer);
                db.SaveChanges();
                return Json("SUCCESS", JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return HttpNotFound();
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

        public ActionResult SaveSettingStatus(SettingsDetailsVM settingStatus)
        {
           
            var session = Session["UserID"];
            int id = (Int32)session;

            var userSettings = db.Settings.SingleOrDefault(user => user.UserID == id);
            userSettings.WorkExperienceStatus = settingStatus.WorkExperienceStatus;
            userSettings.SkillsDetailsStatus = settingStatus.SkillsDetailsStatus;
            userSettings.ProjectDetailsStatus = settingStatus.ProjectDetailsStatus;
            userSettings.LanguagesStatus = settingStatus.LanguagesStatus;
            userSettings.EducationalDetailsStatus = settingStatus.EducationalDetailsStatus;
            TryUpdateModel(userSettings);
            db.SaveChanges();
            return Json("success", JsonRequestBehavior.AllowGet);
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

