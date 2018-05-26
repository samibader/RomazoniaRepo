namespace EShop.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addIsPercentageToOrder : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Order", "CouponIsPercentage", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Order", "CouponValue", c => c.Double());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Order", "CouponValue", c => c.String());
            DropColumn("dbo.Order", "CouponIsPercentage");
        }
    }
}
