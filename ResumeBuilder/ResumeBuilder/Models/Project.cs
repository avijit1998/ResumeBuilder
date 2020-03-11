using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ResumeBuilder.Models
{
    public class Project
    {
        public int ProjectId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        public int UserId { get; set; }

        public string Title { get; set; }

        public int Duration { get; set; }

        public string Role { get; set; }

        public string Description { get; set; }

    }
}