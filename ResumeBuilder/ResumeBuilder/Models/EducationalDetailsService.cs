using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ResumeBuilder.Models
{
    public class EducationalDetailsService
    {
        public int EducationalDetailsServiceID { get; set; }

        public int UserServiceID { get; set; }

        public int CourseServiceID { get; set; }
        
        public string BoardOrUniversity { get; set; }

        public int PassingYear { get; set; }

        public string Stream { get; set; }

        public bool CGPAOrPercentage { get; set; }

        public double TotalPercentageorCGPAValue { get; set; }

        [ForeignKey("UserServiceID")]
        public virtual UserDetailService UserDetailService { get; set; }

        [ForeignKey("CourseServiceID")]
        public virtual CourseService CourseService { get; set; }

    }
}