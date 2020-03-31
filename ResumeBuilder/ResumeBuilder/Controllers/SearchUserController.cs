using ResumeBuilder.Models;
using ResumeBuilder.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ResumeBuilder.Controllers
{
    public class SearchUserController : Controller
    {
        private ResumeBuilderDBContext db;
        public SearchUserController()
        {
            db = new ResumeBuilderDBContext();
        }

        public ActionResult Search()
        {
            return PartialView();
        }

        public ActionResult GetUser()
        {
            //db.Configuration.ProxyCreationEnabled = false;
            List<UserSkillVM> listUserSkills = new List<UserSkillVM>();
            listUserSkills = (from user in db.UserDetails.Include("Skills").ToList()
                              select new UserSkillVM
                              {
                                  UserID = user.UserID,
                                  UserName = user.Name,
                                  SkillNames = user.Skills.Select(x => x.SkillName).ToList()
                              }).ToList();


            return Json(listUserSkills, JsonRequestBehavior.AllowGet);
        }
    }
}