namespace ResumeBuilder.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deletedcgpaorpercentValuefromeducaitonaldetailsmodel : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.EducationalDetails", "CGPAOrPercentageValue");
        }
        
        public override void Down()
        {
            AddColumn("dbo.EducationalDetails", "CGPAOrPercentageValue", c => c.Int(nullable: false));
        }
    }
}
