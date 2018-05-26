namespace EShop.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adding_Attribute_Group : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AttributeGroup",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        SortOrder = c.Int(nullable: false),
                        DateAdded = c.DateTime(),
                        DateModefied = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AttributeGroupDescription",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        LanguageId = c.Long(nullable: false),
                        GroupId = c.Long(nullable: false),
                        Text = c.String(),
                        DateAdded = c.DateTime(),
                        DateModefied = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AttributeGroup", t => t.GroupId)
                .ForeignKey("dbo.Language", t => t.LanguageId)
                .Index(t => t.LanguageId)
                .Index(t => t.GroupId);
            
            AddColumn("dbo.Attrib", "GroupId", c => c.Long(nullable: false));
            CreateIndex("dbo.Attrib", "GroupId");
            AddForeignKey("dbo.Attrib", "GroupId", "dbo.AttributeGroup", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Attrib", "GroupId", "dbo.AttributeGroup");
            DropForeignKey("dbo.AttributeGroupDescription", "LanguageId", "dbo.Language");
            DropForeignKey("dbo.AttributeGroupDescription", "GroupId", "dbo.AttributeGroup");
            DropIndex("dbo.AttributeGroupDescription", new[] { "GroupId" });
            DropIndex("dbo.AttributeGroupDescription", new[] { "LanguageId" });
            DropIndex("dbo.Attrib", new[] { "GroupId" });
            DropColumn("dbo.Attrib", "GroupId");
            DropTable("dbo.AttributeGroupDescription");
            DropTable("dbo.AttributeGroup");
        }
    }
}
