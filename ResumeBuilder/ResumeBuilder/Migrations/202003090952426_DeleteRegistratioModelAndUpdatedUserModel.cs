namespace ResumeBuilder.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteRegistratioModelAndUpdatedUserModel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.EducationalDetails", "StudentID", "dbo.StudentsRegistrations");
            DropForeignKey("dbo.Users", "StudentID", "dbo.StudentsRegistrations");
            DropIndex("dbo.EducationalDetails", new[] { "StudentID" });
            DropIndex("dbo.Users", new[] { "StudentID" });
            CreateTable(
                "dbo.WorkExperiences",
                c => new
                    {
                        WorkExperienceid = c.Int(nullable: false, identity: true),
                        UserID = c.Int(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        OrganizationName = c.String(),
                        Role = c.String(),
                    })
                .PrimaryKey(t => t.WorkExperienceid)
                .ForeignKey("dbo.Users", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID);
            
            AddColumn("dbo.EducationalDetails", "UserId", c => c.Int(nullable: false));
            AddColumn("dbo.Users", "Username", c => c.String(nullable: false));
            AddColumn("dbo.Users", "Password", c => c.String(nullable: false));
            AddColumn("dbo.Users", "ConfirmPassword", c => c.String(nullable: false));
            AddColumn("dbo.Users", "Summary", c => c.String());
            AlterColumn("dbo.Users", "PhoneNumber", c => c.Int());
            CreateIndex("dbo.EducationalDetails", "UserId");
            AddForeignKey("dbo.EducationalDetails", "UserId", "dbo.Users", "UserID", cascadeDelete: true);
            DropColumn("dbo.EducationalDetails", "StudentID");
            DropColumn("dbo.Users", "StudentID");
            DropTable("dbo.StudentsRegistrations");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.StudentsRegistrations",
                c => new
                    {
                        StudentRegistrationID = c.Int(nullable: false, identity: true),
                        Email = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.StudentRegistrationID);
            
            AddColumn("dbo.Users", "StudentID", c => c.Int(nullable: false));
            AddColumn("dbo.EducationalDetails", "StudentID", c => c.Int(nullable: false));
            DropForeignKey("dbo.WorkExperiences", "UserID", "dbo.Users");
            DropForeignKey("dbo.EducationalDetails", "UserId", "dbo.Users");
            DropIndex("dbo.WorkExperiences", new[] { "UserID" });
            DropIndex("dbo.EducationalDetails", new[] { "UserId" });
            AlterColumn("dbo.Users", "PhoneNumber", c => c.Int(nullable: false));
            DropColumn("dbo.Users", "Summary");
            DropColumn("dbo.Users", "ConfirmPassword");
            DropColumn("dbo.Users", "Password");
            DropColumn("dbo.Users", "Username");
            DropColumn("dbo.EducationalDetails", "UserId");
            DropTable("dbo.WorkExperiences");
            CreateIndex("dbo.Users", "StudentID");
            CreateIndex("dbo.EducationalDetails", "StudentID");
            AddForeignKey("dbo.Users", "StudentID", "dbo.StudentsRegistrations", "StudentRegistrationID", cascadeDelete: true);
            AddForeignKey("dbo.EducationalDetails", "StudentID", "dbo.StudentsRegistrations", "StudentRegistrationID", cascadeDelete: true);
        }
    }
}
