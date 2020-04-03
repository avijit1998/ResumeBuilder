using HiQPdf;
using ResumeBuilder.Helpers;
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
        private ProfileVM GetUserDetails(int id)
        {
            var uiModel = new ProfileVM();
            try
            {
                // user details
                var userData = db.UserDetails.FirstOrDefault(a => a.UserID == id);

                if (userData != null)
                {
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
                    uiModel.EducationStatus = (userData.Setting.EducationalDetailsStatus) ? 1 : 0;
                    uiModel.EducationList = (from user in userData.EducationalDetails
                                             select new EducationVM
                                             {
                                                 CourseName = user.Course.CourseName,
                                                 CGPAOrPercentage = user.CGPAOrPercentage,
                                                 Board = user.BoardOrUniversity,
                                                 Stream = (user.Stream == null) ? "N/A" : user.Stream,
                                                 TotalPercentorCGPAValue = user.TotalPercentageOrCGPAValue,
                                                 PassingYear = user.PassingYear
                                             }).OrderByDescending(x => x.PassingYear).ToList();

                    // Skills
                    uiModel.SkillStatus = (userData.Setting.SkillsDetailsStatus) ? 1 : 0;
                    uiModel.SkillList = userData.Skills.Select(a => a.SkillName).ToList();

                    // Project Details
                    uiModel.ProjectStatus = (userData.Setting.ProjectDetailsStatus) ? 1 : 0;
                    uiModel.ProjectList = (from user in userData.Projects
                                           select new ProjectVM
                                           {
                                               Title = user.ProjectTitle,
                                               Description = user.Description,
                                               Duration = user.DurationInMonth
                                           }).ToList();

                    // Work Experience
                    uiModel.WorkExperienceStatus = (userData.Setting.WorkExperienceStatus) ? 1 : 0;
                    uiModel.WorkExList = (from user in userData.WorkExperiences
                                          select new WorkExperienceVM
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
                    uiModel.LanguageStatus = (userData.Setting.LanguagesStatus) ? 1 : 0;
                    uiModel.Languages = userData.Languages.Select(a => a.LanguageName).ToList();
                }
            }
            catch (Exception)
            {
                uiModel.ErrorMsg = "Unexpected error occured, try again...";
            }
            return uiModel;
        }

        // GET: Profile/Preview
        [HttpGet]
        [AuthorizeIfSessionExists]
        public ActionResult Preview()
        {
            ProfileVM uiModel = new ProfileVM();
            try
            {
                var sessionId = Session["UserID"];
                int id = (Int32)sessionId;
                uiModel = GetUserDetails(id);
                if (uiModel == null)
                {
                    uiModel.ErrorMsg = "Unexpected error occured, try again...";
                }
            }
            catch (Exception)
            {
                uiModel.ErrorMsg = "Unexpected error occured, try again...";
            }
            return PartialView(uiModel);
        }

        [NonAction]
        public string RenderViewAsString(string viewName, ProfileVM model)
        {
            try
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
            catch (Exception)
            {
                return "";
            }
        }

        // Get: Profile/ConvertHtmlPageToPdf/targetPreview
        [HttpGet]
        [AuthorizeIfSessionExists]
        public ActionResult ConvertHtmlPageToPdf(string targetPreview)
        {
            try
            {
                var sessionId = Session["UserID"];
                int id = (Int32)sessionId;
                var uiModel = GetUserDetails(id);
                if (uiModel == null)
                {
                    return HttpNotFound();
                }
                // get the HTML code of this view
                string htmlToConvert = RenderViewAsString(targetPreview, uiModel);
                if (htmlToConvert == "")
                {
                    return HttpNotFound();
                }

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
            }
            catch (Exception)
            {
                return HttpNotFound();
            }
        }

        // Get: Profile/PublicProfile/id
        [HttpGet]
        public ActionResult PublicProfile(int id)
        {
            var uiModel = GetUserDetails(id);
            if (uiModel == null)
            {
                return HttpNotFound();
            }
            return View(uiModel);
        }
    }
}