namespace AutoPartsStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSubmissionDateToApplication : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Applications", "SubmissionDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Applications", "SubmissionDate");
        }
    }
}
