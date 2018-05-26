namespace EShop.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class alterOrder : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Order", "CouponValue", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Order", "CouponValue", c => c.Double());
        }
    }
}
