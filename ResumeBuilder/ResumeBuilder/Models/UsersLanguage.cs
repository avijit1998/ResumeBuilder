using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace ResumeBuilder.Models
{
    public class UsersLanguage
    {
        [Key]
        public int UsersLanguageID { get; set; }

        [ForeignKey("UserID")]
        public User User { get; set; }

        public int UserID { get; set; }

        [ForeignKey("LanguageID")]
        public Languages Language { get; set; }

        public int LanguageID { get; set; }
    }
}
