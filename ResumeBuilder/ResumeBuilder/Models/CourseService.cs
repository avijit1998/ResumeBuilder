using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ResumeBuilder.Models
{
    public class CourseService
    {
        [Key]
        public int CourseServiceID { get; set; }

        public string CourseName { get; set; }

        
    }
}