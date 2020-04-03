using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ResumeBuilder.Models;


namespace ResumeBuilder.ViewModels
{
    public class AllInformationVM
    {
        public int UserID { get; set; }

        public string Name { get; set; }

        public string Gender { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string Summary { get; set; }

        public List<int> LanguageIds { get; set; }

        public List<EducationalDetails> EducationalDetail { get; set; }

        public List<Project> Projects { get; set; }

        public Login Login { get; set; }

        public List<WorkExperience> WorkExperiences { get; set; }

        public List<Skill> Skills { get; set; }

        public List<Language> Languages { get; set; }

        public bool IsAdmin { get; set; }
      
    }

}