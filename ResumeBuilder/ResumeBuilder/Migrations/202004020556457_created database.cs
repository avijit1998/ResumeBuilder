namespace ResumeBuilder.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createddatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        CourseID = c.Int(nullable: false, identity: true),
                        CourseName = c.String(),
                    })
                .PrimaryKey(t => t.CourseID);
            
            CreateTable(
                "dbo.EducationalDetails",
                c => new
                    {
                        EducationalDetailsID = c.Int(nullable: false, identity: true),
                        UserID = c.Int(nullable: false),
                        CourseID = c.Int(nullable: false),
                        BoardOrUniversity = c.String(),
                        PassingYear = c.Int(nullable: false),
                        Stream = c.String(),
                        CGPAOrPercentage = c.String(),
                        TotalPercentageOrCGPAValue = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.EducationalDetailsID)
                .ForeignKey("dbo.Courses", t => t.CourseID, cascadeDelete: true)
                .ForeignKey("dbo.UserDetails", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID)
                .Index(t => t.CourseID);
            
            CreateTable(
                "dbo.UserDetails",
                c => new
                    {
                        UserID = c.Int(nullable: false),
                        Name = c.String(),
                        Gender = c.String(),
                        DateOfBirth = c.DateTime(nullable: false),
                        Phone = c.String(),
                        IsAdmin = c.Boolean(nullable: false),
                        Summary = c.String(),
                    })
                .PrimaryKey(t => t.UserID)
                .ForeignKey("dbo.Logins", t => t.UserID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.Languages",
                c => new
                    {
                        LanguageID = c.Int(nullable: false, identity: true),
                        LanguageName = c.String(),
                    })
                .PrimaryKey(t => t.LanguageID);
            
            CreateTable(
                "dbo.Logins",
                c => new
                    {
                        UserID = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        Password = c.String(),
                        Salt = c.String(),
                    })
                .PrimaryKey(t => t.UserID);
            
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        ProjectID = c.Int(nullable: false, identity: true),
                        UserID = c.Int(nullable: false),
                        ProjectTitle = c.String(),
                        DurationInMonth = c.Int(nullable: false),
                        ProjectRole = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ProjectID)
                .ForeignKey("dbo.UserDetails", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.Settings",
                c => new
                    {
                        UserID = c.Int(nullable: false),
                        WorkExperienceStatus = c.Boolean(nullable: false),
                        EducationalDetailsStatus = c.Boolean(nullable: false),
                        ProjectDetailsStatus = c.Boolean(nullable: false),
                        SkillsDetailsStatus = c.Boolean(nullable: false),
                        LanguagesStatus = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.UserID)
                .ForeignKey("dbo.UserDetails", t => t.UserID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.Skills",
                c => new
                    {
                        SkillID = c.Int(nullable: false, identity: true),
                        SkillName = c.String(),
                    })
                .PrimaryKey(t => t.SkillID);
            
            CreateTable(
                "dbo.WorkExperiences",
                c => new
                    {
                        WorkExperienceID = c.Int(nullable: false, identity: true),
                        UserID = c.Int(nullable: false),
                        StartMonth = c.Int(nullable: false),
                        StartYear = c.Int(nullable: false),
                        EndMonth = c.Int(nullable: false),
                        EndYear = c.Int(nullable: false),
                        OrganizationName = c.String(),
                        Designation = c.String(),
                        IsCurrentlyWorking = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.WorkExperienceID)
                .ForeignKey("dbo.UserDetails", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.LanguageUserDetails",
                c => new
                    {
                        Language_LanguageID = c.Int(nullable: false),
                        UserDetails_UserID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Language_LanguageID, t.UserDetails_UserID })
                .ForeignKey("dbo.Languages", t => t.Language_LanguageID, cascadeDelete: true)
                .ForeignKey("dbo.UserDetails", t => t.UserDetails_UserID, cascadeDelete: true)
                .Index(t => t.Language_LanguageID)
                .Index(t => t.UserDetails_UserID);
            
            CreateTable(
                "dbo.SkillUserDetails",
                c => new
                    {
                        Skill_SkillID = c.Int(nullable: false),
                        UserDetails_UserID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Skill_SkillID, t.UserDetails_UserID })
                .ForeignKey("dbo.Skills", t => t.Skill_SkillID, cascadeDelete: true)
                .ForeignKey("dbo.UserDetails", t => t.UserDetails_UserID, cascadeDelete: true)
                .Index(t => t.Skill_SkillID)
                .Index(t => t.UserDetails_UserID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WorkExperiences", "UserID", "dbo.UserDetails");
            DropForeignKey("dbo.SkillUserDetails", "UserDetails_UserID", "dbo.UserDetails");
            DropForeignKey("dbo.SkillUserDetails", "Skill_SkillID", "dbo.Skills");
            DropForeignKey("dbo.Settings", "UserID", "dbo.UserDetails");
            DropForeignKey("dbo.Projects", "UserID", "dbo.UserDetails");
            DropForeignKey("dbo.UserDetails", "UserID", "dbo.Logins");
            DropForeignKey("dbo.LanguageUserDetails", "UserDetails_UserID", "dbo.UserDetails");
            DropForeignKey("dbo.LanguageUserDetails", "Language_LanguageID", "dbo.Languages");
            DropForeignKey("dbo.EducationalDetails", "UserID", "dbo.UserDetails");
            DropForeignKey("dbo.EducationalDetails", "CourseID", "dbo.Courses");
            DropIndex("dbo.SkillUserDetails", new[] { "UserDetails_UserID" });
            DropIndex("dbo.SkillUserDetails", new[] { "Skill_SkillID" });
            DropIndex("dbo.LanguageUserDetails", new[] { "UserDetails_UserID" });
            DropIndex("dbo.LanguageUserDetails", new[] { "Language_LanguageID" });
            DropIndex("dbo.WorkExperiences", new[] { "UserID" });
            DropIndex("dbo.Settings", new[] { "UserID" });
            DropIndex("dbo.Projects", new[] { "UserID" });
            DropIndex("dbo.UserDetails", new[] { "UserID" });
            DropIndex("dbo.EducationalDetails", new[] { "CourseID" });
            DropIndex("dbo.EducationalDetails", new[] { "UserID" });
            DropTable("dbo.SkillUserDetails");
            DropTable("dbo.LanguageUserDetails");
            DropTable("dbo.WorkExperiences");
            DropTable("dbo.Skills");
            DropTable("dbo.Settings");
            DropTable("dbo.Projects");
            DropTable("dbo.Logins");
            DropTable("dbo.Languages");
            DropTable("dbo.UserDetails");
            DropTable("dbo.EducationalDetails");
            DropTable("dbo.Courses");
        }
    }
}
