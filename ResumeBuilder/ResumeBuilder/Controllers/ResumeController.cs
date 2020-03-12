using ResumeBuilder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
            return RedirectToAction("Login","Account");
        }

        [HttpPost]
        public ActionResult SaveBasicInformation(User user)
        {
            var userFromDb=db.Users.FirstOrDefault(u=>u.UserID==user.UserID);
            userFromDb.Summary = user.Summary;
            db.SaveChanges();
            string message = "SUCCESS";
            return Json(new { Message = message, JsonRequestBehavior.AllowGet });
        }
        
        public ActionResult SaveBasicInfo(User user)
        {
            try
            {
                var usr = db.Users.SingleOrDefault(u => u.UserID == user.UserID);
                usr.Summary = user.Summary;

                //db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Form", "Resume");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return RedirectToAction("Login", "Account");
        }

        public ActionResult DisplayDetails( int[] finalresult)
        {
            int id=1;
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
            var ob = db.settings.SingleOrDefault(user => user.UserSettingId == id);
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