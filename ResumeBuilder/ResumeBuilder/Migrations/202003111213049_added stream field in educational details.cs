namespace ResumeBuilder.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedstreamfieldineducationaldetails : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EducationalDetails", "Stream", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.EducationalDetails", "Stream");
        }
    }
}
