using System.Collections.Generic;

namespace ResumeBuilder.ViewModels
{
    public class ProfileVM
    {
        // Basic Details
        public string Name { get; set; }
        public string Gender { get; set; }
        public string DOB { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string UserRole { get; set; }
        public string Summary { get; set; }
        // Language
        public int LanguageStatus { get; set; }
        public List<string> Languages { get; set; }

        // Educational Details
        public int EducationStatus { get; set; }
        public List<EducationVM> EducationList { get; set; }

        // Skills
        public int SkillStatus { get; set; }
        public List<string> SkillList { get; set; }

        // Projects
        public int ProjectStatus { get; set; }
        public List<ProjectVM> ProjectList { get; set; }

        // Work Experience
        public int WorkExperienceStatus { get; set; }
        public List<WorkExperienceVM> WorkExList { get; set; }

        // Error Message
        public string ErrorMsg { get; set; }
    }
}