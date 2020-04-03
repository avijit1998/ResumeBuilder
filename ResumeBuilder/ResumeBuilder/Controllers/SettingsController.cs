using ResumeBuilder.Helpers;
using ResumeBuilder.Models;
using ResumeBuilder.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ResumeBuilder.Controllers
{
    [AuthorizeIfSessionExists]
    public class SettingsController : Controller
    {
        private ResumeBuilderDBContext db;
        
        public SettingsController()
        {
            db = new ResumeBuilderDBContext();
        }
        
        public ActionResult SetUserSettingStatus()
        {
            try
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
            catch (Exception)
            {
                return HttpNotFound();
            }
        }

        [HttpPost]
        public ActionResult SaveSettingStatus(SettingsDetailsVM settingStatus)
        {

            try
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
                return Json("success");
            }
            catch (Exception)
            {
                return HttpNotFound();
            }
        }
    }
}