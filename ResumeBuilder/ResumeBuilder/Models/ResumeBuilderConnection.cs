using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ResumeBuilder.Models
{
    public class ResumeBuilderConnection: DbContext
    {
        public DbSet<Skills> Skills { get; set; }
        public DbSet<Languages> Languages { get; set; }
        public DbSet<User> Users { get; set; }
        
        public DbSet<EducationalDetails> EducationalDetails { get; set; }
        public DbSet<WorkExperience> WorkExperiences { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Project> Projects { get; set; }
    }
}