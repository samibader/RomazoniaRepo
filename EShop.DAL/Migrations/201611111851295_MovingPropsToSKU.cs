namespace EShop.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MovingPropsToSKU : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Discount", "ProductId", "dbo.Product");
            DropForeignKey("dbo.OptionValue", new[] { "ProductSKU_ProductId", "ProductSKU_SKUId" }, "dbo.ProductSKU");
            DropIndex("dbo.Discount", new[] { "ProductId" });
            DropIndex("dbo.OptionValue", new[] { "ProductSKU_ProductId", "ProductSKU_SKUId" });
            AddColumn("dbo.Discount", "ProductSKUId", c => c.Long(nullable: false));
            AddColumn("dbo.Discount", "ProductSKU_ProductId", c => c.Long());
            AddColumn("dbo.Discount", "ProductSKU_SKUId", c => c.Long());
            AddColumn("dbo.OptionValue", "ImageUrl", c => c.String());
            AddColumn("dbo.ProductSKU", "Price", c => c.Int(nullable: false));
            AddColumn("dbo.ProductSKU", "IsNew", c => c.Boolean(nullable: false));
            AddColumn("dbo.ProductSKU", "Quentity", c => c.Int(nullable: false));
            AlterColumn("dbo.Discount", "Value", c => c.Double(nullable: false));
            CreateIndex("dbo.Discount", new[] { "ProductSKU_ProductId", "ProductSKU_SKUId" });
            AddForeignKey("dbo.Discount", new[] { "ProductSKU_ProductId", "ProductSKU_SKUId" }, "dbo.ProductSKU", new[] { "ProductId", "SKUId" });
            DropColumn("dbo.Discount", "ProductId");
            DropColumn("dbo.OptionValue", "ProductSKU_ProductId");
            DropColumn("dbo.OptionValue", "ProductSKU_SKUId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.OptionValue", "ProductSKU_SKUId", c => c.Long());
            AddColumn("dbo.OptionValue", "ProductSKU_ProductId", c => c.Long());
            AddColumn("dbo.Discount", "ProductId", c => c.Long(nullable: false));
            DropForeignKey("dbo.Discount", new[] { "ProductSKU_ProductId", "ProductSKU_SKUId" }, "dbo.ProductSKU");
            DropIndex("dbo.Discount", new[] { "ProductSKU_ProductId", "ProductSKU_SKUId" });
            AlterColumn("dbo.Discount", "Value", c => c.Int(nullable: false));
            DropColumn("dbo.ProductSKU", "Quentity");
            DropColumn("dbo.ProductSKU", "IsNew");
            DropColumn("dbo.ProductSKU", "Price");
            DropColumn("dbo.OptionValue", "ImageUrl");
            DropColumn("dbo.Discount", "ProductSKU_SKUId");
            DropColumn("dbo.Discount", "ProductSKU_ProductId");
            DropColumn("dbo.Discount", "ProductSKUId");
            CreateIndex("dbo.OptionValue", new[] { "ProductSKU_ProductId", "ProductSKU_SKUId" });
            CreateIndex("dbo.Discount", "ProductId");
            AddForeignKey("dbo.OptionValue", new[] { "ProductSKU_ProductId", "ProductSKU_SKUId" }, "dbo.ProductSKU", new[] { "ProductId", "SKUId" });
            AddForeignKey("dbo.Discount", "ProductId", "dbo.Product", "Id");
        }
    }
}
