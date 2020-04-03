using System;

namespace ResumeBuilder.ViewModels
{
    public class WorkExperienceVM
    {
        public string OrganizationName { get; set; }

        public string Role { get; set; }

        public string StartMonth { get; set; }

        public int StartYear { get; set; }

        public string EndMonth { get; set; }

        public int EndYear { get; set; }

        public bool CurrentlyWorking { get; set; }

    }
}