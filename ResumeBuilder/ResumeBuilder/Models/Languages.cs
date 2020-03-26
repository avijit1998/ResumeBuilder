using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ResumeBuilder.Models
{
    public class Languages
    {
        [Key]
        public int LanguageID { get; set; }

        public string Language { get; set; }

        public virtual List<User> Users { get; set; }
    }
}
