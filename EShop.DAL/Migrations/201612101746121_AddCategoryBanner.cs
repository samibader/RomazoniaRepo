namespace EShop.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCategoryBanner : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Category", "Banner", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Category", "Banner");
        }
    }
}
