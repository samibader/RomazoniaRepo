namespace EShop.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_ProductSizeAttributes : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProductSizeAttribute",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        ProductId = c.Long(nullable: false),
                        SizeAttributeId = c.Long(nullable: false),
                        SizeCategoryId = c.Long(nullable: false),
                        ValueId = c.Long(nullable: false),
                        Value = c.Double(nullable: false),
                        DateAdded = c.DateTime(),
                        DateModified = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Product", t => t.ProductId)
                .Index(t => t.ProductId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductSizeAttribute", "ProductId", "dbo.Product");
            DropIndex("dbo.ProductSizeAttribute", new[] { "ProductId" });
            DropTable("dbo.ProductSizeAttribute");
        }
    }
}
