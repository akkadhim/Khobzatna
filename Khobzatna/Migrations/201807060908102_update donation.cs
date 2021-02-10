namespace Khobzatna.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedonation : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Jobs", "DonationId", "dbo.Donations");
            DropForeignKey("dbo.Jobs", "TransactionId", "dbo.Transactions");
            DropIndex("dbo.Jobs", new[] { "DonationId" });
            DropIndex("dbo.Jobs", new[] { "TransactionId" });
            AddColumn("dbo.Donations", "Note", c => c.String());
            AddColumn("dbo.DonationTypes", "Note", c => c.String());
            AddColumn("dbo.Transactions", "Note", c => c.String());
            AlterColumn("dbo.Jobs", "DonationId", c => c.Long());
            AlterColumn("dbo.Jobs", "TransactionId", c => c.Long());
            CreateIndex("dbo.Jobs", "DonationId");
            CreateIndex("dbo.Jobs", "TransactionId");
            AddForeignKey("dbo.Jobs", "DonationId", "dbo.Donations", "DonationId");
            AddForeignKey("dbo.Jobs", "TransactionId", "dbo.Transactions", "TransactionId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Jobs", "TransactionId", "dbo.Transactions");
            DropForeignKey("dbo.Jobs", "DonationId", "dbo.Donations");
            DropIndex("dbo.Jobs", new[] { "TransactionId" });
            DropIndex("dbo.Jobs", new[] { "DonationId" });
            AlterColumn("dbo.Jobs", "TransactionId", c => c.Long(nullable: false));
            AlterColumn("dbo.Jobs", "DonationId", c => c.Long(nullable: false));
            DropColumn("dbo.Transactions", "Note");
            DropColumn("dbo.DonationTypes", "Note");
            DropColumn("dbo.Donations", "Note");
            CreateIndex("dbo.Jobs", "TransactionId");
            CreateIndex("dbo.Jobs", "DonationId");
            AddForeignKey("dbo.Jobs", "TransactionId", "dbo.Transactions", "TransactionId", cascadeDelete: true);
            AddForeignKey("dbo.Jobs", "DonationId", "dbo.Donations", "DonationId", cascadeDelete: true);
        }
    }
}
