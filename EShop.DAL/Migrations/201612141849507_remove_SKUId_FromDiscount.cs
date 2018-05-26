namespace EShop.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class remove_SKUId_FromDiscount : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Discount", new[] { "ProductSKU_ProductId", "ProductSKU_SKUId" });
            RenameColumn(table: "dbo.Discount", name: "ProductSKU_ProductId", newName: "ProductId");
            RenameColumn(table: "dbo.Discount", name: "ProductSKU_SKUId", newName: "SKUId");
            AlterColumn("dbo.Discount", "ProductId", c => c.Long(nullable: false));
            AlterColumn("dbo.Discount", "SKUId", c => c.Long(nullable: false));
            CreateIndex("dbo.Discount", new[] { "ProductId", "SKUId" });
            DropColumn("dbo.Discount", "ProductSKUId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Discount", "ProductSKUId", c => c.Long(nullable: false));
            DropIndex("dbo.Discount", new[] { "ProductId", "SKUId" });
            AlterColumn("dbo.Discount", "SKUId", c => c.Long());
            AlterColumn("dbo.Discount", "ProductId", c => c.Long());
            RenameColumn(table: "dbo.Discount", name: "SKUId", newName: "ProductSKU_SKUId");
            RenameColumn(table: "dbo.Discount", name: "ProductId", newName: "ProductSKU_ProductId");
            CreateIndex("dbo.Discount", new[] { "ProductSKU_ProductId", "ProductSKU_SKUId" });
        }
    }
}
