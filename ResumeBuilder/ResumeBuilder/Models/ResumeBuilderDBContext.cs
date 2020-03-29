using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ResumeBuilder.Models
{
    public class ResumeBuilderDBContext : DbContext
    {
        public DbSet<CourseService> CourseService{ get; set; }

        public DbSet<EducationalDetailsService> EducationalDetailsService { get; set; }

        public DbSet<Language> Languages { get; set; }

        public DbSet<LoginService> LoginService { get; set; }

        public DbSet<ProjectService> ProjectService { get; set; }

        public DbSet<SettingService> SettingService { get; set; }

        public DbSet<Skill> Skills { get; set; }

        public DbSet<UserDetailService> UserDetailService { get; set; }

        public DbSet<WorkExperienceService> WorkExperienceService { get; set; }
    }
}