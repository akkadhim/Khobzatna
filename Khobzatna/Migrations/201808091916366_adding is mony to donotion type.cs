namespace Khobzatna.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingismonytodonotiontype : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DonationTypes", "IsMoney", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DonationTypes", "IsMoney");
        }
    }
}
