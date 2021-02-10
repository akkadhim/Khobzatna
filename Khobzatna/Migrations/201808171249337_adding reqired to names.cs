namespace Khobzatna.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingreqiredtonames : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Beneficiaries", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Donors", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Volunteers", "Name", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Volunteers", "Name", c => c.String());
            AlterColumn("dbo.Donors", "Name", c => c.String());
            AlterColumn("dbo.Beneficiaries", "Name", c => c.String());
        }
    }
}
