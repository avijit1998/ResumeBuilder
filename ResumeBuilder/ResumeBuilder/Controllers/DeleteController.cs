using ResumeBuilder.Helpers;
using ResumeBuilder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ResumeBuilder.Controllers
{
    [AuthorizeIfSessionExists]
    public class DeleteController : Controller
    {
        private ResumeBuilderDBContext db;
        public DeleteController()
        {
            db = new ResumeBuilderDBContext();
        }

        [HttpDelete]
        public ActionResult DeleteProject(int id)
        {
            try
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
            catch (Exception)
            {
                return HttpNotFound();
            }
        }

        [HttpDelete]
        public ActionResult DeleteWorkExperience(int id)
        {
            try
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
            catch (Exception)
            {
                return HttpNotFound();
            }
        }


        [HttpDelete]
        public ActionResult DeleteEducation(int educationId)
        {
            try
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
            catch (Exception)
            {
                return HttpNotFound();
            }

        }

        [HttpDelete]
        public ActionResult DeleteSkill(int userID, int skillID)
        {
            try
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
            catch (Exception)
            {
                return HttpNotFound();
            }
        }
    }
}