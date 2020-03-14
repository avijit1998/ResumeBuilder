using ResumeBuilder.Models;
using ResumeBuilder.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ResumeBuilder.Helpers
{
    public class CustomMapper
    {
        public static List<ProjectInfoVM> Map(IEnumerable<Project> projects)
        {
            var res = new List<ProjectInfoVM>();

            if (projects != null && projects.Any())
            {
                res.AddRange(from p in projects
                             select Map(p));
            }

            return res;
        }

        public static ProjectInfoVM Map(Project project)
        {
            if (project == null)
                return null;
            else
                return new ProjectInfoVM
                {
                    Title = project.Title,
                    Description = project.Description,
                   // ProjectRole = project.ProjectRole,
                    Duration=project.Duration
                };
        }

       
        //Mapper for Work Experience
        public static List<WorkExperienceInfoVM> Map(IEnumerable<WorkExperience> workExp)
        {
            var res = new List<WorkExperienceInfoVM>();

            if (workExp != null && workExp.Any())
            {
                res.AddRange(from p in workExp
                             select Map(p));
            }

            return res;
        }


        public static WorkExperienceInfoVM Map(WorkExperience workExp)
        {
            if (workExp == null)
                return null;
            else
                return new WorkExperienceInfoVM
                {
                    OrganizationName = workExp.OrganizationName,
                    Role=workExp.Role,
                    StartMonth=workExp.StartMonth,
                    StartYear=workExp.StartYear,
                    EndMonth=workExp.EndMonth,
                    EndYear=workExp.EndYear,
                    CurrentlyWorking=workExp.CurrentlyWorking
                };
        }

        //Mapper for Language
        public static List<LanguageInfoVM> Map(IEnumerable<Languages> language)
        {
            var res = new List<LanguageInfoVM>();

            if (language != null && language.Any())
            {
                res.AddRange(from p in language
                             select Map(p));
            }

            return res;
        }

        public static LanguageInfoVM Map(Languages language)
        {
            if (language == null)
                return null;
            else
                return new LanguageInfoVM
                {
                   Language=language.Language
                };
        }
    
        //Mapper for Skills
        public static List<SkillInfoVM> Map(IEnumerable<Skills> skills)
        {
            var res = new List<SkillInfoVM>();

            if (skills != null && skills.Any())
            {
                res.AddRange(from p in skills
                             select Map(p));
            }

            return res;
        }

        public static SkillInfoVM Map(Skills skills)
        {
            if (skills == null)
                return null;
            else
                return new SkillInfoVM
                {
                    Skill = skills.SkillName
                };
        }
    }
}