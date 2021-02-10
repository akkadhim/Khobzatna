namespace Khobzatna.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class firstversion : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Beneficiaries",
                c => new
                    {
                        BeneficiaryId = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        Name = c.String(),
                        Address = c.String(),
                        GPS = c.String(),
                        FamilyNo = c.Int(nullable: false),
                        age = c.Int(nullable: false),
                        Note = c.String(),
                    })
                .PrimaryKey(t => t.BeneficiaryId);
            
            CreateTable(
                "dbo.Donations",
                c => new
                    {
                        DonationId = c.Long(nullable: false, identity: true),
                        DonationTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.DonationId)
                .ForeignKey("dbo.DonationTypes", t => t.DonationTypeId, cascadeDelete: true)
                .Index(t => t.DonationTypeId);
            
            CreateTable(
                "dbo.DonationTypes",
                c => new
                    {
                        DonationTypeId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.DonationTypeId);
            
            CreateTable(
                "dbo.Donors",
                c => new
                    {
                        DonorId = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        Name = c.String(),
                        Address = c.String(),
                        DonationQty = c.Int(nullable: false),
                        Private = c.Boolean(nullable: false),
                        Job = c.String(),
                        Note = c.String(),
                    })
                .PrimaryKey(t => t.DonorId);
            
            CreateTable(
                "dbo.Jobs",
                c => new
                    {
                        JobId = c.Long(nullable: false, identity: true),
                        DonorName = c.String(),
                        DonorId = c.Int(nullable: false),
                        VolunteerName = c.String(),
                        VolunteerId = c.Int(nullable: false),
                        BeneficiaryName = c.String(),
                        BeneficiaryId = c.Int(nullable: false),
                        DonationId = c.Long(nullable: false),
                        TransactionId = c.Long(nullable: false),
                        Status = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.JobId)
                .ForeignKey("dbo.Donations", t => t.DonationId, cascadeDelete: true)
                .ForeignKey("dbo.Transactions", t => t.TransactionId, cascadeDelete: true)
                .Index(t => t.DonationId)
                .Index(t => t.TransactionId);
            
            CreateTable(
                "dbo.Transactions",
                c => new
                    {
                        TransactionId = c.Long(nullable: false, identity: true),
                        Direction = c.Int(nullable: false),
                        TotalIQD = c.Long(nullable: false),
                        TotalUSD = c.Long(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Approved = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.TransactionId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Volunteers",
                c => new
                    {
                        VolunteerId = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        Name = c.String(),
                        Address = c.String(),
                        Job = c.String(),
                        FreeTime = c.String(),
                        Note = c.String(),
                    })
                .PrimaryKey(t => t.VolunteerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Jobs", "TransactionId", "dbo.Transactions");
            DropForeignKey("dbo.Jobs", "DonationId", "dbo.Donations");
            DropForeignKey("dbo.Donations", "DonationTypeId", "dbo.DonationTypes");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Jobs", new[] { "TransactionId" });
            DropIndex("dbo.Jobs", new[] { "DonationId" });
            DropIndex("dbo.Donations", new[] { "DonationTypeId" });
            DropTable("dbo.Volunteers");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Transactions");
            DropTable("dbo.Jobs");
            DropTable("dbo.Donors");
            DropTable("dbo.DonationTypes");
            DropTable("dbo.Donations");
            DropTable("dbo.Beneficiaries");
        }
    }
}
