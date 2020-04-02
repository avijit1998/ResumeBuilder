using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ResumeBuilder.ViewModels
{
    public class SettingsDetailsVM
    {
        public bool WorkExperienceStatus { get; set; }

        public bool EducationalDetailsStatus { get; set; }

        public bool ProjectDetailsStatus { get; set; }

        public bool SkillsDetailsStatus { get; set; }

        public bool LanguagesStatus { get; set; }
    }
}