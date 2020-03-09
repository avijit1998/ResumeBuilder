using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace ResumeBuilder.Models
{
    public class User
    {
        [Key]
        public int UserID { get; set; }

        [ForeignKey("StudentID")]
        public StudentsRegistration StudentsRegistration { get; set; }

        public int StudentID { get; set; }

        [Display(Name = "Phone Number")]
        public int PhoneNumber { get; set; }

        [Display(Name = "Date of Birth")]
        public DateTime? DateOfBirth { get; set; }
    }
}
