namespace ResumeBuilder.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class creatednewfreshdatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CourseServices",
                c => new
                    {
                        CourseServiceID = c.Int(nullable: false, identity: true),
                        CourseName = c.String(),
                    })
                .PrimaryKey(t => t.CourseServiceID);
            
            CreateTable(
                "dbo.EducationalDetailsServices",
                c => new
                    {
                        EducationalDetailsServiceID = c.Int(nullable: false, identity: true),
                        UserServiceID = c.Int(nullable: false),
                        CourseServiceID = c.Int(nullable: false),
                        BoardOrUniversity = c.String(),
                        PassingYear = c.Int(nullable: false),
                        Stream = c.String(),
                        CGPAOrPercentage = c.Boolean(nullable: false),
                        TotalPercentageorCGPAValue = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.EducationalDetailsServiceID)
                .ForeignKey("dbo.CourseServices", t => t.CourseServiceID, cascadeDelete: true)
                .ForeignKey("dbo.UserDetailServices", t => t.UserServiceID, cascadeDelete: true)
                .Index(t => t.UserServiceID)
                .Index(t => t.CourseServiceID);
            
            CreateTable(
                "dbo.UserDetailServices",
                c => new
                    {
                        UserServiceID = c.Int(nullable: false),
                        Name = c.String(),
                        Gender = c.String(),
                        DateofBirth = c.DateTime(),
                        Phone = c.String(),
                        UserRole = c.String(),
                        Summary = c.String(),
                    })
                .PrimaryKey(t => t.UserServiceID)
                .ForeignKey("dbo.LoginServices", t => t.UserServiceID)
                .Index(t => t.UserServiceID);
            
            CreateTable(
                "dbo.Languages",
                c => new
                    {
                        LanguageID = c.Int(nullable: false, identity: true),
                        LanguageName = c.String(),
                    })
                .PrimaryKey(t => t.LanguageID);
            
            CreateTable(
                "dbo.LoginServices",
                c => new
                    {
                        UserServiceID = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        Password = c.String(),
                        Salt = c.String(),
                    })
                .PrimaryKey(t => t.UserServiceID);
            
            CreateTable(
                "dbo.ProjectServices",
                c => new
                    {
                        ProjectServiceID = c.Int(nullable: false, identity: true),
                        UserServiceID = c.Int(nullable: false),
                        ProjectTitle = c.String(),
                        DurationInMonth = c.Int(nullable: false),
                        ProjectRole = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ProjectServiceID)
                .ForeignKey("dbo.UserDetailServices", t => t.UserServiceID, cascadeDelete: true)
                .Index(t => t.UserServiceID);
            
            CreateTable(
                "dbo.SettingServices",
                c => new
                    {
                        UserServiceID = c.Int(nullable: false),
                        CheckWorkExperince = c.Boolean(nullable: false),
                        CheckEducationalDetails = c.Boolean(nullable: false),
                        CheckProjectDetails = c.Boolean(nullable: false),
                        CheckSkillsDetails = c.Boolean(nullable: false),
                        CheckLanguages = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.UserServiceID)
                .ForeignKey("dbo.UserDetailServices", t => t.UserServiceID)
                .Index(t => t.UserServiceID);
            
            CreateTable(
                "dbo.Skills",
                c => new
                    {
                        SkillID = c.Int(nullable: false, identity: true),
                        SkillName = c.String(),
                    })
                .PrimaryKey(t => t.SkillID);
            
            CreateTable(
                "dbo.WorkExperienceServices",
                c => new
                    {
                        WorkExperienceServiceID = c.Int(nullable: false, identity: true),
                        UserServiceID = c.Int(nullable: false),
                        StartMonth = c.Int(nullable: false),
                        StartYear = c.Int(nullable: false),
                        EndMonth = c.Int(nullable: false),
                        EndYear = c.Int(nullable: false),
                        OrganizationName = c.String(),
                        Designation = c.String(),
                        IsCurrentlyWorking = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.WorkExperienceServiceID)
                .ForeignKey("dbo.UserDetailServices", t => t.UserServiceID, cascadeDelete: true)
                .Index(t => t.UserServiceID);
            
            CreateTable(
                "dbo.LanguageUserDetailServices",
                c => new
                    {
                        Language_LanguageID = c.Int(nullable: false),
                        UserDetailService_UserServiceID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Language_LanguageID, t.UserDetailService_UserServiceID })
                .ForeignKey("dbo.Languages", t => t.Language_LanguageID, cascadeDelete: true)
                .ForeignKey("dbo.UserDetailServices", t => t.UserDetailService_UserServiceID, cascadeDelete: true)
                .Index(t => t.Language_LanguageID)
                .Index(t => t.UserDetailService_UserServiceID);
            
            CreateTable(
                "dbo.SkillUserDetailServices",
                c => new
                    {
                        Skill_SkillID = c.Int(nullable: false),
                        UserDetailService_UserServiceID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Skill_SkillID, t.UserDetailService_UserServiceID })
                .ForeignKey("dbo.Skills", t => t.Skill_SkillID, cascadeDelete: true)
                .ForeignKey("dbo.UserDetailServices", t => t.UserDetailService_UserServiceID, cascadeDelete: true)
                .Index(t => t.Skill_SkillID)
                .Index(t => t.UserDetailService_UserServiceID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WorkExperienceServices", "UserServiceID", "dbo.UserDetailServices");
            DropForeignKey("dbo.SkillUserDetailServices", "UserDetailService_UserServiceID", "dbo.UserDetailServices");
            DropForeignKey("dbo.SkillUserDetailServices", "Skill_SkillID", "dbo.Skills");
            DropForeignKey("dbo.SettingServices", "UserServiceID", "dbo.UserDetailServices");
            DropForeignKey("dbo.ProjectServices", "UserServiceID", "dbo.UserDetailServices");
            DropForeignKey("dbo.UserDetailServices", "UserServiceID", "dbo.LoginServices");
            DropForeignKey("dbo.LanguageUserDetailServices", "UserDetailService_UserServiceID", "dbo.UserDetailServices");
            DropForeignKey("dbo.LanguageUserDetailServices", "Language_LanguageID", "dbo.Languages");
            DropForeignKey("dbo.EducationalDetailsServices", "UserServiceID", "dbo.UserDetailServices");
            DropForeignKey("dbo.EducationalDetailsServices", "CourseServiceID", "dbo.CourseServices");
            DropIndex("dbo.SkillUserDetailServices", new[] { "UserDetailService_UserServiceID" });
            DropIndex("dbo.SkillUserDetailServices", new[] { "Skill_SkillID" });
            DropIndex("dbo.LanguageUserDetailServices", new[] { "UserDetailService_UserServiceID" });
            DropIndex("dbo.LanguageUserDetailServices", new[] { "Language_LanguageID" });
            DropIndex("dbo.WorkExperienceServices", new[] { "UserServiceID" });
            DropIndex("dbo.SettingServices", new[] { "UserServiceID" });
            DropIndex("dbo.ProjectServices", new[] { "UserServiceID" });
            DropIndex("dbo.UserDetailServices", new[] { "UserServiceID" });
            DropIndex("dbo.EducationalDetailsServices", new[] { "CourseServiceID" });
            DropIndex("dbo.EducationalDetailsServices", new[] { "UserServiceID" });
            DropTable("dbo.SkillUserDetailServices");
            DropTable("dbo.LanguageUserDetailServices");
            DropTable("dbo.WorkExperienceServices");
            DropTable("dbo.Skills");
            DropTable("dbo.SettingServices");
            DropTable("dbo.ProjectServices");
            DropTable("dbo.LoginServices");
            DropTable("dbo.Languages");
            DropTable("dbo.UserDetailServices");
            DropTable("dbo.EducationalDetailsServices");
            DropTable("dbo.CourseServices");
        }
    }
}
