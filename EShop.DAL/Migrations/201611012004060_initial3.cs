namespace EShop.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProductSKU",
                c => new
                    {
                        ProductId = c.Long(nullable: false),
                        SKUId = c.Long(nullable: false),
                        SKU = c.String(),
                    })
                .PrimaryKey(t => new { t.ProductId, t.SKUId })
                .ForeignKey("dbo.Product", t => t.ProductId)
                .Index(t => t.ProductId);
            
            CreateIndex("dbo.Option", "ProductId");
            AddForeignKey("dbo.OptionValueDescription", "ProductId", "dbo.Product", "Id");
            AddForeignKey("dbo.OptionValue", "ProductId", "dbo.Product", "Id");
            AddForeignKey("dbo.Option", "ProductId", "dbo.Product", "Id");
            AddForeignKey("dbo.OptionDescription", "ProductId", "dbo.Product", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductSKU", "ProductId", "dbo.Product");
            DropForeignKey("dbo.OptionDescription", "ProductId", "dbo.Product");
            DropForeignKey("dbo.Option", "ProductId", "dbo.Product");
            DropForeignKey("dbo.OptionValue", "ProductId", "dbo.Product");
            DropForeignKey("dbo.OptionValueDescription", "ProductId", "dbo.Product");
            DropIndex("dbo.ProductSKU", new[] { "ProductId" });
            DropIndex("dbo.Option", new[] { "ProductId" });
            DropTable("dbo.ProductSKU");
        }
    }
}
