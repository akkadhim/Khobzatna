namespace Khobzatna.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatejob : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Jobs", "DonorId");
            CreateIndex("dbo.Jobs", "VolunteerId");
            CreateIndex("dbo.Jobs", "BeneficiaryId");
            AddForeignKey("dbo.Jobs", "BeneficiaryId", "dbo.Beneficiaries", "BeneficiaryId", cascadeDelete: true);
            AddForeignKey("dbo.Jobs", "DonorId", "dbo.Donors", "DonorId", cascadeDelete: true);
            AddForeignKey("dbo.Jobs", "VolunteerId", "dbo.Volunteers", "VolunteerId", cascadeDelete: true);
            DropColumn("dbo.Jobs", "DonorName");
            DropColumn("dbo.Jobs", "VolunteerName");
            DropColumn("dbo.Jobs", "BeneficiaryName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Jobs", "BeneficiaryName", c => c.String());
            AddColumn("dbo.Jobs", "VolunteerName", c => c.String());
            AddColumn("dbo.Jobs", "DonorName", c => c.String());
            DropForeignKey("dbo.Jobs", "VolunteerId", "dbo.Volunteers");
            DropForeignKey("dbo.Jobs", "DonorId", "dbo.Donors");
            DropForeignKey("dbo.Jobs", "BeneficiaryId", "dbo.Beneficiaries");
            DropIndex("dbo.Jobs", new[] { "BeneficiaryId" });
            DropIndex("dbo.Jobs", new[] { "VolunteerId" });
            DropIndex("dbo.Jobs", new[] { "DonorId" });
        }
    }
}
