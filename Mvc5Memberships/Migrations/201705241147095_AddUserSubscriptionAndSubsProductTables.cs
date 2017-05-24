namespace Mvc5Memberships.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserSubscriptionAndSubsProductTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SubscriptionProduct",
                c => new
                    {
                        ProductId = c.Int(nullable: false),
                        SubscriptionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProductId, t.SubscriptionId });
            
            CreateTable(
                "dbo.UserSubscription",
                c => new
                    {
                        SubscriptionId = c.Int(nullable: false),
                        UserId = c.String(nullable: false, maxLength: 128),
                        StartDate = c.DateTime(),
                        EndDate = c.DateTime(),
                    })
                .PrimaryKey(t => new { t.SubscriptionId, t.UserId });
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UserSubscription");
            DropTable("dbo.SubscriptionProduct");
        }
    }
}
