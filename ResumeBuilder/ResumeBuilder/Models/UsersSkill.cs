using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace ResumeBuilder.Models
{
    public class UsersSkill
    {
        [Key]
        public int UserSkillID { get; set; }

        [ForeignKey("UserID")]
        public User User { get; set; }

        public int UserID { get; set; }

        [ForeignKey("SkillID")]
        public Skills Skill { get; set; }

        public int SkillID { get; set; }
    }
}
