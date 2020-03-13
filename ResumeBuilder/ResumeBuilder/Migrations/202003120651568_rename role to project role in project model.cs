namespace ResumeBuilder.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class renameroletoprojectroleinprojectmodel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Projects", "ProjectRole", c => c.String());
            DropColumn("dbo.Projects", "Role");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Projects", "Role", c => c.String());
            DropColumn("dbo.Projects", "ProjectRole");
        }
    }
}
