using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ResumeBuilder.Models
{
    public class WorkExperience
    {
        [Key]
        public int WorkExperienceID { get; set; }

        public int UserID { get; set; }
        
        public int StartMonth { get; set; }

        public int StartYear { get; set; }

        public int EndMonth { get; set; }

        public int EndYear { get; set; }

        public string OrganizationName { get; set; }

        public string Designation { get; set; }

        public bool IsCurrentlyWorking { get; set; }

        [ForeignKey("UserID")]
        public virtual UserDetails UserDetails { get; set; }

    }
}