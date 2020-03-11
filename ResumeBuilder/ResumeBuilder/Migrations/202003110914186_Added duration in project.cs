namespace ResumeBuilder.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addeddurationinproject : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Projects", "Duration", c => c.Int(nullable: false));
            AddColumn("dbo.Projects", "Role", c => c.String());
            DropColumn("dbo.Projects", "StartDate");
            DropColumn("dbo.Projects", "EndDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Projects", "EndDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Projects", "StartDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.Projects", "Role");
            DropColumn("dbo.Projects", "Duration");
        }
    }
}
