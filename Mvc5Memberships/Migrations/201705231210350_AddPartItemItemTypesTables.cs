namespace Mvc5Memberships.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPartItemItemTypesTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Item",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 255),
                        Description = c.String(maxLength: 2048),
                        Url = c.String(maxLength: 1024),
                        ImageUrl = c.String(maxLength: 1024),
                        Html = c.String(),
                        WaitDays = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        ItemTypeId = c.Int(nullable: false),
                        SectionId = c.Int(nullable: false),
                        PartId = c.Int(nullable: false),
                        IsFree = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ItemType",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 255),
                        Item_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Item", t => t.Item_Id)
                .Index(t => t.Item_Id);
            
            CreateTable(
                "dbo.Part",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 255),
                        Item_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Item", t => t.Item_Id)
                .Index(t => t.Item_Id);
            
            AddColumn("dbo.Section", "Item_Id", c => c.Int());
            CreateIndex("dbo.Section", "Item_Id");
            AddForeignKey("dbo.Section", "Item_Id", "dbo.Item", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Section", "Item_Id", "dbo.Item");
            DropForeignKey("dbo.Part", "Item_Id", "dbo.Item");
            DropForeignKey("dbo.ItemType", "Item_Id", "dbo.Item");
            DropIndex("dbo.Section", new[] { "Item_Id" });
            DropIndex("dbo.Part", new[] { "Item_Id" });
            DropIndex("dbo.ItemType", new[] { "Item_Id" });
            DropColumn("dbo.Section", "Item_Id");
            DropTable("dbo.Part");
            DropTable("dbo.ItemType");
            DropTable("dbo.Item");
        }
    }
}
