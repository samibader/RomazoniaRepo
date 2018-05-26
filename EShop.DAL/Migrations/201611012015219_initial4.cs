namespace EShop.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial4 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProductSKUOptionValue",
                c => new
                    {
                        ProductId = c.Long(nullable: false),
                        SKUId = c.Long(nullable: false),
                        OptionId = c.Long(nullable: false),
                        ValueId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProductId, t.SKUId, t.OptionId, t.ValueId })
                .ForeignKey("dbo.Option", t => new { t.ProductId, t.OptionId })
                .ForeignKey("dbo.OptionValue", t => new { t.ProductId, t.OptionId, t.ValueId })
                .ForeignKey("dbo.Product", t => t.ProductId)
                .ForeignKey("dbo.ProductSKU", t => new { t.ProductId, t.SKUId })
                .Index(t => new { t.ProductId, t.OptionId })
                .Index(t => new { t.ProductId, t.OptionId, t.ValueId })
                .Index(t => new { t.ProductId, t.SKUId });
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductSKUOptionValue", new[] { "ProductId", "SKUId" }, "dbo.ProductSKU");
            DropForeignKey("dbo.ProductSKUOptionValue", "ProductId", "dbo.Product");
            DropForeignKey("dbo.ProductSKUOptionValue", new[] { "ProductId", "OptionId", "ValueId" }, "dbo.OptionValue");
            DropForeignKey("dbo.ProductSKUOptionValue", new[] { "ProductId", "OptionId" }, "dbo.Option");
            DropIndex("dbo.ProductSKUOptionValue", new[] { "ProductId", "SKUId" });
            DropIndex("dbo.ProductSKUOptionValue", new[] { "ProductId", "OptionId", "ValueId" });
            DropIndex("dbo.ProductSKUOptionValue", new[] { "ProductId", "OptionId" });
            DropTable("dbo.ProductSKUOptionValue");
        }
    }
}
