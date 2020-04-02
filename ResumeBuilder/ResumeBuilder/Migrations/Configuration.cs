namespace ResumeBuilder.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ResumeBuilder.Models.ResumeBuilderDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ResumeBuilder.Models.ResumeBuilderDBContext context)
        {
            if (!context.Languages.Any())
            {
                context.Languages.Add(new Models.Language
                {
                    LanguageName = "English"
                });

                context.Languages.Add(new Models.Language
                {
                    LanguageName = "Hindi"
                });

                context.Languages.Add(new Models.Language
                {
                    LanguageName = "Oriya"
                });

                context.Languages.Add(new Models.Language
                {
                    LanguageName = "Gujrati"
                });

                context.Languages.Add(new Models.Language
                {
                    LanguageName = "Bengali"
                });

                context.Languages.Add(new Models.Language
                {
                    LanguageName = "Tamil"
                });

                context.Languages.Add(new Models.Language
                {
                    LanguageName = "Kannada"
                });

                context.Languages.Add(new Models.Language
                {
                    LanguageName = "Telgu"
                });
            }

            if (!context.Skills.Any())
            {
                context.Skills.Add(new Models.Skill
                {
                    SkillName = "ASP"
                });

                context.Skills.Add(new Models.Skill
                {
                    SkillName = "ASP .NET MVC"
                });

                context.Skills.Add(new Models.Skill
                {
                    SkillName = "C"
                });

                context.Skills.Add(new Models.Skill
                {
                    SkillName = "C++"

                });

                context.Skills.Add(new Models.Skill
                {
                    SkillName = "CSS/CSS3"
                });

                context.Skills.Add(new Models.Skill
                {
                    SkillName = "C#"
                });

                context.Skills.Add(new Models.Skill
                {
                    SkillName = "Design Patterns"
                });

                context.Skills.Add(new Models.Skill
                {
                    SkillName = "HTML/HTML5"
                });

                context.Skills.Add(new Models.Skill
                {
                    SkillName = "Hibernate"
                });

                context.Skills.Add(new Models.Skill
                {
                    SkillName = "JQuery"
                });

                context.Skills.Add(new Models.Skill
                {
                    SkillName = "JavaScript"
                });

                context.Skills.Add(new Models.Skill
                {
                    SkillName = "JAVA"
                });

                context.Skills.Add(new Models.Skill
                {
                    SkillName = "JDBC"
                });

                context.Skills.Add(new Models.Skill
                {
                    SkillName = "JSP"
                });

                context.Skills.Add(new Models.Skill
                {
                    SkillName = "Perl"
                });

                context.Skills.Add(new Models.Skill
                {
                    SkillName = "Python"
                });

                context.Skills.Add(new Models.Skill
                {
                    SkillName = "PHP"
                });

                context.Skills.Add(new Models.Skill
                {
                    SkillName = "Servlet"
                });

                context.Skills.Add(new Models.Skill
                {
                    SkillName = "Spring"
                });

                context.Skills.Add(new Models.Skill
                {
                    SkillName = "SQL SERVER"
                });

                context.Skills.Add(new Models.Skill
                {
                    SkillName = "Ruby"
                });

                context.Skills.Add(new Models.Skill
                {
                    SkillName = "R"
                });
                context.Skills.Add(new Models.Skill
                {
                    SkillName = "MongoDB"
                });

                context.Skills.Add(new Models.Skill
                {
                    SkillName = "MySQL"
                });

            }

            if (!context.Courses.Any())
            {
                context.Courses.Add(new Models.Course
                {
                    CourseName = "10th"
                });

                context.Courses.Add(new Models.Course
                {
                    CourseName = "12th"
                });

                context.Courses.Add(new Models.Course
                {
                    CourseName = "Under Graduation"
                });

                context.Courses.Add(new Models.Course
                {
                    CourseName = "Post Graduation"
                });

            }
        }
    }
}
