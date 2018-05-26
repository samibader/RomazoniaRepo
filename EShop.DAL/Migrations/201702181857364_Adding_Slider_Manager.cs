namespace EShop.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Adding_Slider_Manager : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Slide",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        ImageUrl = c.String(),
                        Url = c.String(),
                        WebSite = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Slide");
        }
    }
}
