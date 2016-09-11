namespace TodoList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migracja2 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Task", newName: "TaskItem");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.TaskItem", newName: "Task");
        }
    }
}
