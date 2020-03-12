using System.Collections.Generic;

namespace ResumeBuilder.ViewModels
{
    public class PublicProfileViewModel
    {
        // Basic Details
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string LinkedinLink { get; set; }
        public string UserRole { get; set; }
        public string Summary { get; set; }
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
        public bool WorkExStatus { get; set; }
        public List<WorkExUIModel> WorkExList { get; set; }

        // Contact Details

    }
}