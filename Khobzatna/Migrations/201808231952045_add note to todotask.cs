namespace Khobzatna.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addnotetotodotask : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ToDoTasks", "Note", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ToDoTasks", "Note");
        }
    }
}
