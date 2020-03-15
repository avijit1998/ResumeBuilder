namespace ResumeBuilder.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addole : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserSettings",
                c => new
                    {
                        UserSettingId = c.Int(nullable: false, identity: true),
                        UserID = c.Int(nullable: false),
                        setWorkex = c.Int(nullable: false),
                        setEducation = c.Int(nullable: false),
                        setSkills = c.Int(nullable: false),
                        setProject = c.Int(nullable: false),
                        setContact = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserSettingId)
                .ForeignKey("dbo.Users", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID);
            
            AddColumn("dbo.Users", "UserRole", c => c.String());
            DropColumn("dbo.Users", "Role");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "Role", c => c.String());
            DropForeignKey("dbo.UserSettings", "UserID", "dbo.Users");
            DropIndex("dbo.UserSettings", new[] { "UserID" });
            DropColumn("dbo.Users", "UserRole");
            DropTable("dbo.UserSettings");
        }
    }
}
