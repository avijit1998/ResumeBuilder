using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ResumeBuilder.Models
{
    public class ResumeBuilderDBContext : DbContext
    {
        public DbSet<Course> Courses { get; set; }

        public DbSet<EducationalDetails> EducationalDetails { get; set; }

        public DbSet<Language> Languages { get; set; }

        public DbSet<Login> Logins { get; set; }

        public DbSet<Project> Projects { get; set; }

        public DbSet<Setting> Settings { get; set; }

        public DbSet<Skill> Skills { get; set; }

        public DbSet<UserDetails> UserDetails { get; set; }

        public DbSet<WorkExperience> WorkExperiences { get; set; }
    }
}