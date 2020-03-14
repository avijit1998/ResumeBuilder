namespace ResumeBuilder.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deletedusersskillstableand : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UsersLanguages", "LanguageID", "dbo.Languages");
            DropForeignKey("dbo.UsersLanguages", "UserID", "dbo.Users");
            DropIndex("dbo.UsersLanguages", new[] { "UserID" });
            DropIndex("dbo.UsersLanguages", new[] { "LanguageID" });
            CreateTable(
                "dbo.LanguagesUsers",
                c => new
                    {
                        Languages_LanguageID = c.Int(nullable: false),
                        User_UserID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Languages_LanguageID, t.User_UserID })
                .ForeignKey("dbo.Languages", t => t.Languages_LanguageID, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.User_UserID, cascadeDelete: true)
                .Index(t => t.Languages_LanguageID)
                .Index(t => t.User_UserID);
            
            DropTable("dbo.UsersLanguages");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.UsersLanguages",
                c => new
                    {
                        UsersLanguageID = c.Int(nullable: false, identity: true),
                        UserID = c.Int(nullable: false),
                        LanguageID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UsersLanguageID);
            
            DropForeignKey("dbo.LanguagesUsers", "User_UserID", "dbo.Users");
            DropForeignKey("dbo.LanguagesUsers", "Languages_LanguageID", "dbo.Languages");
            DropIndex("dbo.LanguagesUsers", new[] { "User_UserID" });
            DropIndex("dbo.LanguagesUsers", new[] { "Languages_LanguageID" });
            DropTable("dbo.LanguagesUsers");
            CreateIndex("dbo.UsersLanguages", "LanguageID");
            CreateIndex("dbo.UsersLanguages", "UserID");
            AddForeignKey("dbo.UsersLanguages", "UserID", "dbo.Users", "UserID", cascadeDelete: true);
            AddForeignKey("dbo.UsersLanguages", "LanguageID", "dbo.Languages", "LanguageID", cascadeDelete: true);
        }
    }
}
