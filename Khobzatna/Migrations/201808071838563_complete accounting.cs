namespace Khobzatna.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class completeaccounting : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Jobs", "TransactionId", "dbo.Transactions");
            DropForeignKey("dbo.Jobs", "BeneficiaryId", "dbo.Beneficiaries");
            DropForeignKey("dbo.Jobs", "DonorId", "dbo.Donors");
            DropForeignKey("dbo.Jobs", "VolunteerId", "dbo.Volunteers");
            DropIndex("dbo.Jobs", new[] { "DonorId" });
            DropIndex("dbo.Jobs", new[] { "VolunteerId" });
            DropIndex("dbo.Jobs", new[] { "BeneficiaryId" });
            DropIndex("dbo.Jobs", new[] { "TransactionId" });
            CreateTable(
                "dbo.Accounts",
                c => new
                    {
                        AccountId = c.Int(nullable: false, identity: true),
                        AccountNo = c.String(),
                        Name = c.String(maxLength: 450),
                        EnglishName = c.String(),
                        FriendlyName = c.String(),
                        ParentId = c.Int(),
                        Level = c.Int(nullable: false),
                        Disabled = c.Boolean(nullable: false),
                        DateOfCreation = c.DateTime(nullable: false),
                        LastMatching = c.DateTime(nullable: false),
                        IsCredit = c.Boolean(nullable: false),
                        Credit = c.Double(nullable: false),
                        Debit = c.Double(nullable: false),
                        Balance = c.Double(nullable: false),
                        Currency = c.Int(nullable: false),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.AccountId)
                .ForeignKey("dbo.Accounts", t => t.ParentId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.ParentId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Operations",
                c => new
                    {
                        OperationId = c.Long(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        OperationTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.OperationId)
                .ForeignKey("dbo.OperationTypes", t => t.OperationTypeId, cascadeDelete: true)
                .Index(t => t.OperationTypeId);
            
            CreateTable(
                "dbo.OperationTypes",
                c => new
                    {
                        OperationTypeId = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 450),
                        IsInMenu = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.OperationTypeId);
            
            CreateTable(
                "dbo.OperationActions",
                c => new
                    {
                        OperationActionId = c.Long(nullable: false, identity: true),
                        OperationTypeId = c.Int(nullable: false),
                        AccountId = c.Int(nullable: false),
                        Direction = c.Int(nullable: false),
                        ActionType = c.Int(nullable: false),
                        IsFinalNode = c.Boolean(nullable: false),
                        IsVisible = c.Boolean(nullable: false),
                        HasVehicle = c.Boolean(nullable: false),
                        HasContainer = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.OperationActionId)
                .ForeignKey("dbo.Accounts", t => t.AccountId, cascadeDelete: true)
                .ForeignKey("dbo.OperationTypes", t => t.OperationTypeId, cascadeDelete: true)
                .Index(t => t.OperationTypeId)
                .Index(t => t.AccountId);
            
            CreateTable(
                "dbo.Requests",
                c => new
                    {
                        RequestId = c.Long(nullable: false, identity: true),
                        Note = c.String(),
                        Date = c.DateTime(nullable: false),
                        Status = c.Int(nullable: false),
                        BeneficiaryId = c.Int(nullable: false),
                        RequestTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RequestId)
                .ForeignKey("dbo.Beneficiaries", t => t.BeneficiaryId, cascadeDelete: true)
                .ForeignKey("dbo.RequestTypes", t => t.RequestTypeId, cascadeDelete: true)
                .Index(t => t.BeneficiaryId)
                .Index(t => t.RequestTypeId);
            
            CreateTable(
                "dbo.RequestTypes",
                c => new
                    {
                        RequestTypeId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Note = c.String(),
                    })
                .PrimaryKey(t => t.RequestTypeId);
            
            CreateTable(
                "dbo.ToDoTasks",
                c => new
                    {
                        ToDoTaskId = c.Long(nullable: false, identity: true),
                        Status = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ToDoTaskId);
            
            AddColumn("dbo.Donations", "Date", c => c.DateTime(nullable: false));
            AddColumn("dbo.Donations", "Status", c => c.Int(nullable: false));
            AddColumn("dbo.Donations", "DonorId", c => c.Int(nullable: false));
            AddColumn("dbo.Jobs", "Note", c => c.String());
            AddColumn("dbo.Jobs", "ToDoTaskId", c => c.Long(nullable: false));
            AddColumn("dbo.Jobs", "RequestId", c => c.Long());
            AddColumn("dbo.Jobs", "OperationId", c => c.Long(nullable: false));
            AddColumn("dbo.Transactions", "TransactionNo", c => c.String());
            AddColumn("dbo.Transactions", "Description", c => c.String());
            AddColumn("dbo.Transactions", "UserNote", c => c.String());
            AddColumn("dbo.Transactions", "Debit", c => c.Double(nullable: false));
            AddColumn("dbo.Transactions", "Credit", c => c.Double(nullable: false));
            AddColumn("dbo.Transactions", "Balance", c => c.Double(nullable: false));
            AddColumn("dbo.Transactions", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Transactions", "IsPosted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Transactions", "IsArchive", c => c.Boolean(nullable: false));
            AddColumn("dbo.Transactions", "OperationId", c => c.Long(nullable: false));
            AddColumn("dbo.Transactions", "AccountId", c => c.Int(nullable: false));
            AlterColumn("dbo.Beneficiaries", "UserId", c => c.String(maxLength: 128));
            AlterColumn("dbo.Donors", "UserId", c => c.String(maxLength: 128));
            AlterColumn("dbo.Jobs", "DonorId", c => c.Int());
            AlterColumn("dbo.Jobs", "VolunteerId", c => c.Int());
            AlterColumn("dbo.Jobs", "BeneficiaryId", c => c.Int());
            AlterColumn("dbo.Volunteers", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Beneficiaries", "UserId");
            CreateIndex("dbo.Jobs", "DonorId");
            CreateIndex("dbo.Jobs", "VolunteerId");
            CreateIndex("dbo.Jobs", "BeneficiaryId");
            CreateIndex("dbo.Jobs", "ToDoTaskId");
            CreateIndex("dbo.Jobs", "RequestId");
            CreateIndex("dbo.Jobs", "OperationId");
            CreateIndex("dbo.Donations", "DonorId");
            CreateIndex("dbo.Donors", "UserId");
            CreateIndex("dbo.Transactions", "OperationId");
            CreateIndex("dbo.Transactions", "AccountId");
            CreateIndex("dbo.Volunteers", "UserId");
            AddForeignKey("dbo.Donations", "DonorId", "dbo.Donors", "DonorId", cascadeDelete: true);
            AddForeignKey("dbo.Donors", "UserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Transactions", "AccountId", "dbo.Accounts", "AccountId", cascadeDelete: true);
            AddForeignKey("dbo.Transactions", "OperationId", "dbo.Operations", "OperationId", cascadeDelete: true);
            AddForeignKey("dbo.Jobs", "OperationId", "dbo.Operations", "OperationId", cascadeDelete: true);
            AddForeignKey("dbo.Jobs", "RequestId", "dbo.Requests", "RequestId");
            AddForeignKey("dbo.Jobs", "ToDoTaskId", "dbo.ToDoTasks", "ToDoTaskId", cascadeDelete: true);
            AddForeignKey("dbo.Volunteers", "UserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Beneficiaries", "UserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Jobs", "BeneficiaryId", "dbo.Beneficiaries", "BeneficiaryId");
            AddForeignKey("dbo.Jobs", "DonorId", "dbo.Donors", "DonorId");
            AddForeignKey("dbo.Jobs", "VolunteerId", "dbo.Volunteers", "VolunteerId");
            DropColumn("dbo.Jobs", "TransactionId");
            DropColumn("dbo.Transactions", "Direction");
            DropColumn("dbo.Transactions", "TotalIQD");
            DropColumn("dbo.Transactions", "TotalUSD");
            DropColumn("dbo.Transactions", "Note");
            DropColumn("dbo.Transactions", "Approved");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Transactions", "Approved", c => c.Boolean(nullable: false));
            AddColumn("dbo.Transactions", "Note", c => c.String());
            AddColumn("dbo.Transactions", "TotalUSD", c => c.Long(nullable: false));
            AddColumn("dbo.Transactions", "TotalIQD", c => c.Long(nullable: false));
            AddColumn("dbo.Transactions", "Direction", c => c.Int(nullable: false));
            AddColumn("dbo.Jobs", "TransactionId", c => c.Long());
            DropForeignKey("dbo.Jobs", "VolunteerId", "dbo.Volunteers");
            DropForeignKey("dbo.Jobs", "DonorId", "dbo.Donors");
            DropForeignKey("dbo.Jobs", "BeneficiaryId", "dbo.Beneficiaries");
            DropForeignKey("dbo.Beneficiaries", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Volunteers", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Jobs", "ToDoTaskId", "dbo.ToDoTasks");
            DropForeignKey("dbo.Jobs", "RequestId", "dbo.Requests");
            DropForeignKey("dbo.Requests", "RequestTypeId", "dbo.RequestTypes");
            DropForeignKey("dbo.Requests", "BeneficiaryId", "dbo.Beneficiaries");
            DropForeignKey("dbo.Jobs", "OperationId", "dbo.Operations");
            DropForeignKey("dbo.Transactions", "OperationId", "dbo.Operations");
            DropForeignKey("dbo.Transactions", "AccountId", "dbo.Accounts");
            DropForeignKey("dbo.Operations", "OperationTypeId", "dbo.OperationTypes");
            DropForeignKey("dbo.OperationActions", "OperationTypeId", "dbo.OperationTypes");
            DropForeignKey("dbo.OperationActions", "AccountId", "dbo.Accounts");
            DropForeignKey("dbo.Donors", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Donations", "DonorId", "dbo.Donors");
            DropForeignKey("dbo.Accounts", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Accounts", "ParentId", "dbo.Accounts");
            DropIndex("dbo.Volunteers", new[] { "UserId" });
            DropIndex("dbo.Requests", new[] { "RequestTypeId" });
            DropIndex("dbo.Requests", new[] { "BeneficiaryId" });
            DropIndex("dbo.Transactions", new[] { "AccountId" });
            DropIndex("dbo.Transactions", new[] { "OperationId" });
            DropIndex("dbo.OperationActions", new[] { "AccountId" });
            DropIndex("dbo.OperationActions", new[] { "OperationTypeId" });
            DropIndex("dbo.Operations", new[] { "OperationTypeId" });
            DropIndex("dbo.Donors", new[] { "UserId" });
            DropIndex("dbo.Donations", new[] { "DonorId" });
            DropIndex("dbo.Jobs", new[] { "OperationId" });
            DropIndex("dbo.Jobs", new[] { "RequestId" });
            DropIndex("dbo.Jobs", new[] { "ToDoTaskId" });
            DropIndex("dbo.Jobs", new[] { "BeneficiaryId" });
            DropIndex("dbo.Jobs", new[] { "VolunteerId" });
            DropIndex("dbo.Jobs", new[] { "DonorId" });
            DropIndex("dbo.Beneficiaries", new[] { "UserId" });
            DropIndex("dbo.Accounts", new[] { "UserId" });
            DropIndex("dbo.Accounts", new[] { "ParentId" });
            AlterColumn("dbo.Volunteers", "UserId", c => c.String());
            AlterColumn("dbo.Jobs", "BeneficiaryId", c => c.Int(nullable: false));
            AlterColumn("dbo.Jobs", "VolunteerId", c => c.Int(nullable: false));
            AlterColumn("dbo.Jobs", "DonorId", c => c.Int(nullable: false));
            AlterColumn("dbo.Donors", "UserId", c => c.String());
            AlterColumn("dbo.Beneficiaries", "UserId", c => c.String());
            DropColumn("dbo.Transactions", "AccountId");
            DropColumn("dbo.Transactions", "OperationId");
            DropColumn("dbo.Transactions", "IsArchive");
            DropColumn("dbo.Transactions", "IsPosted");
            DropColumn("dbo.Transactions", "IsDeleted");
            DropColumn("dbo.Transactions", "Balance");
            DropColumn("dbo.Transactions", "Credit");
            DropColumn("dbo.Transactions", "Debit");
            DropColumn("dbo.Transactions", "UserNote");
            DropColumn("dbo.Transactions", "Description");
            DropColumn("dbo.Transactions", "TransactionNo");
            DropColumn("dbo.Jobs", "OperationId");
            DropColumn("dbo.Jobs", "RequestId");
            DropColumn("dbo.Jobs", "ToDoTaskId");
            DropColumn("dbo.Jobs", "Note");
            DropColumn("dbo.Donations", "DonorId");
            DropColumn("dbo.Donations", "Status");
            DropColumn("dbo.Donations", "Date");
            DropTable("dbo.ToDoTasks");
            DropTable("dbo.RequestTypes");
            DropTable("dbo.Requests");
            DropTable("dbo.OperationActions");
            DropTable("dbo.OperationTypes");
            DropTable("dbo.Operations");
            DropTable("dbo.Accounts");
            CreateIndex("dbo.Jobs", "TransactionId");
            CreateIndex("dbo.Jobs", "BeneficiaryId");
            CreateIndex("dbo.Jobs", "VolunteerId");
            CreateIndex("dbo.Jobs", "DonorId");
            AddForeignKey("dbo.Jobs", "VolunteerId", "dbo.Volunteers", "VolunteerId", cascadeDelete: true);
            AddForeignKey("dbo.Jobs", "DonorId", "dbo.Donors", "DonorId", cascadeDelete: true);
            AddForeignKey("dbo.Jobs", "BeneficiaryId", "dbo.Beneficiaries", "BeneficiaryId", cascadeDelete: true);
            AddForeignKey("dbo.Jobs", "TransactionId", "dbo.Transactions", "TransactionId");
        }
    }
}
