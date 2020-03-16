namespace ResumeBuilder.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedTotalPercentorCGPAValueineducaitonaldetailsmodel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EducationalDetails", "TotalPercentorCGPAValue", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.EducationalDetails", "TotalPercentorCGPAValue");
        }
    }
}
