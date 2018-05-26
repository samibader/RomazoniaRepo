namespace EShop.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class builddatabase : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CategoryDescription", "CategoryId", "dbo.Category");
            DropForeignKey("dbo.CategoryDescription", "LanguageId", "dbo.Language");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Category", new[] { "ParentCategory_Id" });
            DropColumn("dbo.Category", "ParentId");
            RenameColumn(table: "dbo.Category", name: "ParentCategory_Id", newName: "ParentId");
            CreateTable(
                "dbo.Attrib",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        SortOrder = c.Int(nullable: false),
                        DateAdded = c.DateTime(),
                        DateModefied = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AttributeDescription",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        LanguageId = c.Long(nullable: false),
                        AttributeId = c.Long(nullable: false),
                        Text = c.String(),
                        DateAdded = c.DateTime(),
                        DateModefied = c.DateTime(),
                        Attrib_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Attrib", t => t.Attrib_Id)
                .ForeignKey("dbo.Language", t => t.LanguageId)
                .Index(t => t.LanguageId)
                .Index(t => t.Attrib_Id);
            
            CreateTable(
                "dbo.Product",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        Price = c.Double(nullable: false),
                        Quantity = c.Long(nullable: false),
                        DesignerId = c.Long(nullable: false),
                        SortOrder = c.Int(nullable: false),
                        Minimum = c.Int(nullable: false),
                        Status = c.Boolean(nullable: false),
                        NumberOfViews = c.Long(nullable: false),
                        DateAdded = c.DateTime(nullable: false),
                        DateModefied = c.DateTime(nullable: false),
                        DateAvailables = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Designer", t => t.DesignerId)
                .Index(t => t.DesignerId);
            
            CreateTable(
                "dbo.Designer",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        ImageUrl = c.String(),
                        ImageThumb = c.String(),
                        DateAdded = c.DateTime(),
                        DateModefied = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Discount",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        DateStart = c.DateTime(),
                        DateEnd = c.DateTime(),
                        DateAdded = c.DateTime(),
                        DateModefied = c.DateTime(),
                        Value = c.Int(nullable: false),
                        IsPercentage = c.Boolean(nullable: false),
                        ProductId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Product", t => t.ProductId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.Image",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        ImageUrl = c.String(),
                        ThumbUrl = c.String(),
                        AlternateText = c.String(),
                        ProductId = c.Long(nullable: false),
                        DesignerId = c.Long(nullable: false),
                        CategoryId = c.Long(nullable: false),
                        OptionValueId = c.Long(nullable: false),
                        DateAdded = c.DateTime(),
                        DateModefied = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Product", t => t.ProductId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.ProductAttributeValue",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        LanguageId = c.Long(nullable: false),
                        ProductId = c.Long(nullable: false),
                        AttributeId = c.Long(nullable: false),
                        Text = c.String(),
                        Attrib_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Attrib", t => t.Attrib_Id)
                .ForeignKey("dbo.Language", t => t.LanguageId)
                .ForeignKey("dbo.Product", t => t.ProductId)
                .Index(t => t.LanguageId)
                .Index(t => t.ProductId)
                .Index(t => t.Attrib_Id);
            
            CreateTable(
                "dbo.ProductDescription",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        LanguageId = c.Long(nullable: false),
                        ProductId = c.Long(nullable: false),
                        Text = c.String(),
                        MetaDescriptions = c.String(),
                        MetaKeywords = c.String(),
                        DateAdded = c.DateTime(nullable: false),
                        DateModefied = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Language", t => t.LanguageId)
                .ForeignKey("dbo.Product", t => t.ProductId)
                .Index(t => t.LanguageId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.ProductOption",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        ProductId = c.Long(nullable: false),
                        OptionId = c.Long(nullable: false),
                        IsRequired = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Option", t => t.OptionId)
                .ForeignKey("dbo.Product", t => t.ProductId)
                .Index(t => t.ProductId)
                .Index(t => t.OptionId);
            
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
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Language", t => t.LanguageId)
                .ForeignKey("dbo.Option", t => t.OptionId)
                .Index(t => t.OptionId)
                .Index(t => t.LanguageId);
            
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
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Option", t => t.OptionId)
                .Index(t => t.OptionId);
            
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
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Language", t => t.LanguageId)
                .ForeignKey("dbo.Option", t => t.OptionId)
                .ForeignKey("dbo.OptionValue", t => t.OptionValueId)
                .Index(t => t.LanguageId)
                .Index(t => t.OptionId)
                .Index(t => t.OptionValueId);
            
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
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Option", t => t.OptionId)
                .ForeignKey("dbo.OptionValue", t => t.OptionValueId)
                .ForeignKey("dbo.Product", t => t.ProductId)
                .ForeignKey("dbo.ProductOption", t => t.ProductOptionId)
                .Index(t => t.ProductOptionId)
                .Index(t => t.ProductId)
                .Index(t => t.OptionId)
                .Index(t => t.OptionValueId);
            
            CreateTable(
                "dbo.ProductRate",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        ProductId = c.Long(nullable: false),
                        Value = c.Byte(nullable: false),
                        Note = c.String(),
                        Status = c.Boolean(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        DateAdded = c.DateTime(nullable: false),
                        DateModefied = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Product", t => t.ProductId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.Tag",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        DateAdded = c.DateTime(nullable: false),
                        DateModefied = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TagDescription",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Text = c.String(),
                        LanguageId = c.Long(nullable: false),
                        TagId = c.Long(nullable: false),
                        DateAdded = c.DateTime(nullable: false),
                        DateModefied = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Language", t => t.LanguageId)
                .ForeignKey("dbo.Tag", t => t.TagId)
                .Index(t => t.LanguageId)
                .Index(t => t.TagId);
            
            CreateTable(
                "dbo.ProductCategory",
                c => new
                    {
                        Product_Id = c.Long(nullable: false),
                        Category_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.Product_Id, t.Category_Id })
                .ForeignKey("dbo.Product", t => t.Product_Id)
                .ForeignKey("dbo.Category", t => t.Category_Id)
                .Index(t => t.Product_Id)
                .Index(t => t.Category_Id);
            
            CreateTable(
                "dbo.TagProduct",
                c => new
                    {
                        Tag_Id = c.Long(nullable: false),
                        Product_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.Tag_Id, t.Product_Id })
                .ForeignKey("dbo.Tag", t => t.Tag_Id)
                .ForeignKey("dbo.Product", t => t.Product_Id)
                .Index(t => t.Tag_Id)
                .Index(t => t.Product_Id);
            
            AddColumn("dbo.Category", "ImageUrl", c => c.String());
            AddColumn("dbo.Category", "ImageThumb", c => c.String());
            AddColumn("dbo.Category", "Status", c => c.Boolean(nullable: false));
            AddColumn("dbo.CategoryDescription", "DateAdded", c => c.DateTime());
            AddColumn("dbo.CategoryDescription", "DateModefied", c => c.DateTime());
            AddColumn("dbo.Language", "ImageUrl", c => c.String());
            AddColumn("dbo.Language", "ImageThumb", c => c.String());
            AddColumn("dbo.Language", "Name", c => c.String());
            AddColumn("dbo.Language", "Status", c => c.Int(nullable: false));
            AddColumn("dbo.Language", "DateAdded", c => c.DateTime());
            AddColumn("dbo.Language", "DateModefied", c => c.DateTime());
            AlterColumn("dbo.Category", "ParentId", c => c.Long());
            AlterColumn("dbo.CategoryDescription", "Name", c => c.String());
            AlterColumn("dbo.CategoryDescription", "Description", c => c.String());
            AlterColumn("dbo.Language", "Code", c => c.String());
            CreateIndex("dbo.Category", "ParentId");
            AddForeignKey("dbo.CategoryDescription", "CategoryId", "dbo.Category", "Id");
            AddForeignKey("dbo.CategoryDescription", "LanguageId", "dbo.Language", "Id");
            AddForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles", "Id");
            AddForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers", "Id");
            DropColumn("dbo.Category", "Image");
            DropColumn("dbo.Category", "IsActive");
            DropColumn("dbo.Language", "ArabicName");
            DropColumn("dbo.Language", "EnglishName");
            DropColumn("dbo.Language", "Image");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Language", "Image", c => c.String(nullable: false, maxLength: 200));
            AddColumn("dbo.Language", "EnglishName", c => c.String(nullable: false, maxLength: 50));
            AddColumn("dbo.Language", "ArabicName", c => c.String(nullable: false, maxLength: 50));
            AddColumn("dbo.Category", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.Category", "Image", c => c.String(nullable: false, maxLength: 200));
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.CategoryDescription", "LanguageId", "dbo.Language");
            DropForeignKey("dbo.CategoryDescription", "CategoryId", "dbo.Category");
            DropForeignKey("dbo.TagDescription", "TagId", "dbo.Tag");
            DropForeignKey("dbo.TagDescription", "LanguageId", "dbo.Language");
            DropForeignKey("dbo.TagProduct", "Product_Id", "dbo.Product");
            DropForeignKey("dbo.TagProduct", "Tag_Id", "dbo.Tag");
            DropForeignKey("dbo.ProductRate", "ProductId", "dbo.Product");
            DropForeignKey("dbo.ProductOption", "ProductId", "dbo.Product");
            DropForeignKey("dbo.ProductOption", "OptionId", "dbo.Option");
            DropForeignKey("dbo.ProductOptionValue", "ProductOptionId", "dbo.ProductOption");
            DropForeignKey("dbo.ProductOptionValue", "ProductId", "dbo.Product");
            DropForeignKey("dbo.ProductOptionValue", "OptionValueId", "dbo.OptionValue");
            DropForeignKey("dbo.ProductOptionValue", "OptionId", "dbo.Option");
            DropForeignKey("dbo.OptionValueDescription", "OptionValueId", "dbo.OptionValue");
            DropForeignKey("dbo.OptionValueDescription", "OptionId", "dbo.Option");
            DropForeignKey("dbo.OptionValueDescription", "LanguageId", "dbo.Language");
            DropForeignKey("dbo.OptionValue", "OptionId", "dbo.Option");
            DropForeignKey("dbo.OptionDescription", "OptionId", "dbo.Option");
            DropForeignKey("dbo.OptionDescription", "LanguageId", "dbo.Language");
            DropForeignKey("dbo.ProductDescription", "ProductId", "dbo.Product");
            DropForeignKey("dbo.ProductDescription", "LanguageId", "dbo.Language");
            DropForeignKey("dbo.ProductAttributeValue", "ProductId", "dbo.Product");
            DropForeignKey("dbo.ProductAttributeValue", "LanguageId", "dbo.Language");
            DropForeignKey("dbo.ProductAttributeValue", "Attrib_Id", "dbo.Attrib");
            DropForeignKey("dbo.Image", "ProductId", "dbo.Product");
            DropForeignKey("dbo.Discount", "ProductId", "dbo.Product");
            DropForeignKey("dbo.Product", "DesignerId", "dbo.Designer");
            DropForeignKey("dbo.ProductCategory", "Category_Id", "dbo.Category");
            DropForeignKey("dbo.ProductCategory", "Product_Id", "dbo.Product");
            DropForeignKey("dbo.AttributeDescription", "LanguageId", "dbo.Language");
            DropForeignKey("dbo.AttributeDescription", "Attrib_Id", "dbo.Attrib");
            DropIndex("dbo.TagProduct", new[] { "Product_Id" });
            DropIndex("dbo.TagProduct", new[] { "Tag_Id" });
            DropIndex("dbo.ProductCategory", new[] { "Category_Id" });
            DropIndex("dbo.ProductCategory", new[] { "Product_Id" });
            DropIndex("dbo.TagDescription", new[] { "TagId" });
            DropIndex("dbo.TagDescription", new[] { "LanguageId" });
            DropIndex("dbo.ProductRate", new[] { "ProductId" });
            DropIndex("dbo.ProductOptionValue", new[] { "OptionValueId" });
            DropIndex("dbo.ProductOptionValue", new[] { "OptionId" });
            DropIndex("dbo.ProductOptionValue", new[] { "ProductId" });
            DropIndex("dbo.ProductOptionValue", new[] { "ProductOptionId" });
            DropIndex("dbo.OptionValueDescription", new[] { "OptionValueId" });
            DropIndex("dbo.OptionValueDescription", new[] { "OptionId" });
            DropIndex("dbo.OptionValueDescription", new[] { "LanguageId" });
            DropIndex("dbo.OptionValue", new[] { "OptionId" });
            DropIndex("dbo.OptionDescription", new[] { "LanguageId" });
            DropIndex("dbo.OptionDescription", new[] { "OptionId" });
            DropIndex("dbo.ProductOption", new[] { "OptionId" });
            DropIndex("dbo.ProductOption", new[] { "ProductId" });
            DropIndex("dbo.ProductDescription", new[] { "ProductId" });
            DropIndex("dbo.ProductDescription", new[] { "LanguageId" });
            DropIndex("dbo.ProductAttributeValue", new[] { "Attrib_Id" });
            DropIndex("dbo.ProductAttributeValue", new[] { "ProductId" });
            DropIndex("dbo.ProductAttributeValue", new[] { "LanguageId" });
            DropIndex("dbo.Image", new[] { "ProductId" });
            DropIndex("dbo.Discount", new[] { "ProductId" });
            DropIndex("dbo.Product", new[] { "DesignerId" });
            DropIndex("dbo.Category", new[] { "ParentId" });
            DropIndex("dbo.AttributeDescription", new[] { "Attrib_Id" });
            DropIndex("dbo.AttributeDescription", new[] { "LanguageId" });
            AlterColumn("dbo.Language", "Code", c => c.String(maxLength: 5));
            AlterColumn("dbo.CategoryDescription", "Description", c => c.String(nullable: false));
            AlterColumn("dbo.CategoryDescription", "Name", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Category", "ParentId", c => c.Long(nullable: false));
            DropColumn("dbo.Language", "DateModefied");
            DropColumn("dbo.Language", "DateAdded");
            DropColumn("dbo.Language", "Status");
            DropColumn("dbo.Language", "Name");
            DropColumn("dbo.Language", "ImageThumb");
            DropColumn("dbo.Language", "ImageUrl");
            DropColumn("dbo.CategoryDescription", "DateModefied");
            DropColumn("dbo.CategoryDescription", "DateAdded");
            DropColumn("dbo.Category", "Status");
            DropColumn("dbo.Category", "ImageThumb");
            DropColumn("dbo.Category", "ImageUrl");
            DropTable("dbo.TagProduct");
            DropTable("dbo.ProductCategory");
            DropTable("dbo.TagDescription");
            DropTable("dbo.Tag");
            DropTable("dbo.ProductRate");
            DropTable("dbo.ProductOptionValue");
            DropTable("dbo.OptionValueDescription");
            DropTable("dbo.OptionValue");
            DropTable("dbo.OptionDescription");
            DropTable("dbo.Option");
            DropTable("dbo.ProductOption");
            DropTable("dbo.ProductDescription");
            DropTable("dbo.ProductAttributeValue");
            DropTable("dbo.Image");
            DropTable("dbo.Discount");
            DropTable("dbo.Designer");
            DropTable("dbo.Product");
            DropTable("dbo.AttributeDescription");
            DropTable("dbo.Attrib");
            RenameColumn(table: "dbo.Category", name: "ParentId", newName: "ParentCategory_Id");
            AddColumn("dbo.Category", "ParentId", c => c.Long(nullable: false));
            CreateIndex("dbo.Category", "ParentCategory_Id");
            AddForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles", "Id", cascadeDelete: true);
            AddForeignKey("dbo.CategoryDescription", "LanguageId", "dbo.Language", "Id", cascadeDelete: true);
            AddForeignKey("dbo.CategoryDescription", "CategoryId", "dbo.Category", "Id", cascadeDelete: true);
        }
    }
}
