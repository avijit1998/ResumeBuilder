using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ResumeBuilder.Models
{
    public class EducationalDetails
    {
        public int EducationalDetailsID { get; set; }

        public int UserID { get; set; }

        public int CourseID { get; set; }
        
        public string BoardOrUniversity { get; set; }

        public int PassingYear { get; set; }

        public string Stream { get; set; }

        public string CGPAOrPercentage { get; set; }

        public double TotalPercentageOrCGPAValue { get; set; }

        [ForeignKey("UserID")]
        public virtual UserDetails UserDetails { get; set; }

        [ForeignKey("CourseID")]
        public virtual Course Course { get; set; }

    }
}