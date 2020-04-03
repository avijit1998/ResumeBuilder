using ResumeBuilder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ResumeBuilder.ViewModels
{
    public class UserSkillVM
    {
        public int UserID { get; set; }

        public string UserName { get; set; }
        
        public List<String> SkillNames { get; set; }
    }
}