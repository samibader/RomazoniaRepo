namespace EShop.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class whatishappining : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderItem", "SKU", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.OrderItem", "SKU");
        }
    }
}
