namespace ResumeBuilder.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class madefewchanges : DbMigration
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserSettings", "UserID", "dbo.Users");
            DropIndex("dbo.UserSettings", new[] { "UserID" });
            DropTable("dbo.UserSettings");
        }
    }
}
