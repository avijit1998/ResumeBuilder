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
    public class SearchController : Controller
    {
        private ResumeBuilderDBContext db;
        public SearchController()
        {
            db = new ResumeBuilderDBContext();
        }

        public ActionResult GetSearchPartialView()
        {
            return PartialView("Search");
        }

        public ActionResult GetUserSkills()
        {
            try
            {
                List<UserSkillVM> listUserSkills = new List<UserSkillVM>();
                listUserSkills = (from user in db.UserDetails.ToList()
                                  select new UserSkillVM
                                  {
                                      UserID = user.UserID,
                                      UserName = user.Name,
                                      SkillNames = user.Skills.Select(x => x.SkillName).ToList()
                                  }).ToList();

                return Json(listUserSkills, JsonRequestBehavior.AllowGet);

            }
            catch (Exception)
            {
                return new HttpNotFoundResult();
            }
        }
    }
}