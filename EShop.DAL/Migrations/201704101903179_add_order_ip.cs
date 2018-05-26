namespace EShop.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_order_ip : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Order", "UserIpAddress", c => c.String());
            AddColumn("dbo.Order", "UserAgent", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Order", "UserAgent");
            DropColumn("dbo.Order", "UserIpAddress");
        }
    }
}
