using HiQPdf;
using ResumeBuilder.Models;
using ResumeBuilder.ViewModels;
using System;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace ResumeBuilder.Controllers
{
    public class ProfileController : Controller
    {
        private ResumeBuilderDBContext db;
        public ProfileController()
        {
            db = new ResumeBuilderDBContext();
        }
       
        [NonAction]
        private ProfileViewModel GetUserDetails()
        {
            var uiModel = new ProfileViewModel();
            int id = 1;
            //var result = Int32.TryParse(Session["UserID"] as String, out id);
            if (true)
            {
                try
                {
                    // user details
                    var userData = db.UserDetails.FirstOrDefault(a => a.UserID == id);

                    // User Name
                    uiModel.Name = userData.Name;

                    // User Gender
                    uiModel.Gender = userData.Gender;

                    // User Gender
                    uiModel.DOB = userData.DateOfBirth.ToShortDateString();

                    // User Phone
                    uiModel.PhoneNumber = userData.Phone;

                    // User E-mail
                    uiModel.Email = userData.Login.Username;

                    // User Summary
                    uiModel.Summary = userData.Summary;

                    // Education Details
                    uiModel.EducationList = (from user in userData.EducationalDetails
                                              select new EducationUIModel
                                              {
                                                  CourseName = user.Course.CourseName,
                                                  CGPAOrPercentage = user.CGPAOrPercentage,
                                                  Board = user.BoardOrUniversity,
                                                  Stream = (user.Stream == null) ? "N/A" : user.Stream,
                                                  TotalPercentorCGPAValue = user.TotalPercentageOrCGPAValue,
                                                  PassingYear = user.PassingYear
                                              }).OrderByDescending(x => x.PassingYear).ToList();

                    // Skills
                    uiModel.SkillList = userData.Skills.Select(a => a.SkillName).ToList();

                    // Project Details
                    uiModel.ProjectList = (from user in userData.Projects
                                            select new ProjectUIModel
                                            {
                                                Title = user.ProjectTitle,
                                                Description = user.Description,
                                                Duration = user.DurationInMonth
                                            }).ToList();

                    // Work Ex.
                    uiModel.WorkExList = (from user in userData.WorkExperiences
                                           select new WorkExUIModel
                                           {
                                               OrganizationName = user.OrganizationName,
                                               StartMonth = (user.StartMonth <= 9) ? "0" + user.StartMonth : user.StartMonth.ToString(),
                                               StartYear = user.StartYear,
                                               EndMonth = (user.EndMonth <= 9) ? "0" + user.EndMonth : user.EndMonth.ToString(),
                                               EndYear = user.EndYear,
                                               Role = user.Designation,
                                               CurrentlyWorking = user.IsCurrentlyWorking
                                           }).OrderByDescending(x => x.StartYear).OrderByDescending(y => y.StartMonth).ToList();

                    // Languages 
                    uiModel.Languages = userData.Languages.Select(a => a.LanguageName).ToList();

                }
                catch (Exception)
                {
                    uiModel.ErrorMsg = "Unexpected error occured, try again...";
                }
            }
            return uiModel;
        }

        // GET: Resume/Preview
        [HttpGet]
        public ActionResult Preview()
        {
            //if (Session["UserID"] != null)
            //{
                var uiModel = GetUserDetails();
                return PartialView(uiModel);
            //}
            //return RedirectToAction("Login", "Account");
        }

        [NonAction]
        public string RenderViewAsString(string viewName, object model)
        {
            // create a string writer to receive the HTML code
            StringWriter stringWriter = new StringWriter();

            // get the view to render
            ViewEngineResult viewResult = ViewEngines.Engines.FindView(ControllerContext, viewName, null);
            // create a context to render a view based on a model
            ViewContext viewContext = new ViewContext(
                ControllerContext,
                viewResult.View,
                new ViewDataDictionary(model),
                new TempDataDictionary(),
                stringWriter
            );

            // render the view to a HTML code
            viewResult.View.Render(viewContext, stringWriter);

            // return the HTML code
            return stringWriter.ToString();
        }

        [HttpGet]
        public ActionResult ConvertHtmlPageToPdf(string targetPreview)
        {
            //if (Session["UserID"] != null)
            //{
                var uiModel = GetUserDetails();
                // get the HTML code of this view
                string htmlToConvert = RenderViewAsString(targetPreview, uiModel);

                // the base URL to resolve relative images and css
                String thisPageUrl = this.ControllerContext.HttpContext.Request.Url.AbsoluteUri;
                String baseUrl = thisPageUrl.Substring(0, thisPageUrl.Length - "Home/ConvertThisPageToPdf".Length);

                // instantiate the HiQPdf HTML to PDF converter
                HtmlToPdf htmlToPdfConverter = new HtmlToPdf();

                // set PDF page margins 
                htmlToPdfConverter.Document.Margins = new PdfMargins(20, 20, 20, 20);

                // set browser width
                htmlToPdfConverter.BrowserWidth = 740;

                // render the HTML code as PDF in memory
                byte[] pdfBuffer = htmlToPdfConverter.ConvertHtmlToMemory(htmlToConvert, baseUrl);

                // send the PDF file to browser
                FileResult fileResult = new FileContentResult(pdfBuffer, "application/pdf");
                fileResult.FileDownloadName = "Resume.pdf";
                return fileResult;
            //}
            //return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        public ActionResult PublicProfile()
        {
            var uiModel = GetUserDetails();
            return View(uiModel);
        }
    }
}