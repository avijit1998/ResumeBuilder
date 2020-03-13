namespace ResumeBuilder.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deletedgradeorpercentvalue : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.EducationalDetails", "GradeOrPercentValue");
        }
        
        public override void Down()
        {
            AddColumn("dbo.EducationalDetails", "GradeOrPercentValue", c => c.String());
        }
    }
}
