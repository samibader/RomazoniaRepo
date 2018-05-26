namespace EShop.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Adding_Discount_Tables : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Discount", new[] { "ProductId", "SKUId" }, "dbo.ProductSKU");
            DropIndex("dbo.Discount", new[] { "ProductId", "SKUId" });
            CreateTable(
                "dbo.ProductDiscount",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        DiscountId = c.Long(nullable: false),
                        SKUId = c.Long(nullable: false),
                        ProductId = c.Long(nullable: false),
                        DateAdded = c.DateTime(),
                        DateModefied = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Discount", t => t.DiscountId)
                .ForeignKey("dbo.ProductSKU", t => new { t.ProductId, t.SKUId })
                .Index(t => t.DiscountId)
                .Index(t => new { t.ProductId, t.SKUId });
            
            CreateTable(
                "dbo.DiscountDescription",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        LanguageId = c.Long(nullable: false),
                        DiscountId = c.Long(nullable: false),
                        Name = c.String(),
                        DateAdded = c.DateTime(),
                        DateModefied = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Discount", t => t.DiscountId)
                .ForeignKey("dbo.Language", t => t.LanguageId)
                .Index(t => t.LanguageId)
                .Index(t => t.DiscountId);
            
            DropColumn("dbo.Discount", "ProductId");
            DropColumn("dbo.Discount", "SKUId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Discount", "SKUId", c => c.Long(nullable: false));
            AddColumn("dbo.Discount", "ProductId", c => c.Long(nullable: false));
            DropForeignKey("dbo.ProductDiscount", new[] { "ProductId", "SKUId" }, "dbo.ProductSKU");
            DropForeignKey("dbo.ProductDiscount", "DiscountId", "dbo.Discount");
            DropForeignKey("dbo.DiscountDescription", "LanguageId", "dbo.Language");
            DropForeignKey("dbo.DiscountDescription", "DiscountId", "dbo.Discount");
            DropIndex("dbo.DiscountDescription", new[] { "DiscountId" });
            DropIndex("dbo.DiscountDescription", new[] { "LanguageId" });
            DropIndex("dbo.ProductDiscount", new[] { "ProductId", "SKUId" });
            DropIndex("dbo.ProductDiscount", new[] { "DiscountId" });
            DropTable("dbo.DiscountDescription");
            DropTable("dbo.ProductDiscount");
            CreateIndex("dbo.Discount", new[] { "ProductId", "SKUId" });
            AddForeignKey("dbo.Discount", new[] { "ProductId", "SKUId" }, "dbo.ProductSKU", new[] { "ProductId", "SKUId" });
        }
    }
}
