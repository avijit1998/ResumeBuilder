namespace ResumeBuilder.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedTablesForUser : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EducationalDetails",
                c => new
                    {
                        EducationalDetailID = c.Int(nullable: false, identity: true),
                        StudentID = c.Int(nullable: false),
                        Course = c.String(),
                        PassingYear = c.Int(nullable: false),
                        CGPAOrPercentage = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.EducationalDetailID)
                .ForeignKey("dbo.StudentsRegistrations", t => t.StudentID, cascadeDelete: true)
                .Index(t => t.StudentID);
            
            CreateTable(
                "dbo.StudentsRegistrations",
                c => new
                    {
                        StudentRegistrationID = c.Int(nullable: false, identity: true),
                        Email = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.StudentRegistrationID);
            
            CreateTable(
                "dbo.Languages",
                c => new
                    {
                        LanguageID = c.Int(nullable: false, identity: true),
                        Language = c.String(),
                    })
                .PrimaryKey(t => t.LanguageID);
            
            CreateTable(
                "dbo.LoginViewModels",
                c => new
                    {
                        LoginViewModelId = c.Int(nullable: false, identity: true),
                        Email = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        RememberMe = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.LoginViewModelId);
            
            CreateTable(
                "dbo.RegisterViewModels",
                c => new
                    {
                        RegisterViewModelId = c.Int(nullable: false, identity: true),
                        Email = c.String(nullable: false),
                        Password = c.String(nullable: false, maxLength: 100),
                        ConfirmPassword = c.String(),
                    })
                .PrimaryKey(t => t.RegisterViewModelId);
            
            CreateTable(
                "dbo.Skills",
                c => new
                    {
                        SkillID = c.Int(nullable: false, identity: true),
                        Skill = c.String(),
                    })
                .PrimaryKey(t => t.SkillID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserID = c.Int(nullable: false, identity: true),
                        StudentID = c.Int(nullable: false),
                        PhoneNumber = c.Int(nullable: false),
                        DateOfBirth = c.DateTime(),
                    })
                .PrimaryKey(t => t.UserID)
                .ForeignKey("dbo.StudentsRegistrations", t => t.StudentID, cascadeDelete: true)
                .Index(t => t.StudentID);
            
            CreateTable(
                "dbo.UsersLanguages",
                c => new
                    {
                        UsersLanguageID = c.Int(nullable: false, identity: true),
                        UserID = c.Int(nullable: false),
                        LanguageID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UsersLanguageID)
                .ForeignKey("dbo.Languages", t => t.LanguageID, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID)
                .Index(t => t.LanguageID);
            
            CreateTable(
                "dbo.UsersSkills",
                c => new
                    {
                        UserSkillID = c.Int(nullable: false, identity: true),
                        UserID = c.Int(nullable: false),
                        SkillID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserSkillID)
                .ForeignKey("dbo.Skills", t => t.SkillID, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID)
                .Index(t => t.SkillID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UsersSkills", "UserID", "dbo.Users");
            DropForeignKey("dbo.UsersSkills", "SkillID", "dbo.Skills");
            DropForeignKey("dbo.UsersLanguages", "UserID", "dbo.Users");
            DropForeignKey("dbo.UsersLanguages", "LanguageID", "dbo.Languages");
            DropForeignKey("dbo.Users", "StudentID", "dbo.StudentsRegistrations");
            DropForeignKey("dbo.EducationalDetails", "StudentID", "dbo.StudentsRegistrations");
            DropIndex("dbo.UsersSkills", new[] { "SkillID" });
            DropIndex("dbo.UsersSkills", new[] { "UserID" });
            DropIndex("dbo.UsersLanguages", new[] { "LanguageID" });
            DropIndex("dbo.UsersLanguages", new[] { "UserID" });
            DropIndex("dbo.Users", new[] { "StudentID" });
            DropIndex("dbo.EducationalDetails", new[] { "StudentID" });
            DropTable("dbo.UsersSkills");
            DropTable("dbo.UsersLanguages");
            DropTable("dbo.Users");
            DropTable("dbo.Skills");
            DropTable("dbo.RegisterViewModels");
            DropTable("dbo.LoginViewModels");
            DropTable("dbo.Languages");
            DropTable("dbo.StudentsRegistrations");
            DropTable("dbo.EducationalDetails");
        }
    }
}
