namespace EShop.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addVirtualProps : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OptionValue", "ProductSKU_ProductId", c => c.Long());
            AddColumn("dbo.OptionValue", "ProductSKU_SKUId", c => c.Long());
            CreateIndex("dbo.OptionValue", new[] { "ProductSKU_ProductId", "ProductSKU_SKUId" });
            AddForeignKey("dbo.OptionValue", new[] { "ProductSKU_ProductId", "ProductSKU_SKUId" }, "dbo.ProductSKU", new[] { "ProductId", "SKUId" });
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OptionValue", new[] { "ProductSKU_ProductId", "ProductSKU_SKUId" }, "dbo.ProductSKU");
            DropIndex("dbo.OptionValue", new[] { "ProductSKU_ProductId", "ProductSKU_SKUId" });
            DropColumn("dbo.OptionValue", "ProductSKU_SKUId");
            DropColumn("dbo.OptionValue", "ProductSKU_ProductId");
        }
    }
}
