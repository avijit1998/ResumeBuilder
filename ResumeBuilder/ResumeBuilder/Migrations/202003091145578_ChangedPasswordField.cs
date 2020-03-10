namespace ResumeBuilder.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedPasswordField : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.LoginViewModels");
            DropTable("dbo.RegisterViewModels");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.RegisterViewModels",
                c => new
                    {
                        RegisterViewModelId = c.Int(nullable: false, identity: true),
                        Email = c.String(nullable: false),
                        Password = c.String(nullable: false, maxLength: 100),
                        ConfirmPassword = c.String(),
                    })
                .PrimaryKey(t => t.RegisterViewModelId);
            
            CreateTable(
                "dbo.LoginViewModels",
                c => new
                    {
                        LoginViewModelId = c.Int(nullable: false, identity: true),
                        Email = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        RememberMe = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.LoginViewModelId);
            
            AlterColumn("dbo.Users", "Password", c => c.String(nullable: false));
        }
    }
}
