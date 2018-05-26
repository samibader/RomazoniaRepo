namespace EShop.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_shippingAndBilling : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ShippingBillingMethodDescription",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        LanguageId = c.Long(nullable: false),
                        ShippingBillingMethodId = c.Long(nullable: false),
                        Name = c.String(),
                        MetaDescription = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Language", t => t.LanguageId)
                .ForeignKey("dbo.ShippingBillingMethod", t => t.ShippingBillingMethodId)
                .Index(t => t.LanguageId)
                .Index(t => t.ShippingBillingMethodId);
            
            CreateTable(
                "dbo.ShippingBillingMethod",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        IsShipping = c.Boolean(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ShippingCompanyDescription",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        LanguageId = c.Long(nullable: false),
                        ShippingCompanyId = c.Long(nullable: false),
                        Name = c.String(),
                        MetaDescription = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Language", t => t.LanguageId)
                .ForeignKey("dbo.ShippingCompany", t => t.ShippingCompanyId)
                .Index(t => t.LanguageId)
                .Index(t => t.ShippingCompanyId);
            
            AddColumn("dbo.ShippingCompany", "IsActive", c => c.Boolean(nullable: false));
            DropColumn("dbo.ShippingCompany", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ShippingCompany", "Name", c => c.String());
            DropForeignKey("dbo.ShippingCompanyDescription", "ShippingCompanyId", "dbo.ShippingCompany");
            DropForeignKey("dbo.ShippingCompanyDescription", "LanguageId", "dbo.Language");
            DropForeignKey("dbo.ShippingBillingMethodDescription", "ShippingBillingMethodId", "dbo.ShippingBillingMethod");
            DropForeignKey("dbo.ShippingBillingMethodDescription", "LanguageId", "dbo.Language");
            DropIndex("dbo.ShippingCompanyDescription", new[] { "ShippingCompanyId" });
            DropIndex("dbo.ShippingCompanyDescription", new[] { "LanguageId" });
            DropIndex("dbo.ShippingBillingMethodDescription", new[] { "ShippingBillingMethodId" });
            DropIndex("dbo.ShippingBillingMethodDescription", new[] { "LanguageId" });
            DropColumn("dbo.ShippingCompany", "IsActive");
            DropTable("dbo.ShippingCompanyDescription");
            DropTable("dbo.ShippingBillingMethod");
            DropTable("dbo.ShippingBillingMethodDescription");
        }
    }
}
