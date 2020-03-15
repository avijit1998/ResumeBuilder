namespace ResumeBuilder.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class thereissomechangeswhichIdontknow : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UsersSkills", "SkillID", "dbo.Skills");
            DropForeignKey("dbo.UsersSkills", "UserID", "dbo.Users");
            DropIndex("dbo.UsersSkills", new[] { "UserID" });
            DropIndex("dbo.UsersSkills", new[] { "SkillID" });
            DropTable("dbo.UsersSkills");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.UsersSkills",
                c => new
                    {
                        UserSkillID = c.Int(nullable: false, identity: true),
                        UserID = c.Int(nullable: false),
                        SkillID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserSkillID);
            
            CreateIndex("dbo.UsersSkills", "SkillID");
            CreateIndex("dbo.UsersSkills", "UserID");
            AddForeignKey("dbo.UsersSkills", "UserID", "dbo.Users", "UserID", cascadeDelete: true);
            AddForeignKey("dbo.UsersSkills", "SkillID", "dbo.Skills", "SkillID", cascadeDelete: true);
        }
    }
}
