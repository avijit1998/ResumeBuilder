namespace ResumeBuilder.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addedgradeorpercentvalue : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EducationalDetails", "GradeOrPercentValue", c => c.String());
            DropColumn("dbo.EducationalDetails", "CGPAOrPercentage");
        }
        
        public override void Down()
        {
            AddColumn("dbo.EducationalDetails", "CGPAOrPercentage", c => c.Int(nullable: false));
            DropColumn("dbo.EducationalDetails", "GradeOrPercentValue");
        }
    }
}
