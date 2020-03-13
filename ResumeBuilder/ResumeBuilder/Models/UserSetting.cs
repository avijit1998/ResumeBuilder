using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResumeBuilder.Models
{
    public class UserSetting
    {
        [Key]
        public int UserSettingId { get; set; }

        [ForeignKey("UserID")]
        public User User { get; set; }

        public int UserID { get; set; }

        public int setWorkex { get; set; }
        
        public int setEducation { get; set; }
        
        public int setSkills { get; set; }
        
        public int setProject { get; set; }
        
        public int setContact { get; set; }
        
    }
}