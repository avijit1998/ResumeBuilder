using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ResumeBuilder.ViewModels
{
    public class AllInformation
    {
        public AllInformation()
        {
            ProjectInfo = new List<ProjectInfoVM>();
            WorkExperiences = new List<WorkExperienceInfoVM>();
            Languages = new List<LanguageInfoVM>();
            Skills = new List<SkillInfoVM>();
        }

        public UserInfoVM UserInfo { get; set; }

        public List<ProjectInfoVM> ProjectInfo { get; set; }

        public List<WorkExperienceInfoVM> WorkExperiences { get; set; }

        public List<LanguageInfoVM> Languages { get; set; }

        public List<SkillInfoVM> Skills { get; set; }
        
        
        //EducationDetails
        //public int PassingYear { get; set; }

        //public int CGPAOrPercentage { get; set; }

        //public string Stream { get; set; }

        //public string CourseName { get; set; }

    }

    public class UserInfoVM
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public virtual string ConfirmPassword { get; set; }

        public string Name { get; set; }

        public string Gender { get; set; }

        public string Summary { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime? DateOfBirth { get; set; }
    }


    public class LanguageInfoVM
    {
        public string Language{get; set;}

    }

    public class SkillInfoVM
    {
        public string Skill { get; set; }
    }

    public class ProjectInfoVM
    {
        public string Title { get; set; }

        public int Duration { get; set; }

        public string Role { get; set; }

        public string Description { get; set; }
    }

    public class WorkExperienceInfoVM
    {
        public int StartMonth { get; set; }

        public int StartYear { get; set; }

        public int EndMonth { get; set; }

        public int EndYear { get; set; }

        public string OrganizationName { get; set; }

        public string Role { get; set; }

        public bool CurrentlyWorking { get; set; }
    }
}