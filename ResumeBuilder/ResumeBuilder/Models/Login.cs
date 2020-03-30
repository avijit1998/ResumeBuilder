using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ResumeBuilder.Models
{
    public class Login
    {
        [Key]
        public int UserID { get; set; }

        public string  Username { get; set; }

        public string Password { get; set; }

        public string Salt { get; set; }
        
        public virtual UserDetails UserDetails { get; set; }
    }
}