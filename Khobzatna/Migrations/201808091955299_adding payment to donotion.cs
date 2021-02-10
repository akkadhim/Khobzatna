namespace Khobzatna.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingpaymenttodonotion : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Donations", "Payment", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Donations", "Payment");
        }
    }
}
