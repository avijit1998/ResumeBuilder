using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace ResumeBuilder.Models
{
    public class EducationalDetails
    {
        [Key]
        public int EducationalDetailID { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        public int UserId { get; set; }

        [ForeignKey("CourseId")]
        public Course Course { get; set; }

        public int CourseId { get; set; }

        public string Board { get; set; }   

        public int PassingYear { get; set; }

        public string Stream { get; set; }

        public string CGPAOrPercentage { get; set; }

        public double TotalPercentorCGPAValue { get; set; }

     }
}   
