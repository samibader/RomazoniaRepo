namespace EShop.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Editing_sizeAttributes_Remove_unWantedColomns : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SizeAttributeOptionHelper",
                c => new
                    {
                        SizeAttribute_Id = c.Long(nullable: false),
                        OptionHelper_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.SizeAttribute_Id, t.OptionHelper_Id })
                .ForeignKey("dbo.SizeAttribute", t => t.SizeAttribute_Id)
                .ForeignKey("dbo.OptionHelper", t => t.OptionHelper_Id)
                .Index(t => t.SizeAttribute_Id)
                .Index(t => t.OptionHelper_Id);
            
            DropColumn("dbo.Product", "Name");
            DropColumn("dbo.Product", "Quantity");
            DropColumn("dbo.Product", "SortOrder");
            DropColumn("dbo.Product", "DateAvailables");
            DropTable("dbo.SizeAttributeMedian");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.SizeAttributeMedian",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        SizeAttributeId = c.Long(nullable: false),
                        SizeValueId = c.Long(nullable: false),
                        Value = c.Int(nullable: false),
                        DateAdded = c.DateTime(),
                        DateModefied = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Product", "DateAvailables", c => c.DateTime());
            AddColumn("dbo.Product", "SortOrder", c => c.Int(nullable: false));
            AddColumn("dbo.Product", "Quantity", c => c.Long(nullable: false));
            AddColumn("dbo.Product", "Name", c => c.String());
            DropForeignKey("dbo.SizeAttributeOptionHelper", "OptionHelper_Id", "dbo.OptionHelper");
            DropForeignKey("dbo.SizeAttributeOptionHelper", "SizeAttribute_Id", "dbo.SizeAttribute");
            DropIndex("dbo.SizeAttributeOptionHelper", new[] { "OptionHelper_Id" });
            DropIndex("dbo.SizeAttributeOptionHelper", new[] { "SizeAttribute_Id" });
            DropTable("dbo.SizeAttributeOptionHelper");
        }
    }
}
