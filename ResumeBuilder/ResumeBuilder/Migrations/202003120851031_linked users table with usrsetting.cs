namespace ResumeBuilder.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class linkeduserstablewithusrsetting : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserSettings", "setUserId", "dbo.Users");
            RenameColumn(table: "dbo.UserSettings", name: "setUserId", newName: "UserID");
            RenameIndex(table: "dbo.UserSettings", name: "IX_setUserId", newName: "IX_UserID");
            DropPrimaryKey("dbo.UserSettings");
            AddColumn("dbo.UserSettings", "UserSettingId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.UserSettings", "UserSettingId");
            AddForeignKey("dbo.UserSettings", "UserID", "dbo.Users", "UserID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserSettings", "UserID", "dbo.Users");
            DropPrimaryKey("dbo.UserSettings");
            DropColumn("dbo.UserSettings", "UserSettingId");
            AddPrimaryKey("dbo.UserSettings", "setUserId");
            RenameIndex(table: "dbo.UserSettings", name: "IX_UserID", newName: "IX_setUserId");
            RenameColumn(table: "dbo.UserSettings", name: "UserID", newName: "setUserId");
            AddForeignKey("dbo.UserSettings", "setUserId", "dbo.Users", "UserID");
        }
    }
}
