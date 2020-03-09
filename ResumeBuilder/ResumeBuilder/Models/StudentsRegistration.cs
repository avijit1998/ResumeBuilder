using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ResumeBuilder.Models
{
    public class StudentsRegistration
    {
        [Key]
        public int StudentRegistrationID { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
    }
}
