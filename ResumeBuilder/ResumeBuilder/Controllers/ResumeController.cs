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
        ResumeBuilderConnection dbContext = new ResumeBuilderConnection();
        // GET: Resume
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Form()
        {
            if (Session["UserID"] != null)
            {
               
                int id;
                var re = Int32.TryParse(Session["UserID"] as String, out id);
                var user = dbContext.Users.Where(m => m.UserID == id).FirstOrDefault();
                return View(user);                               
            }

            return RedirectToAction("Login","Account");
        }
    }
}