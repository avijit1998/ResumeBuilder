using ResumeBuilder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ResumeBuilder.ViewModels
{
    public class AllDetailsVM
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string LinkedinLink { get; set; }
        public string UserRole { get; set; }
        public string Summary { get; set; }

        public string CourseName { get; set; }

        public int PassingYear { get; set; }

        public int CGPAOrPercentage { get; set; }


        public string Title { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Description { get; set; }
    }

}