using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ResumeBuilder.Models
{
    public class Project
    {
        [Key]
        public int ProjectID { get; set; }

        public int UserID { get; set; }
        
        public string ProjectTitle { get; set; }

        public int DurationInMonth { get; set; }

        public string ProjectRole { get; set; }

        public string Description { get; set; }

        [ForeignKey("UserID")]
        public virtual UserDetails UserDetails { get; set; }
    }
}