namespace ResumeBuilder.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class renameskilltoskillnameinskillsmodelandaddedvirtualpropertiesforskillinusersandvirtualpropertiesforusersinskills : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SkillsUsers",
                c => new
                    {
                        Skills_SkillID = c.Int(nullable: false),
                        User_UserID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Skills_SkillID, t.User_UserID })
                .ForeignKey("dbo.Skills", t => t.Skills_SkillID, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.User_UserID, cascadeDelete: true)
                .Index(t => t.Skills_SkillID)
                .Index(t => t.User_UserID);
            
            AddColumn("dbo.Skills", "SkillName", c => c.String());
            DropColumn("dbo.Skills", "Skill");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Skills", "Skill", c => c.String());
            DropForeignKey("dbo.SkillsUsers", "User_UserID", "dbo.Users");
            DropForeignKey("dbo.SkillsUsers", "Skills_SkillID", "dbo.Skills");
            DropIndex("dbo.SkillsUsers", new[] { "User_UserID" });
            DropIndex("dbo.SkillsUsers", new[] { "Skills_SkillID" });
            DropColumn("dbo.Skills", "SkillName");
            DropTable("dbo.SkillsUsers");
        }
    }
}
