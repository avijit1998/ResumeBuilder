using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ResumeBuilder.ViewModels
{
    public class WorkExperienceInfoVM
    {
        
        public int WorkExperienceID { get; set; }

        [Required]
        public int UserID { get; set; }

        [Required]
        public int StartMonth { get; set; }

        [Required]
        public int StartYear { get; set; }

        [Required]
        public int EndMonth { get; set; }

        [Required]
        public int EndYear { get; set; }

        [Required]
        public string OrganizationName { get; set; }

        [Required]
        public string Designation { get; set; }

        [Required]
        public bool IsCurrentlyWorking { get; set; }

    }
}