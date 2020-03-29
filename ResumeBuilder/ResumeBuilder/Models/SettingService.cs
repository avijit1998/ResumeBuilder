using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ResumeBuilder.Models
{
    public class SettingService
    {
        [Key, ForeignKey("UserDetailService")]
        public int UserServiceID { get; set; }

        public bool CheckWorkExperince { get; set; }

        public bool CheckEducationalDetails { get; set; }

        public bool CheckProjectDetails { get; set; }

        public bool CheckSkillsDetails { get; set; }

        public bool CheckLanguages { get; set; }

        public virtual UserDetailService UserDetailService { get; set; }

        public SettingService()
        {
            CheckEducationalDetails = true;
            CheckWorkExperince = true;
            CheckProjectDetails = true;
            CheckSkillsDetails = true;
            CheckLanguages = true;
        }
    }
}