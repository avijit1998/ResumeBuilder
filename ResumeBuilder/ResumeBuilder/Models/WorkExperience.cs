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
        public int WorkExperienceid { get; set; }

        [ForeignKey("UserID")]
        public User User { get; set; }

        public int UserID { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string OrganizationName { get; set; }

        public string Role { get; set; }


    }
}