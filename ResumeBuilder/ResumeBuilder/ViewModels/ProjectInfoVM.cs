using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ResumeBuilder.ViewModels
{
    public class ProjectInfoVM
    {
        public int ProjectID { get; set; }

        public int UserID { get; set; }

        public string ProjectTitle { get; set; }

        public int DurationInMonth { get; set; }

        public string ProjectRole { get; set; }

        public string Description { get; set; }

    }
}