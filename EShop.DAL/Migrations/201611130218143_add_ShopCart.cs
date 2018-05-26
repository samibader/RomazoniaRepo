namespace EShop.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_ShopCart : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ShoppingCart",
                c => new
                    {
                        ProductId = c.Long(nullable: false),
                        SKUId = c.Long(nullable: false),
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        Quantity = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .ForeignKey("dbo.ProductSKU", t => new { t.ProductId, t.SKUId })
                .Index(t => new { t.ProductId, t.SKUId })
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ShoppingCart", new[] { "ProductId", "SKUId" }, "dbo.ProductSKU");
            DropForeignKey("dbo.ShoppingCart", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.ShoppingCart", new[] { "UserId" });
            DropIndex("dbo.ShoppingCart", new[] { "ProductId", "SKUId" });
            DropTable("dbo.ShoppingCart");
        }
    }
}
