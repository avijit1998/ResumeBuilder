using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ResumeBuilder.Models
{
    public class ProjectService
    {
        [Key]
        public int ProjectServiceID { get; set; }

        public int UserServiceID { get; set; }
        
        public string ProjectTitle { get; set; }

        public int DurationInMonth { get; set; }

        public string ProjectRole { get; set; }

        public string Description { get; set; }

        [ForeignKey("UserServiceID")]
        public virtual UserDetailService UserDetailService { get; set; }
    }
}