namespace ResumeBuilder.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addeduserrole : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "UserRole", c => c.String());
            DropColumn("dbo.Users", "Role");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "Role", c => c.String());
            DropColumn("dbo.Users", "UserRole");
        }
    }
}
