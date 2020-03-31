using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ResumeBuilder.ViewModels
{
    public class SkillsVM
    {
        [Required]
        public int UserID { get; set; }

        [Required]
        public string[] SkillNames { get; set; }
    }
}