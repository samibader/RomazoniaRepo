namespace EShop.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OptionValue",
                c => new
                    {
                        ProductId = c.Long(nullable: false),
                        OptionId = c.Long(nullable: false),
                        ValueId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProductId, t.OptionId, t.ValueId })
                .ForeignKey("dbo.Option", t => new { t.ProductId, t.OptionId })
                .Index(t => new { t.ProductId, t.OptionId });
            
            CreateTable(
                "dbo.OptionValueDescription",
                c => new
                    {
                        ProductId = c.Long(nullable: false),
                        OptionId = c.Long(nullable: false),
                        ValueId = c.Long(nullable: false),
                        LanguageId = c.Long(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => new { t.ProductId, t.OptionId, t.ValueId, t.LanguageId })
                .ForeignKey("dbo.Language", t => t.LanguageId)
                .ForeignKey("dbo.OptionValue", t => new { t.ProductId, t.OptionId, t.ValueId })
                .Index(t => new { t.ProductId, t.OptionId, t.ValueId })
                .Index(t => t.LanguageId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OptionValueDescription", new[] { "ProductId", "OptionId", "ValueId" }, "dbo.OptionValue");
            DropForeignKey("dbo.OptionValueDescription", "LanguageId", "dbo.Language");
            DropForeignKey("dbo.OptionValue", new[] { "ProductId", "OptionId" }, "dbo.Option");
            DropIndex("dbo.OptionValueDescription", new[] { "LanguageId" });
            DropIndex("dbo.OptionValueDescription", new[] { "ProductId", "OptionId", "ValueId" });
            DropIndex("dbo.OptionValue", new[] { "ProductId", "OptionId" });
            DropTable("dbo.OptionValueDescription");
            DropTable("dbo.OptionValue");
        }
    }
}
