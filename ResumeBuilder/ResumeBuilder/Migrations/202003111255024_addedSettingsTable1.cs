namespace ResumeBuilder.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedSettingsTable1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserSettings",
                c => new
                    {
                        setUserId = c.Int(nullable: false),
                        setWorkex = c.Int(nullable: false),
                        setEducation = c.Int(nullable: false),
                        setSkills = c.Int(nullable: false),
                        setProject = c.Int(nullable: false),
                        setContact = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.setUserId)
                .ForeignKey("dbo.Users", t => t.setUserId)
                .Index(t => t.setUserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserSettings", "setUserId", "dbo.Users");
            DropIndex("dbo.UserSettings", new[] { "setUserId" });
            DropTable("dbo.UserSettings");
        }
    }
}
