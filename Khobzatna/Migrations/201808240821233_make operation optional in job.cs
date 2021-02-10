namespace Khobzatna.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class makeoperationoptionalinjob : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Jobs", "OperationId", "dbo.Operations");
            DropIndex("dbo.Jobs", new[] { "OperationId" });
            AlterColumn("dbo.Jobs", "OperationId", c => c.Long());
            CreateIndex("dbo.Jobs", "OperationId");
            AddForeignKey("dbo.Jobs", "OperationId", "dbo.Operations", "OperationId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Jobs", "OperationId", "dbo.Operations");
            DropIndex("dbo.Jobs", new[] { "OperationId" });
            AlterColumn("dbo.Jobs", "OperationId", c => c.Long(nullable: false));
            CreateIndex("dbo.Jobs", "OperationId");
            AddForeignKey("dbo.Jobs", "OperationId", "dbo.Operations", "OperationId", cascadeDelete: true);
        }
    }
}
