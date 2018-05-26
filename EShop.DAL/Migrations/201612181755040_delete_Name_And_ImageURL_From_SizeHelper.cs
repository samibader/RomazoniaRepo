namespace EShop.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class delete_Name_And_ImageURL_From_SizeHelper : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProductRate", "UserId", c => c.String());
            AddColumn("dbo.ProductRate", "PriceRate", c => c.Int(nullable: false));
            AddColumn("dbo.ProductRate", "QualityRate", c => c.Int(nullable: false));
            AddColumn("dbo.ProductRate", "ThirdRate", c => c.Int(nullable: false));
            DropColumn("dbo.SizeHelper", "ImageUrl");
            DropColumn("dbo.SizeHelper", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SizeHelper", "Name", c => c.String());
            AddColumn("dbo.SizeHelper", "ImageUrl", c => c.String());
            DropColumn("dbo.ProductRate", "ThirdRate");
            DropColumn("dbo.ProductRate", "QualityRate");
            DropColumn("dbo.ProductRate", "PriceRate");
            DropColumn("dbo.ProductRate", "UserId");
        }
    }
}
