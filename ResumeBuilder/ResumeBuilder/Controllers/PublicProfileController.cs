using ResumeBuilder.Models;
using ResumeBuilder.ViewModels;
using System.Linq;
using System.Web.Mvc;

namespace ResumeBuilder.Controllers
{
    public class PublicProfileController : Controller
    {
        private ResumeBuilderConnection _context;
        private PublicProfileViewModel _uiModel;

        public PublicProfileController()
        {
            _context = new ResumeBuilderConnection();
            _uiModel = new PublicProfileViewModel();
        }

        // GET: PublicProfile/Index/id
        public ActionResult Index(int id)
        {
            // User Name
            _uiModel.Name = _context.Users.FirstOrDefault(a => a.UserID == id).Name;

            // User Role
            _uiModel.UserRole = "Web Developer";
            
            // User Phone
            _uiModel.PhoneNumber = _context.Users.FirstOrDefault(a => a.UserID == id).PhoneNumber;

            // User E-mail
            _uiModel.Email = _context.Users.FirstOrDefault(a => a.UserID == id).Username;

            //User Linkedin Link
            _uiModel.LinkedinLink = "https://www.linkedin.com/user";

            // User Summary
            _uiModel.Summary = "Oh, I misunderstood the problem. ResumeBuilder ResumeBuilder Setting a padding on, ResumeBuilder ResumeBuilder bin the padding won't help you.";
            
            //Education Details
            ViewBag.education = 1;
            
            
            return View(_uiModel);
            //var data = _context.Users.ToList(),return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}