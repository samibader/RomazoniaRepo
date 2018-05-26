namespace EShop.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial1 : DbMigration
    {
        public override void Up()
        {
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
                "dbo.Language",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        ImageUrl = c.String(),
                        ImageThumb = c.String(),
                        Name = c.String(),
                        Status = c.Int(nullable: false),
                        Code = c.String(),
                        SortOrder = c.Int(nullable: false),
                        DateAdded = c.DateTime(),
                        DateModefied = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CategoryDescription",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        LanguageId = c.Long(nullable: false),
                        CategoryId = c.Long(nullable: false),
                        Name = c.String(),
                        Description = c.String(),
                        MetaDescription = c.String(),
                        MetaKeyWord = c.String(),
                        DateAdded = c.DateTime(),
                        DateModefied = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Category", t => t.CategoryId)
                .ForeignKey("dbo.Language", t => t.LanguageId)
                .Index(t => t.LanguageId)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.Category",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        ParentId = c.Long(),
                        ImageUrl = c.String(),
                        ImageThumb = c.String(),
                        SortOrder = c.Int(nullable: false),
                        Status = c.Boolean(nullable: false),
                        DateAdded = c.DateTime(),
                        DateModified = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Category", t => t.ParentId)
                .Index(t => t.ParentId);
            
            CreateTable(
                "dbo.Product",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        Price = c.Double(nullable: false),
                        CategoryId = c.Long(nullable: false),
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
                .ForeignKey("dbo.Category", t => t.CategoryId)
                .ForeignKey("dbo.Designer", t => t.DesignerId)
                .Index(t => t.CategoryId)
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
                "dbo.OptionDescription",
                c => new
                    {
                        ProductId = c.Long(nullable: false),
                        OptionId = c.Long(nullable: false),
                        LanguageId = c.Long(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => new { t.ProductId, t.OptionId, t.LanguageId })
                .ForeignKey("dbo.Language", t => t.LanguageId)
                .ForeignKey("dbo.Option", t => new { t.ProductId, t.OptionId })
                .Index(t => new { t.ProductId, t.OptionId })
                .Index(t => t.LanguageId);
            
            CreateTable(
                "dbo.Option",
                c => new
                    {
                        ProductId = c.Long(nullable: false),
                        OptionId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProductId, t.OptionId });
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.OptionDescription", new[] { "ProductId", "OptionId" }, "dbo.Option");
            DropForeignKey("dbo.OptionDescription", "LanguageId", "dbo.Language");
            DropForeignKey("dbo.CategoryDescription", "LanguageId", "dbo.Language");
            DropForeignKey("dbo.TagDescription", "TagId", "dbo.Tag");
            DropForeignKey("dbo.TagDescription", "LanguageId", "dbo.Language");
            DropForeignKey("dbo.TagProduct", "Product_Id", "dbo.Product");
            DropForeignKey("dbo.TagProduct", "Tag_Id", "dbo.Tag");
            DropForeignKey("dbo.ProductRate", "ProductId", "dbo.Product");
            DropForeignKey("dbo.ProductDescription", "ProductId", "dbo.Product");
            DropForeignKey("dbo.ProductDescription", "LanguageId", "dbo.Language");
            DropForeignKey("dbo.ProductAttributeValue", "ProductId", "dbo.Product");
            DropForeignKey("dbo.ProductAttributeValue", "LanguageId", "dbo.Language");
            DropForeignKey("dbo.ProductAttributeValue", "Attrib_Id", "dbo.Attrib");
            DropForeignKey("dbo.Image", "ProductId", "dbo.Product");
            DropForeignKey("dbo.Discount", "ProductId", "dbo.Product");
            DropForeignKey("dbo.Product", "DesignerId", "dbo.Designer");
            DropForeignKey("dbo.Product", "CategoryId", "dbo.Category");
            DropForeignKey("dbo.Category", "ParentId", "dbo.Category");
            DropForeignKey("dbo.CategoryDescription", "CategoryId", "dbo.Category");
            DropForeignKey("dbo.AttributeDescription", "LanguageId", "dbo.Language");
            DropForeignKey("dbo.AttributeDescription", "Attrib_Id", "dbo.Attrib");
            DropIndex("dbo.TagProduct", new[] { "Product_Id" });
            DropIndex("dbo.TagProduct", new[] { "Tag_Id" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.OptionDescription", new[] { "LanguageId" });
            DropIndex("dbo.OptionDescription", new[] { "ProductId", "OptionId" });
            DropIndex("dbo.TagDescription", new[] { "TagId" });
            DropIndex("dbo.TagDescription", new[] { "LanguageId" });
            DropIndex("dbo.ProductRate", new[] { "ProductId" });
            DropIndex("dbo.ProductDescription", new[] { "ProductId" });
            DropIndex("dbo.ProductDescription", new[] { "LanguageId" });
            DropIndex("dbo.ProductAttributeValue", new[] { "Attrib_Id" });
            DropIndex("dbo.ProductAttributeValue", new[] { "ProductId" });
            DropIndex("dbo.ProductAttributeValue", new[] { "LanguageId" });
            DropIndex("dbo.Image", new[] { "ProductId" });
            DropIndex("dbo.Discount", new[] { "ProductId" });
            DropIndex("dbo.Product", new[] { "DesignerId" });
            DropIndex("dbo.Product", new[] { "CategoryId" });
            DropIndex("dbo.Category", new[] { "ParentId" });
            DropIndex("dbo.CategoryDescription", new[] { "CategoryId" });
            DropIndex("dbo.CategoryDescription", new[] { "LanguageId" });
            DropIndex("dbo.AttributeDescription", new[] { "Attrib_Id" });
            DropIndex("dbo.AttributeDescription", new[] { "LanguageId" });
            DropTable("dbo.TagProduct");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Option");
            DropTable("dbo.OptionDescription");
            DropTable("dbo.TagDescription");
            DropTable("dbo.Tag");
            DropTable("dbo.ProductRate");
            DropTable("dbo.ProductDescription");
            DropTable("dbo.ProductAttributeValue");
            DropTable("dbo.Image");
            DropTable("dbo.Discount");
            DropTable("dbo.Designer");
            DropTable("dbo.Product");
            DropTable("dbo.Category");
            DropTable("dbo.CategoryDescription");
            DropTable("dbo.Language");
            DropTable("dbo.AttributeDescription");
            DropTable("dbo.Attrib");
        }
    }
}
