using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        [Required]
        public string Username { get; set; }

        [Required]
        [RegularExpression("((?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%!+-_=]).{8,})")] 
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 8)]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        public virtual string ConfirmPassword { get; set; }

        public string Name { get; set; }

        public string Gender { get; set; }

        public string Summary { get; set; }

        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Date of Birth")]
        public DateTime? DateOfBirth { get; set; }

        public string UserRole { get; set; }

        public virtual IList<Project> Projects { get; set; }

        public virtual IList<WorkExperience> WorkExperiences { get; set; }

        public virtual IList<EducationalDetails> EducationalDetails { get; set; }

        public virtual IList<UsersLanguage> UsersLanguages { get; set; }

        public virtual IList<UsersSkill> UsersSkills { get; set; }
      
        public User()
        {
            UserRole = "Candidate";
        }
    }
}
