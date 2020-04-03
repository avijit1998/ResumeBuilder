using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ResumeBuilder.ViewModels
{
    public class EducationInfoVM
    {
        public int EducationalDetailsID { get; set; }

        [Required]
        public int UserID { get; set; }

        [Required]
        public int CourseID { get; set; }

        [Required]
        public string BoardOrUniversity { get; set; }

        [Required]
        public int PassingYear { get; set; }

        [Required]
        public string Stream { get; set; }

        [Required]
        public string CGPAOrPercentage { get; set; }

        [Required]
        public double TotalPercentageOrCGPAValue { get; set; }        
    }
}