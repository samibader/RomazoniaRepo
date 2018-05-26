namespace EShop.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addCompanyToOrder : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Order", "ArabicCompanyName", c => c.String());
            AddColumn("dbo.Order", "EnglishCompanyName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Order", "EnglishCompanyName");
            DropColumn("dbo.Order", "ArabicCompanyName");
        }
    }
}
