namespace EShop.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modifyOrderStatus : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Order", "Status", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Order", "Status", c => c.String());
        }
    }
}
