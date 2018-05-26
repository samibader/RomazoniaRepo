namespace EShop.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Favorite : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Favorite",
                c => new
                    {
                        ProductId = c.Long(nullable: false),
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .ForeignKey("dbo.Product", t => t.ProductId)
                .Index(t => t.ProductId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Favorite", "ProductId", "dbo.Product");
            DropForeignKey("dbo.Favorite", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Favorite", new[] { "UserId" });
            DropIndex("dbo.Favorite", new[] { "ProductId" });
            DropTable("dbo.Favorite");
        }
    }
}
