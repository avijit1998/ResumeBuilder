using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using ResumeBuilder.Models;
using System.Security.Cryptography;
using ResumeBuilder.ViewModels;

namespace ResumeBuilder.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        
        public AccountController()
        {
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            else
            {
                try
                {
                    ResumeBuilderConnection dbContext = new ResumeBuilderConnection();
                    var user = dbContext.Users.Where(m => m.Username == model.UserName).FirstOrDefault();
                    string savedPasswordHash = user.Password;
                    byte[] hashBytes = Convert.FromBase64String(savedPasswordHash);
                    byte[] salt = new byte[16];
                    Array.Copy(hashBytes, 0, salt, 0, 16);
                    var pbkdf2 = new Rfc2898DeriveBytes(model.Password, salt, 10000);
                    byte[] hash = pbkdf2.GetBytes(20);
                    for (int i = 0; i < 20; i++)
                    {
                        if (hashBytes[i + 16] != hash[i])
                        {
                            throw new UnauthorizedAccessException();                            
                        }
                    }
                    Session["UserID"] = user.UserID.ToString();
                    Session["UserName"] = user.Username.ToString();
                    return UserDashBoard(user);
                }
                catch(Exception)
                {
                    ModelState.AddModelError("","Invalid Username or Password.");
                    return View(model);
                }
            }
        }

        public ActionResult UserDashBoard(User user)  
        {  
            if (Session["UserID"] != null)  
            {  
                return RedirectToAction("Form", "Resume", user);  
            } else  
            {  
                return RedirectToAction("Login");  
            }  
        }    

        
        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(User model)
        {
            ResumeBuilderConnection dbContext = new ResumeBuilderConnection();
            User u = dbContext.Users.Where(m => m.Username == model.Username).FirstOrDefault();
            if (u != null)
            {
                ModelState.AddModelError("", "User already exists.");
                return View(model);
            }
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);
            var pbkdf1 = new Rfc2898DeriveBytes(model.Password, salt, 10000);
            byte[] hash = pbkdf1.GetBytes(20);
            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);
            string savedPasswordHash = Convert.ToBase64String(hashBytes);

            if (ModelState.IsValid)
            {
                ResumeBuilderConnection db = new ResumeBuilderConnection();
                User user = new User
                {
                    Username = model.Username,
                    Password = savedPasswordHash,
                    ConfirmPassword = savedPasswordHash
                };
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Login", "Account");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            //AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        
    }
}