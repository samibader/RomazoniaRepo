namespace EShop.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Removing_tag_description : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TagProduct", "Tag_Id", "dbo.Tag");
            DropForeignKey("dbo.TagProduct", "Product_Id", "dbo.Product");
            DropForeignKey("dbo.TagDescription", "LanguageId", "dbo.Language");
            DropForeignKey("dbo.TagDescription", "TagId", "dbo.Tag");
            DropIndex("dbo.TagDescription", new[] { "LanguageId" });
            DropIndex("dbo.TagDescription", new[] { "TagId" });
            DropIndex("dbo.TagProduct", new[] { "Tag_Id" });
            DropIndex("dbo.TagProduct", new[] { "Product_Id" });
            CreateTable(
                "dbo.TagProductSKU",
                c => new
                    {
                        Tag_Id = c.Long(nullable: false),
                        ProductSKU_ProductId = c.Long(nullable: false),
                        ProductSKU_SKUId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.Tag_Id, t.ProductSKU_ProductId, t.ProductSKU_SKUId })
                .ForeignKey("dbo.Tag", t => t.Tag_Id)
                .ForeignKey("dbo.ProductSKU", t => new { t.ProductSKU_ProductId, t.ProductSKU_SKUId })
                .Index(t => t.Tag_Id)
                .Index(t => new { t.ProductSKU_ProductId, t.ProductSKU_SKUId });
            
            AddColumn("dbo.Tag", "Text", c => c.String());
            AddColumn("dbo.Tag", "LanguageId", c => c.Long(nullable: false));
            AddColumn("dbo.Tag", "Product_Id", c => c.Long());
            CreateIndex("dbo.Tag", "LanguageId");
            CreateIndex("dbo.Tag", "Product_Id");
            AddForeignKey("dbo.Tag", "LanguageId", "dbo.Language", "Id");
            AddForeignKey("dbo.Tag", "Product_Id", "dbo.Product", "Id");
            DropTable("dbo.TagDescription");
            DropTable("dbo.TagProduct");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.TagProduct",
                c => new
                    {
                        Tag_Id = c.Long(nullable: false),
                        Product_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.Tag_Id, t.Product_Id });
            
            CreateTable(
                "dbo.TagDescription",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Text = c.String(),
                        LanguageId = c.Long(nullable: false),
                        TagId = c.Long(nullable: false),
                        DateAdded = c.DateTime(),
                        DateModefied = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            DropForeignKey("dbo.Tag", "Product_Id", "dbo.Product");
            DropForeignKey("dbo.TagProductSKU", new[] { "ProductSKU_ProductId", "ProductSKU_SKUId" }, "dbo.ProductSKU");
            DropForeignKey("dbo.TagProductSKU", "Tag_Id", "dbo.Tag");
            DropForeignKey("dbo.Tag", "LanguageId", "dbo.Language");
            DropIndex("dbo.TagProductSKU", new[] { "ProductSKU_ProductId", "ProductSKU_SKUId" });
            DropIndex("dbo.TagProductSKU", new[] { "Tag_Id" });
            DropIndex("dbo.Tag", new[] { "Product_Id" });
            DropIndex("dbo.Tag", new[] { "LanguageId" });
            DropColumn("dbo.Tag", "Product_Id");
            DropColumn("dbo.Tag", "LanguageId");
            DropColumn("dbo.Tag", "Text");
            DropTable("dbo.TagProductSKU");
            CreateIndex("dbo.TagProduct", "Product_Id");
            CreateIndex("dbo.TagProduct", "Tag_Id");
            CreateIndex("dbo.TagDescription", "TagId");
            CreateIndex("dbo.TagDescription", "LanguageId");
            AddForeignKey("dbo.TagDescription", "TagId", "dbo.Tag", "Id");
            AddForeignKey("dbo.TagDescription", "LanguageId", "dbo.Language", "Id");
            AddForeignKey("dbo.TagProduct", "Product_Id", "dbo.Product", "Id");
            AddForeignKey("dbo.TagProduct", "Tag_Id", "dbo.Tag", "Id");
        }
    }
}
