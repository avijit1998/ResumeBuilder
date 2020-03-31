using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ResumeBuilder.Models
{
    public class UserDetails
    {
        [Key, ForeignKey("Login")]
        public int UserID { get; set; }

        public string Name { get; set; }
        
        public string Gender { get; set; }
        
        public DateTime DateOfBirth { get; set; }

        public string Phone { get; set; }

        public bool IsAdmin { get; set; }

        public string Summary { get; set; }

        public virtual Login Login { get; set; }

        public virtual Setting Setting { get; set; }

        public virtual List<EducationalDetails> EducationalDetails { get; set; }

        public virtual List<Project> Projects { get; set; }

        public virtual List<WorkExperience> WorkExperiences { get; set; }

        public virtual List<Skill> Skills { get; set; }
        
        public virtual List<Language> Languages { get; set; }

        public UserDetails()
        {
            Languages = new List<Language>();
            Skills = new List<Skill>();
        }

    }
}