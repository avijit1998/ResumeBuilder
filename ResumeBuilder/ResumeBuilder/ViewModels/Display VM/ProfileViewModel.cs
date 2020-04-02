using System.Collections.Generic;

namespace ResumeBuilder.ViewModels
{
    public class ProfileViewModel
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
        public bool LanguageStatus { get; set; }
        public List<string> Languages { get; set; }

        // Educational Details
        public bool EducationStatus { get; set; }
        public List<EducationUIModel> EducationList { get; set; }

        // Skills
        public bool SkillStatus { get; set; }
        public List<string> SkillList { get; set; }

        // Projects
        public bool ProjectStatus { get; set; }
        public List<ProjectUIModel> ProjectList { get; set; }

        // Work Experience
        public bool WorkExperienceStatus { get; set; }
        public List<WorkExUIModel> WorkExList { get; set; }

        // Error Message
        public string ErrorMsg { get; set; }
    }
}