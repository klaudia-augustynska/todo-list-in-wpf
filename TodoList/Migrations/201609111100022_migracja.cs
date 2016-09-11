namespace TodoList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migracja : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Task", "DateCreated");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Task", "DateCreated", c => c.DateTime(nullable: false));
        }
    }
}
