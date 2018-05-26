namespace EShop.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class favoritefix : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Favorite", "ProductId", "dbo.Product");
            DropIndex("dbo.Favorite", new[] { "ProductId" });
            AddColumn("dbo.Favorite", "SKUId", c => c.Long(nullable: false));
            CreateIndex("dbo.Favorite", new[] { "ProductId", "SKUId" });
            AddForeignKey("dbo.Favorite", new[] { "ProductId", "SKUId" }, "dbo.ProductSKU", new[] { "ProductId", "SKUId" });
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Favorite", new[] { "ProductId", "SKUId" }, "dbo.ProductSKU");
            DropIndex("dbo.Favorite", new[] { "ProductId", "SKUId" });
            DropColumn("dbo.Favorite", "SKUId");
            CreateIndex("dbo.Favorite", "ProductId");
            AddForeignKey("dbo.Favorite", "ProductId", "dbo.Product", "Id");
        }
    }
}
