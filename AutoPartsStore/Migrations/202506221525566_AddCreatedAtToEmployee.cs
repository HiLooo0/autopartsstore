
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCreatedAtToEmployee : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employees", "CreatedAt", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Employees", "CreatedAt");
        }
    }

