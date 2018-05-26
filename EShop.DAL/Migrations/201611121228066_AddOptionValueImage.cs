namespace EShop.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddOptionValueImage : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Image", new[] { "ProductId" });
            AddColumn("dbo.Image", "OptionValueProductId", c => c.Long());
            AddColumn("dbo.Image", "OptionValueOptionId", c => c.Long());
            AddColumn("dbo.Image", "OptionValueValueId", c => c.Long());
            AlterColumn("dbo.Image", "ProductId", c => c.Long());
            AlterColumn("dbo.Image", "DesignerId", c => c.Long());
            AlterColumn("dbo.Image", "CategoryId", c => c.Long());
            CreateIndex("dbo.Image", new[] { "OptionValueProductId", "OptionValueOptionId", "OptionValueValueId" });
            CreateIndex("dbo.Image", "ProductId");
            AddForeignKey("dbo.Image", new[] { "OptionValueProductId", "OptionValueOptionId", "OptionValueValueId" }, "dbo.OptionValue", new[] { "ProductId", "OptionId", "ValueId" });
            DropColumn("dbo.Image", "OptionValueId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Image", "OptionValueId", c => c.Long(nullable: false));
            DropForeignKey("dbo.Image", new[] { "OptionValueProductId", "OptionValueOptionId", "OptionValueValueId" }, "dbo.OptionValue");
            DropIndex("dbo.Image", new[] { "ProductId" });
            DropIndex("dbo.Image", new[] { "OptionValueProductId", "OptionValueOptionId", "OptionValueValueId" });
            AlterColumn("dbo.Image", "CategoryId", c => c.Long(nullable: false));
            AlterColumn("dbo.Image", "DesignerId", c => c.Long(nullable: false));
            AlterColumn("dbo.Image", "ProductId", c => c.Long(nullable: false));
            DropColumn("dbo.Image", "OptionValueValueId");
            DropColumn("dbo.Image", "OptionValueOptionId");
            DropColumn("dbo.Image", "OptionValueProductId");
            CreateIndex("dbo.Image", "ProductId");
        }
    }
}
