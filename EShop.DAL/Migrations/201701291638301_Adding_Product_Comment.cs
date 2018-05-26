namespace EShop.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Adding_Product_Comment : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Product", "Comment", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Product", "Comment");
        }
    }
}
