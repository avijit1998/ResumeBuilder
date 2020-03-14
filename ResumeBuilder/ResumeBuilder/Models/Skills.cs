using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ResumeBuilder.Models
{
    public class Skills
    {
        [Key]
        public int SkillID { get; set; }

        public string SkillName { get; set; }

        public virtual List<User> Users { get; set; }
    }
}
