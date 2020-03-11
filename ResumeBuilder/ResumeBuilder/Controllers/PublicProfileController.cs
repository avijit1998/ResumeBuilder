﻿using ResumeBuilder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ResumeBuilder.Controllers
{
    public class PublicProfileController : Controller
    {
        private ResumeBuilderConnection _context;   

        public PublicProfileController()
        {
            _context = new ResumeBuilderConnection();
        }

        // GET: PublicProfile/Index/id
        public ActionResult Index(int id)
        {
            // User Name
            ViewBag.profName = _context.Users.FirstOrDefault(a => a.UserID == id).Name;
            // User Role
            ViewBag.profRole = "Web Developer";
            // User Phone
            ViewBag.profPhone = _context.Users.FirstOrDefault(a => a.UserID == id).PhoneNumber;
            // User E-mail
            ViewBag.profEmail = _context.Users.FirstOrDefault(a => a.UserID == id).Username;
            ViewBag.profLinkedin = "https://www.linkedin.com/user";
            return View();
            var data = _context.Users.ToList();
            //return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}