namespace ResumeBuilder.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addedstartendmonthyearinworkexperiencemodel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.WorkExperiences", "StartMonth", c => c.Int(nullable: false));
            AddColumn("dbo.WorkExperiences", "StartYear", c => c.Int(nullable: false));
            AddColumn("dbo.WorkExperiences", "EndMonth", c => c.Int(nullable: false));
            AddColumn("dbo.WorkExperiences", "EndYear", c => c.Int(nullable: false));
            DropColumn("dbo.WorkExperiences", "StartDate");
            DropColumn("dbo.WorkExperiences", "EndDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.WorkExperiences", "EndDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.WorkExperiences", "StartDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.WorkExperiences", "EndYear");
            DropColumn("dbo.WorkExperiences", "EndMonth");
            DropColumn("dbo.WorkExperiences", "StartYear");
            DropColumn("dbo.WorkExperiences", "StartMonth");
        }
    }
}
