namespace EShop.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class solve_order_prop : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.ShippingAddress", "OrderId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ShippingAddress", "OrderId", c => c.Long(nullable: false));
        }
    }
}
