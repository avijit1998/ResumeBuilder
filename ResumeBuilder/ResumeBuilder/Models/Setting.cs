using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ResumeBuilder.Models
{
    public class Setting
    {
        [Key, ForeignKey("UserDetails")]
        public int UserID { get; set; }

        public bool WorkExperienceStatus { get; set; }

        public bool EducationalDetailsStatus { get; set; }

        public bool ProjectDetailsStatus { get; set; }

        public bool SkillsDetailsStatus { get; set; }

        public bool LanguagesStatus { get; set; }

        public virtual UserDetails UserDetails { get; set; }

        public Setting()
        {
            WorkExperienceStatus = true;
            EducationalDetailsStatus = true;
            ProjectDetailsStatus = true;
            SkillsDetailsStatus = true;
            LanguagesStatus = true;
        }
    }
}