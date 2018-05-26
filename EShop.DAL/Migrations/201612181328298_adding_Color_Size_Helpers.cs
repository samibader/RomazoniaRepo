namespace EShop.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adding_Color_Size_Helpers : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ColorHelper",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        ArabicName = c.String(),
                        EngishName = c.String(),
                        ImageUrl = c.String(),
                        Value = c.String(),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SizeHelper",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        ArabicSizeCategoryName = c.String(),
                        EnglishSizeCategoryName = c.String(),
                        ArabicName = c.String(),
                        EngishName = c.String(),
                        ImageUrl = c.String(),
                        Value = c.String(),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SizeHelper");
            DropTable("dbo.ColorHelper");
        }
    }
}
