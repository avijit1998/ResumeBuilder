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

        public string Course { get; set; }

        public int PassingYear { get; set; }

        public int CGPAOrPercentage { get; set; }
    }
}
