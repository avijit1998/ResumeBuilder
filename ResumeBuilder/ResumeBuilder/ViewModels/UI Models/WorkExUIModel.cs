using System;

namespace ResumeBuilder.ViewModels
{
    public class WorkExUIModel
    {
        public string OrganizationName { get; set; }

        public string Role { get; set; }

        public int StartMonth { get; set; }

        public int StartYear { get; set; }

        public int EndMonth { get; set; }

        public int EndYear { get; set; }

        public bool CurrentlyWorking { get; set; }

    }
}