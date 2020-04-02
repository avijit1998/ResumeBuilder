using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ResumeBuilder.ViewModels
{
    public class ProjectInfoVM
    {
        public int ProjectID { get; set; }

        [Required]
        public int UserID { get; set; }

        [Required]
        public string ProjectTitle { get; set; }

        [Required]
        public int DurationInMonth { get; set; }

        [Required]
        public string ProjectRole { get; set; }

        [Required]
        public string Description { get; set; }

    }
}