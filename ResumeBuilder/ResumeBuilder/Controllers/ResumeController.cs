using ResumeBuilder.Models;
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
        public ResumeController()
        {
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

                ViewBag.Projects = projects;

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
               // Role = proj.Role,
                Description = proj.Description
            };
            return Json(project, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UpdateProject(int projectId, Project model)
        {
            var project = db.Projects.FirstOrDefault(x => x.ProjectId == projectId);

            project.Title = model.Title;
            //project.Role = model.Role;
            project.Description = model.Description; ;
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
        //public ActionResult SaveBasicInfo(User user)
        //{
        //    try
        //    {
        //        var usr = db.Users.SingleOrDefault(u => u.UserID == user.UserID);
        //        usr.Summary = user.Summary;

        //        //db.Users.Add(user);
        //        db.SaveChanges();
        //        return RedirectToAction("Form", "Resume");
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e);
        //    }
        //    return RedirectToAction("Login", "Account");
        //}

        public ActionResult LogOff()
        {
            if (Session["UserID"] != null)
            {
                Session.Remove("UserID");
                Session.RemoveAll();
            }

            return RedirectToAction("Login", "Account");
        }
    }
}