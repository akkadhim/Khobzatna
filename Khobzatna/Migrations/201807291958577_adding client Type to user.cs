namespace Khobzatna.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingclientTypetouser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "ClientType", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "ClientType");
        }
    }
}
