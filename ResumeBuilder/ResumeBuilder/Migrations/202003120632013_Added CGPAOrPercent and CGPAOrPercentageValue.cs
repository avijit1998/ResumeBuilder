namespace ResumeBuilder.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedCGPAOrPercentandCGPAOrPercentageValue : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EducationalDetails", "CGPAOrPercentage", c => c.String());
            AddColumn("dbo.EducationalDetails", "CGPAOrPercentageValue", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.EducationalDetails", "CGPAOrPercentageValue");
            DropColumn("dbo.EducationalDetails", "CGPAOrPercentage");
        }
    }
}
