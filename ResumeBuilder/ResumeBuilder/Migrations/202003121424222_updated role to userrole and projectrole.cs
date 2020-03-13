namespace ResumeBuilder.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedroletouserroleandprojectrole : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "UserRole", c => c.String());
            AddColumn("dbo.Projects", "ProjectRole", c => c.String());
            DropColumn("dbo.Users", "Role");
            DropColumn("dbo.Projects", "Role");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Projects", "Role", c => c.String());
            AddColumn("dbo.Users", "Role", c => c.String());
            DropColumn("dbo.Projects", "ProjectRole");
            DropColumn("dbo.Users", "UserRole");
        }
    }
}
