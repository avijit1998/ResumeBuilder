namespace ResumeBuilder.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addedcurrentlyworkinginworkexperiencemodel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.WorkExperiences", "CurrentlyWorking", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.WorkExperiences", "CurrentlyWorking");
        }
    }
}
