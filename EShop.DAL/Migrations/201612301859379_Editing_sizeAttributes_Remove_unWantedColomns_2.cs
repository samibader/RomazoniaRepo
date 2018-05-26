namespace EShop.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Editing_sizeAttributes_Remove_unWantedColomns_2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SizeAttributeOptionHelper", "SizeAttribute_Id", "dbo.SizeAttribute");
            DropForeignKey("dbo.SizeAttributeOptionHelper", "OptionHelper_Id", "dbo.OptionHelper");
            DropIndex("dbo.SizeAttributeOptionHelper", new[] { "SizeAttribute_Id" });
            DropIndex("dbo.SizeAttributeOptionHelper", new[] { "OptionHelper_Id" });
            AddColumn("dbo.SizeAttribute", "SizeCatId", c => c.Long(nullable: false));
            AddColumn("dbo.SizeAttribute", "SizeCategory_Id", c => c.Long());
            CreateIndex("dbo.SizeAttribute", "SizeCategory_Id");
            AddForeignKey("dbo.SizeAttribute", "SizeCategory_Id", "dbo.OptionHelper", "Id");
            DropColumn("dbo.ProductSKU", "IsNew");
            DropTable("dbo.SizeAttributeOptionHelper");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.SizeAttributeOptionHelper",
                c => new
                    {
                        SizeAttribute_Id = c.Long(nullable: false),
                        OptionHelper_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.SizeAttribute_Id, t.OptionHelper_Id });
            
            AddColumn("dbo.ProductSKU", "IsNew", c => c.Boolean(nullable: false));
            DropForeignKey("dbo.SizeAttribute", "SizeCategory_Id", "dbo.OptionHelper");
            DropIndex("dbo.SizeAttribute", new[] { "SizeCategory_Id" });
            DropColumn("dbo.SizeAttribute", "SizeCategory_Id");
            DropColumn("dbo.SizeAttribute", "SizeCatId");
            CreateIndex("dbo.SizeAttributeOptionHelper", "OptionHelper_Id");
            CreateIndex("dbo.SizeAttributeOptionHelper", "SizeAttribute_Id");
            AddForeignKey("dbo.SizeAttributeOptionHelper", "OptionHelper_Id", "dbo.OptionHelper", "Id");
            AddForeignKey("dbo.SizeAttributeOptionHelper", "SizeAttribute_Id", "dbo.SizeAttribute", "Id");
        }
    }
}
