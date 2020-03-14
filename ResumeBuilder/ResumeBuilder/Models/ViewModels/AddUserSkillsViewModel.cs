using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ResumeBuilder.Models.ViewModels
{
    public class AddUserSkillsViewModel
    {
        public int UserID { get; set; }

        public string[] SkillNames { get; set; }

        public List<int> SkillIds { get; set; }
    }
}