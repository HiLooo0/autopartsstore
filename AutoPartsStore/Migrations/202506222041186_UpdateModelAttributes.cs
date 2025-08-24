namespace AutoPartsStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateModelAttributes : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Employees", "FullName", c => c.String());
            AlterColumn("dbo.Employees", "Position", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Employees", "Position", c => c.String(nullable: false));
            AlterColumn("dbo.Employees", "FullName", c => c.String(nullable: false));
        }
    }
}
