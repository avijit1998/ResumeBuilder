namespace ResumeBuilder.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedcourseIdinEducationalDetailsandaddedcoursemodel : DbMigration
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
            
            AddColumn("dbo.EducationalDetails", "CourseId", c => c.Int(nullable: false));
            AlterColumn("dbo.Users", "Password", c => c.String(nullable: false, maxLength: 100));
            CreateIndex("dbo.EducationalDetails", "CourseId");
            AddForeignKey("dbo.EducationalDetails", "CourseId", "dbo.Courses", "CourseId", cascadeDelete: true);
            DropColumn("dbo.EducationalDetails", "Course");
        }
        
        public override void Down()
        {
            AddColumn("dbo.EducationalDetails", "Course", c => c.String());
            DropForeignKey("dbo.EducationalDetails", "CourseId", "dbo.Courses");
            DropIndex("dbo.EducationalDetails", new[] { "CourseId" });
            AlterColumn("dbo.Users", "Password", c => c.String(nullable: false));
            DropColumn("dbo.EducationalDetails", "CourseId");
            DropTable("dbo.Courses");
        }
    }
}
