namespace EShop.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addNameToProductDescription : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProductDescription", "Name", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProductDescription", "Name");
        }
    }
}
