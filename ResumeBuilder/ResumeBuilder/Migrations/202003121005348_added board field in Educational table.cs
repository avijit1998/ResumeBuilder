namespace ResumeBuilder.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedboardfieldinEducationaltable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EducationalDetails", "Board", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.EducationalDetails", "Board");
        }
    }
}
