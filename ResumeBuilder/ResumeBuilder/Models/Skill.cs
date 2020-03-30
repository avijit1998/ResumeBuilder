using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ResumeBuilder.Models
{
    public class Skill
    {
        [Key]
        public int SkillID { get; set; }

        public string SkillName { get; set; }

        public virtual List<UserDetails> Users { get; set; }
    }
}