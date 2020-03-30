using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ResumeBuilder.Models.ViewModels
{
    public class EditUserLanguageViewModel
    {
        public int UserID { get; set; }

        public string Username { get; set; }

        public string Name { get; set; }

        public string Gender { get; set; }

        public string Summary { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public int[] LanguageIds { get; set; }
    }
}