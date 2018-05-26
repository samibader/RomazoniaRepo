namespace EShop.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addoption : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ProductCategory", "Product_Id", "dbo.Product");
            DropForeignKey("dbo.ProductCategory", "Category_Id", "dbo.Category");
            DropForeignKey("dbo.OptionDescription", "LanguageId", "dbo.Language");
            DropForeignKey("dbo.OptionDescription", "OptionId", "dbo.Option");
            DropForeignKey("dbo.OptionValue", "OptionId", "dbo.Option");
            DropForeignKey("dbo.OptionValueDescription", "LanguageId", "dbo.Language");
            DropForeignKey("dbo.OptionValueDescription", "OptionId", "dbo.Option");
            DropForeignKey("dbo.OptionValueDescription", "OptionValueId", "dbo.OptionValue");
            DropForeignKey("dbo.ProductOptionValue", "OptionId", "dbo.Option");
            DropForeignKey("dbo.ProductOptionValue", "OptionValueId", "dbo.OptionValue");
            DropForeignKey("dbo.ProductOptionValue", "ProductId", "dbo.Product");
            DropForeignKey("dbo.ProductOptionValue", "ProductOptionId", "dbo.ProductOption");
            DropForeignKey("dbo.ProductOption", "OptionId", "dbo.Option");
            DropForeignKey("dbo.ProductOption", "ProductId", "dbo.Product");
            DropIndex("dbo.ProductOption", new[] { "ProductId" });
            DropIndex("dbo.ProductOption", new[] { "OptionId" });
            DropIndex("dbo.OptionDescription", new[] { "OptionId" });
            DropIndex("dbo.OptionDescription", new[] { "LanguageId" });
            DropIndex("dbo.OptionValue", new[] { "OptionId" });
            DropIndex("dbo.OptionValueDescription", new[] { "LanguageId" });
            DropIndex("dbo.OptionValueDescription", new[] { "OptionId" });
            DropIndex("dbo.OptionValueDescription", new[] { "OptionValueId" });
            DropIndex("dbo.ProductOptionValue", new[] { "ProductOptionId" });
            DropIndex("dbo.ProductOptionValue", new[] { "ProductId" });
            DropIndex("dbo.ProductOptionValue", new[] { "OptionId" });
            DropIndex("dbo.ProductOptionValue", new[] { "OptionValueId" });
            DropIndex("dbo.ProductCategory", new[] { "Product_Id" });
            DropIndex("dbo.ProductCategory", new[] { "Category_Id" });
            AddColumn("dbo.Product", "CategoryId", c => c.Long(nullable: false));
            CreateIndex("dbo.Product", "CategoryId");
            AddForeignKey("dbo.Product", "CategoryId", "dbo.Category", "Id");
            DropTable("dbo.ProductOption");
            DropTable("dbo.Option");
            DropTable("dbo.OptionDescription");
            DropTable("dbo.OptionValue");
            DropTable("dbo.OptionValueDescription");
            DropTable("dbo.ProductOptionValue");
            DropTable("dbo.ProductCategory");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ProductCategory",
                c => new
                    {
                        Product_Id = c.Long(nullable: false),
                        Category_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.Product_Id, t.Category_Id });
            
            CreateTable(
                "dbo.ProductOptionValue",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        ProductOptionId = c.Long(nullable: false),
                        ProductId = c.Long(nullable: false),
                        OptionId = c.Long(nullable: false),
                        OptionValueId = c.Long(nullable: false),
                        Quantity = c.Int(nullable: false),
                        Subtract = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OptionValueDescription",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        LanguageId = c.Long(nullable: false),
                        OptionId = c.Long(nullable: false),
                        OptionValueId = c.Long(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OptionValue",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        OptionId = c.Long(nullable: false),
                        ImageUrl = c.String(),
                        ImageThumb = c.String(),
                        SortOrder = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OptionDescription",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        OptionId = c.Long(nullable: false),
                        Name = c.String(),
                        LanguageId = c.Long(nullable: false),
                        DateAdded = c.DateTime(),
                        DateModefied = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Option",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        SortOrder = c.Int(nullable: false),
                        DateAdded = c.DateTime(),
                        DateModefied = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProductOption",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        ProductId = c.Long(nullable: false),
                        OptionId = c.Long(nullable: false),
                        IsRequired = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            DropForeignKey("dbo.Product", "CategoryId", "dbo.Category");
            DropIndex("dbo.Product", new[] { "CategoryId" });
            DropColumn("dbo.Product", "CategoryId");
            CreateIndex("dbo.ProductCategory", "Category_Id");
            CreateIndex("dbo.ProductCategory", "Product_Id");
            CreateIndex("dbo.ProductOptionValue", "OptionValueId");
            CreateIndex("dbo.ProductOptionValue", "OptionId");
            CreateIndex("dbo.ProductOptionValue", "ProductId");
            CreateIndex("dbo.ProductOptionValue", "ProductOptionId");
            CreateIndex("dbo.OptionValueDescription", "OptionValueId");
            CreateIndex("dbo.OptionValueDescription", "OptionId");
            CreateIndex("dbo.OptionValueDescription", "LanguageId");
            CreateIndex("dbo.OptionValue", "OptionId");
            CreateIndex("dbo.OptionDescription", "LanguageId");
            CreateIndex("dbo.OptionDescription", "OptionId");
            CreateIndex("dbo.ProductOption", "OptionId");
            CreateIndex("dbo.ProductOption", "ProductId");
            AddForeignKey("dbo.ProductOption", "ProductId", "dbo.Product", "Id");
            AddForeignKey("dbo.ProductOption", "OptionId", "dbo.Option", "Id");
            AddForeignKey("dbo.ProductOptionValue", "ProductOptionId", "dbo.ProductOption", "Id");
            AddForeignKey("dbo.ProductOptionValue", "ProductId", "dbo.Product", "Id");
            AddForeignKey("dbo.ProductOptionValue", "OptionValueId", "dbo.OptionValue", "Id");
            AddForeignKey("dbo.ProductOptionValue", "OptionId", "dbo.Option", "Id");
            AddForeignKey("dbo.OptionValueDescription", "OptionValueId", "dbo.OptionValue", "Id");
            AddForeignKey("dbo.OptionValueDescription", "OptionId", "dbo.Option", "Id");
            AddForeignKey("dbo.OptionValueDescription", "LanguageId", "dbo.Language", "Id");
            AddForeignKey("dbo.OptionValue", "OptionId", "dbo.Option", "Id");
            AddForeignKey("dbo.OptionDescription", "OptionId", "dbo.Option", "Id");
            AddForeignKey("dbo.OptionDescription", "LanguageId", "dbo.Language", "Id");
            AddForeignKey("dbo.ProductCategory", "Category_Id", "dbo.Category", "Id");
            AddForeignKey("dbo.ProductCategory", "Product_Id", "dbo.Product", "Id");
        }
    }
}
