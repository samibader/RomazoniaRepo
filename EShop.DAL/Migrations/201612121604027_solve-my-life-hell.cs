namespace EShop.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class solvemylifehell : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Order", "CouponValue", c => c.String());
            AddColumn("dbo.Order", "ArabicShippingName", c => c.String());
            AddColumn("dbo.Order", "EnglishShippingName", c => c.String());
            AddColumn("dbo.Order", "ArabicBillingName", c => c.String());
            AddColumn("dbo.Order", "EnglishBillingName", c => c.String());
            AlterColumn("dbo.Order", "Discount", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Order", "Discount", c => c.Double(nullable: false));
            DropColumn("dbo.Order", "EnglishBillingName");
            DropColumn("dbo.Order", "ArabicBillingName");
            DropColumn("dbo.Order", "EnglishShippingName");
            DropColumn("dbo.Order", "ArabicShippingName");
            DropColumn("dbo.Order", "CouponValue");
        }
    }
}
