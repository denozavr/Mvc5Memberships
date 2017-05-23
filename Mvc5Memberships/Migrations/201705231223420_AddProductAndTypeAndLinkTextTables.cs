namespace Mvc5Memberships.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddProductAndTypeAndLinkTextTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProductLinkText",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 255),
                        Product_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Product", t => t.Product_Id)
                .Index(t => t.Product_Id);
            
            CreateTable(
                "dbo.Product",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 255),
                        Description = c.String(maxLength: 2048),
                        ImageUrl = c.String(maxLength: 1024),
                        ProductLinkTextId = c.Int(nullable: false),
                        ProductTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProductType",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 50),
                        Product_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Product", t => t.Product_Id)
                .Index(t => t.Product_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductType", "Product_Id", "dbo.Product");
            DropForeignKey("dbo.ProductLinkText", "Product_Id", "dbo.Product");
            DropIndex("dbo.ProductType", new[] { "Product_Id" });
            DropIndex("dbo.ProductLinkText", new[] { "Product_Id" });
            DropTable("dbo.ProductType");
            DropTable("dbo.Product");
            DropTable("dbo.ProductLinkText");
        }
    }
}
