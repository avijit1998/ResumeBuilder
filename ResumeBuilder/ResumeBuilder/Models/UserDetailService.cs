using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ResumeBuilder.Models
{
    public class UserDetailService
    {
        [Key, ForeignKey("LoginService")]
        public int UserServiceID { get; set; }

        public string Name { get; set; }
        
        public string Gender { get; set; }
        
        public DateTime? DateofBirth { get; set; }

        public string Phone { get; set; }

        public string UserRole { get; set; }

        public string Summary { get; set; }

        public virtual LoginService LoginService { get; set; }

        public virtual SettingService SettingService { get; set; }

        public virtual List<EducationalDetailsService> EducationalDetailsService { get; set; }

        public virtual List<ProjectService> ProjectService { get; set; }

        public virtual List<WorkExperienceService> WorkExperienceService { get; set; }

        public virtual List<Skill> SkillService { get; set; }
        
        public virtual List<Language> LanguageService { get; set; }

        public UserDetailService()
        {
            UserRole = "Candidate";
        }
    }
}