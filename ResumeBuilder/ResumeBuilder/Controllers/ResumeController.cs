﻿using ResumeBuilder.Models;
using ResumeBuilder.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ResumeBuilder.ViewModels;
using ResumeBuilder.Helpers;


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

        public ActionResult ShowData()
        {
            if (Session["UserID"] != null)
            {
                int id;
                var re = Int32.TryParse(Session["UserID"] as String, out id);
                var user = db.Users.Where(m => m.UserID == id).FirstOrDefault();
                var projects = db.Projects.Where(m => m.UserId == id).ToList();
                var workExperiences = db.WorkExperiences.Where(m => m.UserID == id).ToList();
               
                ViewBag.Projects = projects;
                ViewBag.WorkExperiences = workExperiences;

                return View(user);
            }
            return RedirectToAction("Login", "Account");
            
        }

        //public ActionResult GetAllData()
        //{
        //    if (Session["UserID"] != null)
        //    {
        //        int id;
        //        var re = Int32.TryParse(Session["UserID"] as String, out id);
        //        var userFromDb = db.Users.Include("Projects").Include("WorkExperiences")
        //                        .Include("EducationalDetails").Include("UsersLanguages.Language")
        //                        .Include("UsersSkills.Skill").FirstOrDefault(x => x.UserID == id);

                
        //        if (userFromDb != null)
        //        {

        //            var data=new AllInformation()
        //            {
        //                UserInfo = new UserInfoVM
        //                {
        //                    Name = userFromDb.Name,
        //                    Username=userFromDb.Username,
        //                    PhoneNumber=userFromDb.PhoneNumber,
        //                    Summary=userFromDb.Summary,
        //                    Gender=userFromDb.Gender
        //                }
        //            };

        //            data.ProjectInfo.AddRange(CustomMapper.Map(userFromDb.Projects));
        //            data.WorkExperiences.AddRange(CustomMapper.Map(userFromDb.WorkExperiences));
        //            //data.Languages.AddRange(CustomMapper.Map(userFromDb.));
        //            //data.Skills.AddRange(CustomMapper.Map(userFromDb.Skills));

        //            return Json(data, JsonRequestBehavior.AllowGet);
        //        }
                
        //    }
            
        //    return RedirectToAction("Login", "Account");
        //}

        public ActionResult GetProjectById(int Id)
        {
            var proj = db.Projects.FirstOrDefault(x => x.ProjectId == Id);
            var project = new Project
            {
                ProjectId = proj.ProjectId,
                Title = proj.Title,
                Duration = proj.Duration,
                ProjectRole = proj.ProjectRole,
                Description = proj.Description
            };
            return Json(project, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UpdateProject(int projectId, Project model)
        {
            var project = db.Projects.FirstOrDefault(x => x.ProjectId == projectId);

            project.Title = model.Title;
            project.ProjectRole = model.ProjectRole;
            project.Description = model.Description; 
            project.Duration = model.Duration;

            db.Entry(project).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return Json("Success");
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
                return View(user);                               
            }
            return RedirectToAction("Login", "Account");
        }

        [HttpPost]
        public ActionResult DeleteProject(int projectID)
        {
            var proj = db.Projects.FirstOrDefault(x => x.ProjectId == projectID);
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

        public ActionResult GetWorkExperienceById(int workExperienceId)
        {
            var workex = db.WorkExperiences.FirstOrDefault(x => x.WorkExperienceid == workExperienceId);
            var workExperience = new WorkExperience
            {
                WorkExperienceid = workex.WorkExperienceid,
                OrganizationName = workex.OrganizationName,
                Role = workex.Role,
                StartMonth = workex.StartMonth,
                StartYear = workex.StartYear,
                EndMonth = workex.EndMonth,
                EndYear = workex.EndYear,
                CurrentlyWorking = workex.CurrentlyWorking
            };
            return Json(workExperience, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UpdateWorkExperience(int workExpeienceId, WorkExperience model)
        {
            var workEx = db.WorkExperiences.FirstOrDefault(x => x.WorkExperienceid == workExpeienceId);
            workEx.OrganizationName = model.OrganizationName;
            workEx.Role = model.Role;
            //workEx.StartMonth = model.StartMonth;
            //workEx.StartYear = model.StartYear;
            //workEx.EndMonth = model.EndMonth;
            //workEx.EndYear = model.EndYear;
            //workEx.CurrentlyWorking = model.CurrentlyWorking;

            db.Entry(workEx).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return Json("Success");
        }

        [HttpDelete]
        public ActionResult DeleteWorkExperience(int workExId)
        {
            var workEx = db.WorkExperiences.FirstOrDefault(x => x.WorkExperienceid == workExId);
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
            var userFromDb = db.Users.FirstOrDefault(u => u.UserID == user.UserID);

            userFromDb.Summary = user.Summary;
            db.SaveChanges();

            string message = "SUCCESS";

            return Json(new { Message = message, JsonRequestBehavior.AllowGet });
        }

        [HttpPost]
        public ActionResult SaveBasicInformation(User user)
        {
            var userFromDb = db.Users.FirstOrDefault(u => u.UserID == user.UserID);

            userFromDb.Name = user.Name;
            userFromDb.Gender = user.Gender;
            userFromDb.PhoneNumber = user.PhoneNumber;
            userFromDb.DateOfBirth = user.DateOfBirth;

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
            List<string> skills;

            skills = db.Skills.Where(x => x.Skill.StartsWith(term)).Select(y => y.Skill).ToList();

            return Json(skills, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DisplayDetails(int[] finalresult)
        {
            int id = 1;
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
            var ob = db.settings.SingleOrDefault(user => user.UserID == id);
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