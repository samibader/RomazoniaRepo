namespace EShop.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Adding_Designer_Description : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DesignerDescription",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        LanguageId = c.Long(nullable: false),
                        DesignerId = c.Long(nullable: false),
                        Text = c.String(),
                        DateAdded = c.DateTime(),
                        DateModefied = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Designer", t => t.DesignerId)
                .ForeignKey("dbo.Language", t => t.LanguageId)
                .Index(t => t.LanguageId)
                .Index(t => t.DesignerId);
            
          
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DesignerDescription", "LanguageId", "dbo.Language");
            DropForeignKey("dbo.DesignerDescription", "DesignerId", "dbo.Designer");
            DropIndex("dbo.DesignerDescription", new[] { "DesignerId" });
            DropIndex("dbo.DesignerDescription", new[] { "LanguageId" });
            DropTable("dbo.DesignerDescription");
        }
    }
}
