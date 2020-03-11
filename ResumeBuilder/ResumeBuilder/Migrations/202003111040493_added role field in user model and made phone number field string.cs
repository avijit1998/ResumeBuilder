namespace ResumeBuilder.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedrolefieldinusermodelandmadephonenumberfieldstring : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "PhoneNumber", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "PhoneNumber", c => c.Int());
        }
    }
}
