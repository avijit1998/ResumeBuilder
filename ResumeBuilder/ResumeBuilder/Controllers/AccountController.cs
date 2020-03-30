////using System;
////using System.Linq;
////using System.Web.Mvc;
////using ResumeBuilder.Models;
////using System.Security.Cryptography;
////using ResumeBuilder.ViewModels;
////using ResumeBuilder.Models;

////namespace ResumeBuilder.Controllers
////{
////    [Authorize]
////    public class AccountController : Controller
////    {
        
////        public AccountController()
////        {
////        }

////        [HttpGet]
////        [AllowAnonymous]
////        public ActionResult LogOff()
////        {
////            Session.RemoveAll();
////            Session.Clear();
////            Session.Abandon();
////            return RedirectToAction("Login", "Account");
////        }

////        //[HttpPost]
////        //[AllowAnonymous]
////        //[ValidateAntiForgeryToken]
////        //[ActionName("LogOff")]
////        //public void LogOffPost()
////        //{
////        //    Session.RemoveAll();
////        //    Session.Clear();
////        //    Session.Abandon();
////        //}

////        // GET: /Account/Login
////        [AllowAnonymous]
////        public ActionResult Login()
////        {
////            return View();
////        }

////        //
////        // POST: /Account/Login
////        [HttpPost]
////        [AllowAnonymous]
////        [ValidateAntiForgeryToken]
////        public ActionResult Login(LoginViewModel model)
////        {
////            if (!ModelState.IsValid)
////            {
////                return View(model);
////            }
////            else
////            {
////                try
////                {
////                    ResumeBuilderDBContext dbContext = new ResumeBuilderDBContext();
////                    var user = dbContext.Users.Where(m => m.Username == model.UserName).FirstOrDefault();
////                    string savedPasswordHash = user.Password;
////                    byte[] hashBytes = Convert.FromBase64String(savedPasswordHash);
////                    byte[] salt = new byte[16];
////                    Array.Copy(hashBytes, 0, salt, 0, 16);
////                    var pbkdf2 = new Rfc2898DeriveBytes(model.Password, salt, 10000);
////                    byte[] hash = pbkdf2.GetBytes(20);
////                    for (int i = 0; i < 20; i++)
////                    {
////                        if (hashBytes[i + 16] != hash[i])
////                        {
////                            throw new UnauthorizedAccessException();                            
////                        }
////                    }
////                    Session["UserID"] = user.UserID.ToString();
////                    Session["UserName"] = user.Username.ToString();
////                    return UserDashBoard(user);
////                }
////                catch(Exception)
////                {
////                    ModelState.AddModelError("","Invalid Email Address or Password.");
////                    return View(model);
////                }
////            }
////        }

////        public ActionResult UserDashBoard(UserDetail
//user)  
////        {  
////            if (Session["UserID"] != null)  
////            {
////                return RedirectToAction("Index", "Resume");
////                //return View("~/Views/Resume/Index.cshtml");
////                //return RedirectToAction("Form", "Resume");  
////            }
////            else if (Session.Count == 0)
////            {
////                return RedirectToAction("Login");
////            }
////            else
////            {
////                return RedirectToAction("Login");
////            }
////        }    

        
////        //
////        // GET: /Account/Register
////        [AllowAnonymous]
////        public ActionResult Register()
////        {
////            return View();
////        }

////        //
////        // POST: /Account/Register
////        [HttpPost]
////        [AllowAnonymous]
////        [ValidateAntiForgeryToken]
////        public ActionResult Register(UserDetail model)
////        {
////            ResumeBuilderDBContext dbContext = new ResumeBuilderDBContext();
            
////            if (dbContext.Users.ToList().Any())
////            {
////                User u = dbContext.Users.Where(m => m.Username == model.Username).FirstOrDefault();
////                if (u != null)
////                {
////                    ModelState.AddModelError("", "User already exists.");
////                    return View(model);
////                }
////            }

////            byte[] salt;
////            new RNGCryptoProvider().GetBytes(salt = new byte[16]);
////            var pbkdf1 = new Rfc2898DeriveBytes(model.Password, salt, 10000);
////            byte[] hash = pbkdf1.GetBytes(20);
////            byte[] hashBytes = new byte[36];
////            Array.Copy(salt, 0, hashBytes, 0, 16);
////            Array.Copy(hash, 0, hashBytes, 16, 20);
////            string savedPasswordHash = Convert.ToBase64String(hashBytes);

////            if (ModelState.IsValid)
////            {
////                ResumeBuilderDBContext db = new ResumeBuilderDBContext();
////                User user = new User
////                {
////                    Username = model.Username,
////                    Password = savedPasswordHash,
////                    ConfirmPassword = savedPasswordHash
////                };
////                UserSetting ob = new UserSetting
////                {
////                    UserID = user.UserID,
////                    User=user,
////                    setContact=0,
////                    setEducation=0,
////                    setProject=0,
////                    setSkills=0,
////                    setWorkex=0,

////                };
////                db.Users.Add(user);
////                db.settings.Add(ob);
////                db.SaveChanges();
////                return RedirectToAction("Login", "Account");
////            }

////            // If we got this far, something failed, redisplay form
////            return View(model);
////        }

        

////        protected override void Dispose(bool disposing)
////        {
////            base.Dispose(disposing);
////        }

        
////    }
////}