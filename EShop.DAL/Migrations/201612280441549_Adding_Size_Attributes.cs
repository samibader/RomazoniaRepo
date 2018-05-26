namespace EShop.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Adding_Size_Attributes : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SizeAttributeDescription",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        SizeAttributeId = c.Long(nullable: false),
                        DateAdded = c.DateTime(),
                        DateModefied = c.DateTime(),
                        LanguageId = c.Long(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Language", t => t.LanguageId)
                .ForeignKey("dbo.SizeAttribute", t => t.SizeAttributeId)
                .Index(t => t.SizeAttributeId)
                .Index(t => t.LanguageId);
            
            CreateTable(
                "dbo.SizeAttribute",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        DateAdded = c.DateTime(),
                        DateModefied = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SizeAttributeDescription", "SizeAttributeId", "dbo.SizeAttribute");
            DropForeignKey("dbo.SizeAttributeDescription", "LanguageId", "dbo.Language");
            DropIndex("dbo.SizeAttributeDescription", new[] { "LanguageId" });
            DropIndex("dbo.SizeAttributeDescription", new[] { "SizeAttributeId" });
            DropTable("dbo.SizeAttributeMedian");
            DropTable("dbo.SizeAttribute");
            DropTable("dbo.SizeAttributeDescription");
        }
    }
}
