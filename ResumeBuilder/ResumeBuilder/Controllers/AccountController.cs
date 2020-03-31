using System;
using System.Linq;
using System.Web.Mvc;
using ResumeBuilder.Models;
using System.Security.Cryptography;
using ResumeBuilder.ViewModels;
using ResumeBuilder.Helpers;
using System.Text;

namespace ResumeBuilder.Controllers
{
	[Authorize]
	public class AccountController : Controller
	{
        [HttpGet]
        public ActionResult LogOff()
        {
            Session.Abandon();
            return RedirectToAction("Login", "Account");
        }
        
		// GET: /Account/Login
		[AllowAnonymous]
		public ActionResult Login()
		{
			if (Session["UserID"] == null)
			{
				return View();
			}
			else
			{
				Session.Abandon();
				return View();
			}
			
		}

		//POST: /Account/Login
		[HttpPost]
		[AllowAnonymous]
		[ValidateAntiForgeryToken]
		public ActionResult Login(LoginDetailsViewModel loginData)
		{
			if (!ModelState.IsValid)
			{
				ModelState.AddModelError("", "Invalid Username or Password.");
				return View(loginData);
			}

			ResumeBuilderDBContext dbContext = new ResumeBuilderDBContext();
			
			if (!dbContext.Logins.Any(m => m.Username == loginData.UserName))
			{
				ModelState.AddModelError("", "Username does not exist.");
				return View(loginData);
			}
			else
			{
				try
				{
					var userLoginDetails = dbContext.Logins.FirstOrDefault(m => m.Username == loginData.UserName);
					var salt = userLoginDetails.Salt;
          string enterPassword = loginData.Password;
          string savedPassword = userLoginDetails.Password;
					
          if (PasswordSecurity.IsValid(enterPassword,salt,savedPassword))
					{
						if (Session.Count == 0)
						{
							Session["UserID"] = userLoginDetails.UserID;
							return RedirectToAction("Index", "Resume");
						}
						else
						{
							ModelState.AddModelError("", "Session already exists. Try Again.");
							return View(loginData);
						}
					}
					else
					{
						throw new UnauthorizedAccessException();
					}
				}
				catch (UnauthorizedAccessException)
				{
					ModelState.AddModelError("", "Wrong Password. Try Again.");
					return View(loginData);
				}
				catch (Exception)
				{
					ModelState.AddModelError("", "Oops!!! Something went wrong. Try Again.");
					return View(loginData);
				}
			}
		}

		// GET: /Account/Register
		[AllowAnonymous]
		public ActionResult Register()
		{
			return View();
		}

		// POST: /Account/Register
		[HttpPost]
		[AllowAnonymous]
		[ValidateAntiForgeryToken]
		public ActionResult Register(LoginDetailsViewModel registrationDetails)
		{
			if (!ModelState.IsValid)
			{
				return View(registrationDetails);
			}

			ResumeBuilderDBContext dbContext = new ResumeBuilderDBContext();

			if(dbContext.Logins.Any(m => m.Username == registrationDetails.UserName))
			{
				ModelState.AddModelError("", "User already exists.");
				return View(registrationDetails);
			}
			else
			{
				try
				{
                    string salt = PasswordSecurity.GenerateSalt();
                    string hashedPassword = PasswordSecurity.HashPassword(registrationDetails.Password, salt);

					UserDetails newUser = new UserDetails
					{
						DateOfBirth = DateTime.Now                    
					};

					Login newLogin = new Login
					{
						Username = registrationDetails.UserName,
						Password = hashedPassword,
						Salt = salt,
						UserDetails = newUser
					};

					dbContext.Logins.Add(newLogin);
					dbContext.SaveChanges();
				}
				catch(Exception)
				{
					ModelState.AddModelError("", "Something went wrong try again.");
					return View(registrationDetails);
				}
				
			}

			// If we got this far, something failed, redisplay form
			return View(registrationDetails);
		}

		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
		}
	}
}