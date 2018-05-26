namespace EShop.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class drop_order_prop : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Order", "ShippingAddressId");
            DropColumn("dbo.Order", "BillingAddressId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Order", "BillingAddressId", c => c.Long(nullable: false));
            AddColumn("dbo.Order", "ShippingAddressId", c => c.Long(nullable: false));
        }
    }
}
