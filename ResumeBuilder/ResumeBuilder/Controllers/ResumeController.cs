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
                //var re = Int32.TryParse(session as string, out id);
                var user = db.UserDetails.Include("EducationalDetails").Include("Projects")
                    .Include("Login").Include("Languages").Include("Skills")
                    .Include("WorkExperiences").FirstOrDefault(x => x.UserID == id);

                ViewBag.Languages = db.Languages.ToList();
                ViewBag.Courses = db.Courses.ToList();
                var langIds = user.Languages.Select(x => x.LanguageID).ToList();
                if (user != null)
                {
                    AllInformation allinfo = new AllInformation();
                    {
                        allinfo.UserID = id;
                        allinfo.Name = user.Name==null ? "N/A" : user.Name;
                        allinfo.Gender = user.Gender==null ? "N/A" :user.Gender;
                        allinfo.PhoneNumber = user.Phone==null? "N/A" : user.Phone;
                        allinfo.DateOfBirth = user.DateOfBirth;
                        allinfo.Summary = user.Summary==null? "N/A" : user.Summary;
                        allinfo.Languages = user.Languages==null? new List<Language>() :  user.Languages;
                        allinfo.WorkExperiences = user.WorkExperiences==null? new List<WorkExperience>() : user .WorkExperiences;
                        allinfo.Projects = user.Projects==null ? new List<Project>() : user.Projects ;
                        //allinfo.Login.Username = user.Login.Username;
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
            if (!ModelState.IsValid)
            {
                return null;
            }
            try
            {
                if (projectInfoVM == null)
                {
                    return null;
                }
                db.Projects.Add(new Project
                {
                    UserID = projectInfoVM.UserID,
                    ProjectTitle = projectInfoVM.ProjectTitle,
                    DurationInMonth = projectInfoVM.DurationInMonth,
                    ProjectRole = projectInfoVM.ProjectRole,
                    Description = projectInfoVM.Description
                });
                db.SaveChanges();
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
            if (!ModelState.IsValid)
            {
                return null;
            }

            try
            {
                if (workExperienceVM == null)
                {
                    return null;
                }

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
                return Json("SUCCESS", JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return null;
            }
        }

        [HttpPost]
        public ActionResult SaveEducationalDetails(EducationalDetailsVM educationalDetailsVM)
        {
            if (!ModelState.IsValid)
            {
                return null;
            }
            try
            {
                if (educationalDetailsVM == null)
                {
                    return null;
                }

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
                db.SaveChanges();
                return Json("SUCCESS", JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return null;
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
                return null;
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


    }
}

//        public ActionResult GetProjectById(int Id)
//        {
//            var proj = db.Projects.FirstOrDefault(x => x.ProjectId == Id);
//            var project = new Project
//            {
//                ProjectId = proj.ProjectId,
//                Title = proj.Title,
//                Duration = proj.Duration,
//                ProjectRole = proj.ProjectRole,
//                Description = proj.Description
//            };
//            return Json(project, JsonRequestBehavior.AllowGet);
//        }

//        [HttpPost]
//        public ActionResult UpdateProject(int projectId, Project model)
//        {
//            var project = db.Projects.FirstOrDefault(x => x.ProjectId == projectId);

//            project.Title = model.Title;
//            project.ProjectRole = model.ProjectRole;
//            project.Description = model.Description;
//            project.Duration = model.Duration;

//            db.Entry(project).State = System.Data.Entity.EntityState.Modified;
//            db.SaveChanges();
//            return Json("Success");
//        }


//        public ActionResult Form()
//        {
//            //var summary = user.Summary;
//            if (Session["UserID"] != null)
//            {
//                int id;
//                var re = Int32.TryParse(Session["UserID"] as String, out id);
//                var user = db.Users.Where(m => m.UserID == id).FirstOrDefault();
//                ViewBag.Courses = db.Courses.ToList();

//                ViewBag.AlreadyDoneCourses = (from course in db.EducationalDetails
//                                             where course.UserId == id
//                                             select course.CourseId).ToList();

//                ViewBag.Languages = db.Languages.ToList();
//                return PartialView(user);                               
//            }
//            return RedirectToAction("Login", "Account");
//        }

//        [HttpPost]
//        public ActionResult DeleteProject(int projectID)
//        {
//            var proj = db.Projects.FirstOrDefault(x => x.ProjectId == projectID);
//            if (proj != null)
//            {
//                db.Projects.Remove(proj);
//                db.SaveChanges();
//                return Json("Successfully Deleted");
//            }
//            else
//            {
//                return HttpNotFound();
//            }

//        }

//        public ActionResult GetWorkExperienceById(int id)
//        {
//            var workex = db.WorkExperiences.FirstOrDefault(x => x.WorkExperienceid == id);
//            var workExperience = new WorkExperience
//            {
//                WorkExperienceid = workex.WorkExperienceid,
//                OrganizationName = workex.OrganizationName,
//                Role = workex.Role,
//                StartMonth = workex.StartMonth,
//                StartYear = workex.StartYear,
//                EndMonth = workex.EndMonth,
//                EndYear = workex.EndYear,
//                CurrentlyWorking = workex.CurrentlyWorking
//            };
//            return Json(workExperience, JsonRequestBehavior.AllowGet);
//        }

//        [HttpPost]
//        public ActionResult UpdateWorkExperience(int WorkExperienceid, WorkExperience model)
//        {
//            var workEx = db.WorkExperiences.FirstOrDefault(x => x.WorkExperienceid == WorkExperienceid);
//            workEx.OrganizationName = model.OrganizationName;
//            workEx.Role = model.Role;
//            workEx.StartMonth = model.StartMonth;
//            workEx.StartYear = model.StartYear;
//            workEx.EndMonth = model.EndMonth;
//            workEx.EndYear = model.EndYear;
//            workEx.CurrentlyWorking = model.CurrentlyWorking;

//            db.Entry(workEx).State = System.Data.Entity.EntityState.Modified;
//            db.SaveChanges();
//            return Json("Success");
//        }

//        [HttpPost]
//        public ActionResult DeleteWorkExperience(int workExId)
//        {
//            var workEx = db.WorkExperiences.FirstOrDefault(x => x.WorkExperienceid == workExId);
//            if (workEx != null)
//            {
//                db.WorkExperiences.Remove(workEx);
//                db.SaveChanges();
//                return Json("Successfully Deleted");
//            }
//            else
//            {
//                return HttpNotFound();
//            }
//        }


//        [HttpPost]
//        public ActionResult SaveSummary(UserDetail user)
//        {
//            var userFromDb = db.UserDetail.FirstOrDefault(u => u.UserID == user.UserID);

//            userFromDb.Summary = user.Summary;
//            db.SaveChanges();

//            string message = "SUCCESS";

//            return Json(new { Message = message, JsonRequestBehavior.AllowGet });
//        }

       


        
//        public ActionResult DisplayDetails(int[] finalresult)
//        {
//            int id = 1;
//            if (Session["UserID"] != null)
//            {

//                var re = Int32.TryParse(Session["UserID"] as String, out id);
//            }
//            var ob = db.settings.Where(user => user.UserID == id).FirstOrDefault();
//            ob.setWorkex = finalresult[0];
//            ob.setProject = finalresult[1];
//            ob.setEducation = finalresult[2];
//            ob.setSkills = finalresult[3];
//            ob.setContact = finalresult[4];
//            TryUpdateModel(ob);
//            db.SaveChanges();
//            return Json("success",JsonRequestBehavior.AllowGet);
//        }

//        public ActionResult settingsValue()
//        {
//            int id = 1;
//            if (Session["UserID"] != null)
//            {

//                var re = Int32.TryParse(Session["UserID"] as String, out id);
//            }
//            var ob = db.settings.SingleOrDefault(user => user.UserID == id);
//            UserSetting ob1 = new UserSetting();
//            ob1.setWorkex = ob.setWorkex;
//            ob1.UserSettingId = ob.UserSettingId;
//            ob1.setSkills = ob.setSkills;
//            ob1.setProject = ob.setProject;
//            ob1.setEducation = ob.setEducation;
//            ob1.setContact = ob.setContact;

//            return Json(ob1, JsonRequestBehavior.AllowGet);
//        }

//        

//        public ActionResult Search()
//        {
//            return PartialView();
//        }

//        public ActionResult GetUserSkills()
//        {
//            //db.Configuration.ProxyCreationEnabled = false;
//            List<UserSkillVM> listUserSkills = new List<UserSkillVM>();
//            listUserSkills = (from user in db.Users.Include("Skills").ToList()
//                                    select new UserSkillVM
//                                    {
//                                        UserID = user.UserID,
//                                        UserName = user.Name,
//                                        SkillNames = user.Skills.Select(x => x.SkillName).ToList()
//                                    }).ToList();
            
            
//            return Json(listUserSkills, JsonRequestBehavior.AllowGet);
//        }

//        public ActionResult GetEducationById(int id)
//        {
//            var edu = db.EducationalDetails.FirstOrDefault(x => x.EducationalDetailID == id);

//            var education = new EducationalDetails
//            {
//                EducationalDetailID = edu.EducationalDetailID,
//                CourseId = edu.CourseId,
//                Board = edu.Board,
//                PassingYear = edu.PassingYear,
//                Stream = edu.Stream,
//                TotalPercentorCGPAValue = edu.TotalPercentorCGPAValue,
//                CGPAOrPercentage = edu.CGPAOrPercentage
//            };
//            return Json(education, JsonRequestBehavior.AllowGet);
//        }


//        [HttpPost]
//        public ActionResult UpdateEducation(int EducationalDetailID, EducationalDetails model)
//        {
//            var edu = db.EducationalDetails.FirstOrDefault(x => x.EducationalDetailID == EducationalDetailID);

//            edu.EducationalDetailID = model.EducationalDetailID;
//            edu.CourseId = model.CourseId;
//            edu.PassingYear = model.PassingYear;
//            edu.Stream = model.Stream;
//            edu.Board = model.Board;
//            edu.CGPAOrPercentage = model.CGPAOrPercentage;
//            edu.TotalPercentorCGPAValue = model.TotalPercentorCGPAValue;

//            db.Entry(edu).State = System.Data.Entity.EntityState.Modified;
//            db.SaveChanges();
//            return Json("Success");
//        }


//        [HttpPost]
//        public ActionResult DeleteEducation(int educationId)
//        {
//            var edu = db.EducationalDetails.FirstOrDefault(x => x.EducationalDetailID == educationId);
//            if (edu != null)
//            {
//                db.EducationalDetails.Remove(edu);
//                db.SaveChanges();
//                return Json("Successfully Deleted");
//            }
//            else
//            {
//                return HttpNotFound();
//            }

//        }

//        [HttpPost]
//        public ActionResult DeleteSkill(int skillId)
//        {
//            int id;
//            var re = Int32.TryParse(Session["UserID"] as String, out id);
//            var user = db.Users.FirstOrDefault(x => x.UserID == id);
//            var skill = db.Skills.FirstOrDefault(x => x.SkillID == skillId);

//            if(user!=null && skill!=null)
//            {
//                user.Skills.Remove(skill);

//                db.SaveChanges();
//                return Json("Successfully Deleted");
//            }
//            else
//            {
//                return HttpNotFound();
//            }
//        }
        

        
//        public ActionResult GetCurrentUser(int id)
//        {
//            var data = db.Users.Include("Languages").FirstOrDefault(x => x.UserID == id);
//            var user = new EditUserLanguageViewModel
//            {
//                UserID = data.UserID,
//                Name=data.Name,
//                Username = data.Username,
//                Gender = data.Gender,
//                Summary=data.Summary,
//                PhoneNumber=data.PhoneNumber,
//                DateOfBirth=data.DateOfBirth,
//                LanguageIds = data.Languages.Select(s => s.LanguageID).ToArray()
//            };

//            return Json(user, JsonRequestBehavior.AllowGet);
//        }

//        [HttpPost]
//        public ActionResult UpdateUser(int UserID, UpdateUserLanguageViewModel userLanguageViewModel)
//        {

//            var user = db.Users.Include("Languages").FirstOrDefault(x => x.UserID == UserID);

//            user.Name = userLanguageViewModel.Name;
//            user.Username = userLanguageViewModel.Username;
//            user.Gender = userLanguageViewModel.Gender;
//            user.Summary = userLanguageViewModel.Summary;
//            user.PhoneNumber = userLanguageViewModel.PhoneNumber;
//            user.DateOfBirth = userLanguageViewModel.DateOfBirth;
//            user.Languages.Clear();

//            if (userLanguageViewModel.LanguageIds.Any())
//            {
//                var languages = db.Languages.Where(x => userLanguageViewModel.LanguageIds.Contains(x.LanguageID)).ToList();
//                user.Languages.AddRange(languages);
//            }

//            db.Entry(user).State = System.Data.Entity.EntityState.Modified;
//            db.SaveChanges();
//            return Json("Success");

        //     if (user != null)
        //     {
        //         AllInformation allinfo = new AllInformation();
        //         {
        //             allinfo.Name = user.Name;
        //             allinfo.Gender = user.Gender;
        //             allinfo.PhoneNumber = user.Phone;
        //             allinfo.DateOfBirth = user.DateOfBirth;
        //             allinfo.Summary = user.Summary;
        //             allinfo.Languages = user.Languages;
        //             allinfo.WorkExperiences = user.WorkExperiences;
        //             allinfo.Projects = user.Projects;
        //             allinfo.Login.Username=user.Login.Username;
        //             allinfo.Skills = user.Skills;
        //             allinfo.EducationalDetail = user.EducationalDetails;
        //         }
        //          return View(allinfo);
        //     }
        //     else
        //     {
        //         return new HttpNotFoundResult(); 
        //     }  

        //}


        //        public ActionResult GetProjectById(int Id)
        //        {
        //            var proj = db.Projects.FirstOrDefault(x => x.ProjectId == Id);
        //            var project = new Project
        //            {
        //                ProjectId = proj.ProjectId,
        //                Title = proj.Title,
        //                Duration = proj.Duration,
        //                ProjectRole = proj.ProjectRole,
        //                Description = proj.Description
        //            };
        //            return Json(project, JsonRequestBehavior.AllowGet);
        //        }

        //        [HttpPost]
        //        public ActionResult UpdateProject(int projectId, Project model)
        //        {
        //            var project = db.Projects.FirstOrDefault(x => x.ProjectId == projectId);

        //            project.Title = model.Title;
        //            project.ProjectRole = model.ProjectRole;
        //            project.Description = model.Description;
        //            project.Duration = model.Duration;

        //            db.Entry(project).State = System.Data.Entity.EntityState.Modified;
        //            db.SaveChanges();
        //            return Json("Success");
        //        }


        //        public ActionResult Form()
        //        {
        //            //var summary = user.Summary;
        //            if (Session["UserID"] != null)
        //            {
        //                int id;
        //                var re = Int32.TryParse(Session["UserID"] as String, out id);
        //                var user = db.Users.Where(m => m.UserID == id).FirstOrDefault();
        //                ViewBag.Courses = db.Courses.ToList();

        //                ViewBag.AlreadyDoneCourses = (from course in db.EducationalDetails
        //                                             where course.UserId == id
        //                                             select course.CourseId).ToList();

        //                ViewBag.Languages = db.Languages.ToList();
        //                return PartialView(user);                               
        //            }
        //            return RedirectToAction("Login", "Account");
        //        }

        //        [HttpPost]
        //        public ActionResult DeleteProject(int projectID)
        //        {
        //            var proj = db.Projects.FirstOrDefault(x => x.ProjectId == projectID);
        //            if (proj != null)
        //            {
        //                db.Projects.Remove(proj);
        //                db.SaveChanges();
        //                return Json("Successfully Deleted");
        //            }
        //            else
        //            {
        //                return HttpNotFound();
        //            }

        //        }

        //        public ActionResult GetWorkExperienceById(int id)
        //        {
        //            var workex = db.WorkExperiences.FirstOrDefault(x => x.WorkExperienceid == id);
        //            var workExperience = new WorkExperience
        //            {
        //                WorkExperienceid = workex.WorkExperienceid,
        //                OrganizationName = workex.OrganizationName,
        //                Role = workex.Role,
        //                StartMonth = workex.StartMonth,
        //                StartYear = workex.StartYear,
        //                EndMonth = workex.EndMonth,
        //                EndYear = workex.EndYear,
        //                CurrentlyWorking = workex.CurrentlyWorking
        //            };
        //            return Json(workExperience, JsonRequestBehavior.AllowGet);
        //        }

        //        [HttpPost]
        //        public ActionResult UpdateWorkExperience(int WorkExperienceid, WorkExperience model)
        //        {
        //            var workEx = db.WorkExperiences.FirstOrDefault(x => x.WorkExperienceid == WorkExperienceid);
        //            workEx.OrganizationName = model.OrganizationName;
        //            workEx.Role = model.Role;
        //            workEx.StartMonth = model.StartMonth;
        //            workEx.StartYear = model.StartYear;
        //            workEx.EndMonth = model.EndMonth;
        //            workEx.EndYear = model.EndYear;
        //            workEx.CurrentlyWorking = model.CurrentlyWorking;

        //            db.Entry(workEx).State = System.Data.Entity.EntityState.Modified;
        //            db.SaveChanges();
        //            return Json("Success");
        //        }

        //        [HttpPost]
        //        public ActionResult DeleteWorkExperience(int workExId)
        //        {
        //            var workEx = db.WorkExperiences.FirstOrDefault(x => x.WorkExperienceid == workExId);
        //            if (workEx != null)
        //            {
        //                db.WorkExperiences.Remove(workEx);
        //                db.SaveChanges();
        //                return Json("Successfully Deleted");
        //            }
        //            else
        //            {
        //                return HttpNotFound();
        //            }
        //        }


        

        

        

        //        public JsonResult GetSkill(string term)
        //       {
        //            term = term.Trim();
        //            List<string> skills;

        //            skills = db.Skills.Where(x => x.SkillName.StartsWith(term)).Select(y => y.SkillName).ToList();

        //            return Json(skills, JsonRequestBehavior.AllowGet);
        //        }

        //        [HttpPost]
        //        public ActionResult SaveUserSkills(AddUserSkillsViewModel addUserSkillsViewModel)
        //        {
        //            User user = new User();

        //            user = db.Users.Where(x => x.UserID == addUserSkillsViewModel.UserID).FirstOrDefault();

        //            var skillIdsList = db.Skills.Where(x => addUserSkillsViewModel.SkillNames.Contains(x.SkillName)).Select(m=>m.SkillID).ToList();

        //            List<Skills> refer = new List<Skills>();

        //            foreach (var item in skillIdsList)
        //            {
        //                refer.Add(db.Skills.Where(x => x.SkillID == item).FirstOrDefault());
        //            }

        //            user.Skills.AddRange(refer);

        //            db.SaveChanges();

        //            string message = "SUCCESS";

        //            return Json(new { Message = message, JsonRequestBehavior.AllowGet });

        //        }

        //        public ActionResult DisplayDetails(int[] finalresult)
        //        {
        //            int id = 1;
        //            if (Session["UserID"] != null)
        //            {

        //                var re = Int32.TryParse(Session["UserID"] as String, out id);
        //            }
        //            var ob = db.settings.Where(user => user.UserID == id).FirstOrDefault();
        //            ob.setWorkex = finalresult[0];
        //            ob.setProject = finalresult[1];
        //            ob.setEducation = finalresult[2];
        //            ob.setSkills = finalresult[3];
        //            ob.setContact = finalresult[4];
        //            TryUpdateModel(ob);
        //            db.SaveChanges();
        //            return Json("success",JsonRequestBehavior.AllowGet);
        //        }

        //        public ActionResult settingsValue()
        //        {
        //            int id = 1;
        //            if (Session["UserID"] != null)
        //            {

        //                var re = Int32.TryParse(Session["UserID"] as String, out id);
        //            }
        //            var ob = db.settings.SingleOrDefault(user => user.UserID == id);
        //            UserSetting ob1 = new UserSetting();
        //            ob1.setWorkex = ob.setWorkex;
        //            ob1.UserSettingId = ob.UserSettingId;
        //            ob1.setSkills = ob.setSkills;
        //            ob1.setProject = ob.setProject;
        //            ob1.setEducation = ob.setEducation;
        //            ob1.setContact = ob.setContact;

        //            return Json(ob1, JsonRequestBehavior.AllowGet);
        //        }

        //        [NonAction]
        //        private PublicProfileViewModel GetUserDetails()
        //        {
        //            int id;
        //            var result = Int32.TryParse(Session["UserID"] as String, out id);
        //            if (result)
        //            {
        //                try
        //                {
        //                    // user details
        //                    var userData = db.Users.FirstOrDefault(a => a.UserID == id);
        //                    // User Name
        //                    _uiModel.Name = userData.Name;

        //                    // User Gender
        //                    _uiModel.Gender = userData.Gender;

        //                    // User Gender
        //                    _uiModel.DOB = (userData.DateOfBirth.ToString().Split(' '))[0];

        //                    // User Role
        //                    _uiModel.UserRole = userData.UserRole;

        //                    // User Phone
        //                    _uiModel.PhoneNumber = userData.PhoneNumber;

        //                    // User E-mail
        //                    _uiModel.Email = userData.Username;

        //                    // User Summary
        //                    _uiModel.Summary = userData.Summary;

        //                    // Education Details
        //                    _uiModel.EducationList = (from user in db.EducationalDetails.Where(x => x.UserId == id)
        //                                              select new EducationUIModel
        //                                              {
        //                                                  CourseName = (db.Courses.FirstOrDefault(x => x.CourseId == user.CourseId).CourseName == "10"
        //                                                               || db.Courses.FirstOrDefault(x => x.CourseId == user.CourseId).CourseName == "12") ?
        //                                                               db.Courses.FirstOrDefault(x => x.CourseId == user.CourseId).CourseName + " TH" :
        //                                                               db.Courses.FirstOrDefault(x => x.CourseId == user.CourseId).CourseName,
        //                                                  CGPAOrPercentage = user.CGPAOrPercentage,
        //                                                  Board = user.Board,
        //                                                  Stream = (user.Stream == null) ? "N/A" : user.Stream,
        //                                                  TotalPercentorCGPAValue = user.TotalPercentorCGPAValue,
        //                                                  PassingYear = user.PassingYear
        //                                              }).OrderByDescending(x => x.PassingYear).ToList();

        //                    // Skills
        //                    _uiModel.SkillList = userData.Skills.Select(a => a.SkillName).ToList();

        //                    // Project Details
        //                    _uiModel.ProjectList = (from user in db.Projects.Where(x => x.UserId == id)
        //                                            select new ProjectUIModel
        //                                            {
        //                                                Title = user.Title,
        //                                                Description = user.Description,
        //                                                Duration = user.Duration
        //                                            }).ToList();

        //                    // Work Ex.
        //                    _uiModel.WorkExList = (from user in db.WorkExperiences.Where(x => x.UserID == id)
        //                                           select new WorkExUIModel
        //                                           {
        //                                               OrganizationName = user.OrganizationName,
        //                                               StartMonth = (user.StartMonth <= 9) ? "0" + user.StartMonth : user.StartMonth.ToString(),
        //                                               StartYear = user.StartYear,
        //                                               EndMonth = (user.EndMonth <= 9) ? "0" + user.EndMonth : user.EndMonth.ToString(),
        //                                               EndYear = user.EndYear,
        //                                               Role = user.Role,
        //                                               CurrentlyWorking = user.CurrentlyWorking
        //                                           }).OrderByDescending(x => x.StartYear).ToList();

        //                    // Languages 
        //                    _uiModel.Languages = userData.Languages.Select(a => a.Language).ToList();

        //                }
        //                catch (Exception)
        //                {
        //                    _uiModel.ErrorMsg = "Unexpected error occured, try again...";
        //                }
        //            }
        //            return _uiModel;
        //        }

        //        // GET: Resume/Preview
        //        public ActionResult Preview()
        //        {
        //            if (Session["UserID"] != null)
        //            {
        //                _uiModel = GetUserDetails();
        //                return PartialView(_uiModel);
        //            }
        //            return RedirectToAction("Login", "Account");
        //        }

        //        public ActionResult Search()
        //        {
        //            return PartialView();
        //        }

        //        public ActionResult GetUserSkills()
        //        {
        //            //db.Configuration.ProxyCreationEnabled = false;
        //            List<UserSkillVM> listUserSkills = new List<UserSkillVM>();
        //            listUserSkills = (from user in db.Users.Include("Skills").ToList()
        //                                    select new UserSkillVM
        //                                    {
        //                                        UserID = user.UserID,
        //                                       
        //                                        SkillNames = user.Skills.Select(x => x.SkillName).ToList()
        //                                    }).ToList();


        //            return Json(listUserSkills, JsonRequestBehavior.AllowGet);
        //        }

        //        public ActionResult GetEducationById(int id)
        //        {
        //            var edu = db.EducationalDetails.FirstOrDefault(x => x.EducationalDetailID == id);

        //            var education = new EducationalDetails
        //            {
        //                EducationalDetailID = edu.EducationalDetailID,
        //                CourseId = edu.CourseId,
        //                Board = edu.Board,
        //                PassingYear = edu.PassingYear,
        //                Stream = edu.Stream,
        //                TotalPercentorCGPAValue = edu.TotalPercentorCGPAValue,
        //                CGPAOrPercentage = edu.CGPAOrPercentage
        //            };
        //            return Json(education, JsonRequestBehavior.AllowGet);
        //        }


        //        [HttpPost]
        //        public ActionResult UpdateEducation(int EducationalDetailID, EducationalDetails model)
        //        {
        //            var edu = db.EducationalDetails.FirstOrDefault(x => x.EducationalDetailID == EducationalDetailID);

        //            edu.EducationalDetailID = model.EducationalDetailID;
        //            edu.CourseId = model.CourseId;
        //            edu.PassingYear = model.PassingYear;
        //            edu.Stream = model.Stream;
        //            edu.Board = model.Board;
        //            edu.CGPAOrPercentage = model.CGPAOrPercentage;
        //            edu.TotalPercentorCGPAValue = model.TotalPercentorCGPAValue;

        //            db.Entry(edu).State = System.Data.Entity.EntityState.Modified;
        //            db.SaveChanges();
        //            return Json("Success");
        //        }


        //        [HttpPost]
        //        public ActionResult DeleteEducation(int educationId)
        //        {
        //            var edu = db.EducationalDetails.FirstOrDefault(x => x.EducationalDetailID == educationId);
        //            if (edu != null)
        //            {
        //                db.EducationalDetails.Remove(edu);
        //                db.SaveChanges();
        //                return Json("Successfully Deleted");
        //            }
        //            else
        //            {
        //                return HttpNotFound();
        //            }

        //        }

        //        [HttpPost]
        //        public ActionResult DeleteSkill(int skillId)
        //        {
        //            int id;
        //            var re = Int32.TryParse(Session["UserID"] as String, out id);
        //            var user = db.Users.FirstOrDefault(x => x.UserID == id);
        //            var skill = db.Skills.FirstOrDefault(x => x.SkillID == skillId);

        //            if(user!=null && skill!=null)
        //            {
        //                user.Skills.Remove(skill);

        //                db.SaveChanges();
        //                return Json("Successfully Deleted");
        //            }
        //            else
        //            {
        //                return HttpNotFound();
        //            }
        //        }

        //        [NonAction]
        //        public string RenderViewAsString(string viewName, object model)
        //        {
        //            // create a string writer to receive the HTML code
        //            StringWriter stringWriter = new StringWriter();

        //            // get the view to render
        //            ViewEngineResult viewResult = ViewEngines.Engines.FindView(ControllerContext, viewName, null);
        //            // create a context to render a view based on a model
        //            ViewContext viewContext = new ViewContext(
        //                ControllerContext,
        //                viewResult.View,
        //                new ViewDataDictionary(model),
        //                new TempDataDictionary(),
        //                stringWriter
        //            );

        //            // render the view to a HTML code
        //            viewResult.View.Render(viewContext, stringWriter);

        //            // return the HTML code
        //            return stringWriter.ToString();
        //        }

        //        [HttpGet]
        //        public ActionResult ConvertHtmlPageToPdf(string targetPreview)
        //        {
        //            if (Session["UserID"] != null)
        //            {
        //                _uiModel = GetUserDetails();
        //                // get the HTML code of this view
        //                string htmlToConvert = RenderViewAsString(targetPreview, _uiModel);

        //                // the base URL to resolve relative images and css
        //                String thisPageUrl = this.ControllerContext.HttpContext.Request.Url.AbsoluteUri;
        //                String baseUrl = thisPageUrl.Substring(0, thisPageUrl.Length - "Home/ConvertThisPageToPdf".Length);

        //                // instantiate the HiQPdf HTML to PDF converter
        //                HtmlToPdf htmlToPdfConverter = new HtmlToPdf();

        //                // set PDF page margins 
        //                htmlToPdfConverter.Document.Margins = new PdfMargins(20, 20, 20, 20);

        //                // set browser width
        //                htmlToPdfConverter.BrowserWidth = 740;

        //                // render the HTML code as PDF in memory
        //                byte[] pdfBuffer = htmlToPdfConverter.ConvertHtmlToMemory(htmlToConvert, baseUrl);

        //                // send the PDF file to browser
        //                FileResult fileResult = new FileContentResult(pdfBuffer, "application/pdf");
        //                fileResult.FileDownloadName = "Resume.pdf";

        //                return fileResult;
        //            }
        //            return RedirectToAction("Login", "Account");
        //        }

        //        public ActionResult GetCurrentUser(int id)
        //        {
        //            var data = db.Users.Include("Languages").FirstOrDefault(x => x.UserID == id);
        //            var user = new EditUserLanguageViewModel
        //            {
        //                UserID = data.UserID,
        //                Name=data.Name,
        //                Username = data.Username,
        //                Gender = data.Gender,
        //                Summary=data.Summary,
        //                PhoneNumber=data.PhoneNumber,
        //                DateOfBirth=data.DateOfBirth,
        //                LanguageIds = data.Languages.Select(s => s.LanguageID).ToArray()
        //            };

        //            return Json(user, JsonRequestBehavior.AllowGet);
        //        }

        //        [HttpPost]
        //        public ActionResult UpdateUser(int UserID, UpdateUserLanguageViewModel userLanguageViewModel)
        //        {

        //            var user = db.Users.Include("Languages").FirstOrDefault(x => x.UserID == UserID);

        //            user.Name = userLanguageViewModel.Name;
        //            user.Username = userLanguageViewModel.Username;
        //            user.Gender = userLanguageViewModel.Gender;
        //            user.Summary = userLanguageViewModel.Summary;
        //            user.PhoneNumber = userLanguageViewModel.PhoneNumber;
        //            user.DateOfBirth = userLanguageViewModel.DateOfBirth;
        //            user.Languages.Clear();

        //            if (userLanguageViewModel.LanguageIds.Any())
        //            {
        //                var languages = db.Languages.Where(x => userLanguageViewModel.LanguageIds.Contains(x.LanguageID)).ToList();
        //                user.Languages.AddRange(languages);
        //            }

        //            db.Entry(user).State = System.Data.Entity.EntityState.Modified;
        //            db.SaveChanges();
        //            return Json("Success");
        //}

