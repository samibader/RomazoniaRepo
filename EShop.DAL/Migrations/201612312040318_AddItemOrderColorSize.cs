namespace EShop.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddItemOrderColorSize : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderItem", "ColorArabic", c => c.String());
            AddColumn("dbo.OrderItem", "ColorEnglish", c => c.String());
            AddColumn("dbo.OrderItem", "SizeArabic", c => c.String());
            AddColumn("dbo.OrderItem", "SizeEnglish", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.OrderItem", "SizeEnglish");
            DropColumn("dbo.OrderItem", "SizeArabic");
            DropColumn("dbo.OrderItem", "ColorEnglish");
            DropColumn("dbo.OrderItem", "ColorArabic");
        }
    }
}
