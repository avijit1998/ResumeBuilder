namespace ResumeBuilder.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newdatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        CourseId = c.Int(nullable: false, identity: true),
                        CourseName = c.String(),
                    })
                .PrimaryKey(t => t.CourseId);
            
            CreateTable(
                "dbo.EducationalDetails",
                c => new
                    {
                        EducationalDetailID = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        CourseId = c.Int(nullable: false),
                        Board = c.String(),
                        PassingYear = c.Int(nullable: false),
                        Stream = c.String(),
                        CGPAOrPercentage = c.String(),
                        TotalPercentorCGPAValue = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.EducationalDetailID)
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.CourseId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserID = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false),
                        Password = c.String(nullable: false, maxLength: 100),
                        ConfirmPassword = c.String(nullable: false),
                        Name = c.String(),
                        Gender = c.String(),
                        Summary = c.String(),
                        PhoneNumber = c.String(),
                        DateOfBirth = c.DateTime(),
                        UserRole = c.String(),
                    })
                .PrimaryKey(t => t.UserID);
            
            CreateTable(
                "dbo.Languages",
                c => new
                    {
                        LanguageID = c.Int(nullable: false, identity: true),
                        Language = c.String(),
                    })
                .PrimaryKey(t => t.LanguageID);
            
            CreateTable(
                "dbo.Skills",
                c => new
                    {
                        SkillID = c.Int(nullable: false, identity: true),
                        SkillName = c.String(),
                    })
                .PrimaryKey(t => t.SkillID);
            
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        ProjectId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        Title = c.String(),
                        Duration = c.Int(nullable: false),
                        ProjectRole = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ProjectId)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UserSettings",
                c => new
                    {
                        UserSettingId = c.Int(nullable: false, identity: true),
                        UserID = c.Int(nullable: false),
                        setWorkex = c.Int(nullable: false),
                        setEducation = c.Int(nullable: false),
                        setSkills = c.Int(nullable: false),
                        setProject = c.Int(nullable: false),
                        setContact = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserSettingId)
                .ForeignKey("dbo.Users", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.WorkExperiences",
                c => new
                    {
                        WorkExperienceid = c.Int(nullable: false, identity: true),
                        UserID = c.Int(nullable: false),
                        StartMonth = c.Int(nullable: false),
                        StartYear = c.Int(nullable: false),
                        EndMonth = c.Int(nullable: false),
                        EndYear = c.Int(nullable: false),
                        OrganizationName = c.String(),
                        Role = c.String(),
                        CurrentlyWorking = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.WorkExperienceid)
                .ForeignKey("dbo.Users", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.LanguagesUsers",
                c => new
                    {
                        Languages_LanguageID = c.Int(nullable: false),
                        User_UserID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Languages_LanguageID, t.User_UserID })
                .ForeignKey("dbo.Languages", t => t.Languages_LanguageID, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.User_UserID, cascadeDelete: true)
                .Index(t => t.Languages_LanguageID)
                .Index(t => t.User_UserID);
            
            CreateTable(
                "dbo.SkillsUsers",
                c => new
                    {
                        Skills_SkillID = c.Int(nullable: false),
                        User_UserID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Skills_SkillID, t.User_UserID })
                .ForeignKey("dbo.Skills", t => t.Skills_SkillID, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.User_UserID, cascadeDelete: true)
                .Index(t => t.Skills_SkillID)
                .Index(t => t.User_UserID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WorkExperiences", "UserID", "dbo.Users");
            DropForeignKey("dbo.UserSettings", "UserID", "dbo.Users");
            DropForeignKey("dbo.Projects", "UserId", "dbo.Users");
            DropForeignKey("dbo.EducationalDetails", "UserId", "dbo.Users");
            DropForeignKey("dbo.SkillsUsers", "User_UserID", "dbo.Users");
            DropForeignKey("dbo.SkillsUsers", "Skills_SkillID", "dbo.Skills");
            DropForeignKey("dbo.LanguagesUsers", "User_UserID", "dbo.Users");
            DropForeignKey("dbo.LanguagesUsers", "Languages_LanguageID", "dbo.Languages");
            DropForeignKey("dbo.EducationalDetails", "CourseId", "dbo.Courses");
            DropIndex("dbo.SkillsUsers", new[] { "User_UserID" });
            DropIndex("dbo.SkillsUsers", new[] { "Skills_SkillID" });
            DropIndex("dbo.LanguagesUsers", new[] { "User_UserID" });
            DropIndex("dbo.LanguagesUsers", new[] { "Languages_LanguageID" });
            DropIndex("dbo.WorkExperiences", new[] { "UserID" });
            DropIndex("dbo.UserSettings", new[] { "UserID" });
            DropIndex("dbo.Projects", new[] { "UserId" });
            DropIndex("dbo.EducationalDetails", new[] { "CourseId" });
            DropIndex("dbo.EducationalDetails", new[] { "UserId" });
            DropTable("dbo.SkillsUsers");
            DropTable("dbo.LanguagesUsers");
            DropTable("dbo.WorkExperiences");
            DropTable("dbo.UserSettings");
            DropTable("dbo.Projects");
            DropTable("dbo.Skills");
            DropTable("dbo.Languages");
            DropTable("dbo.Users");
            DropTable("dbo.EducationalDetails");
            DropTable("dbo.Courses");
        }
    }
}
