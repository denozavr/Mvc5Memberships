namespace Mvc5Memberships.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProductItemAndSubscriptionTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProductItem",
                c => new
                    {
                        ProductId = c.Int(nullable: false),
                        ItemId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProductId, t.ItemId });
            
            CreateTable(
                "dbo.Subscription",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 255),
                        Description = c.String(maxLength: 2048),
                        RegistrationCode = c.String(maxLength: 20),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Subscription");
            DropTable("dbo.ProductItem");
        }
    }
}
