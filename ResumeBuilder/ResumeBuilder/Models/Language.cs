using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ResumeBuilder.Models
{
    public class Language
    {
        public int LanguageID { get; set; }

        public string LanguageName { get; set; }

        public virtual List<UserDetails> Users { get; set; }
    }
}