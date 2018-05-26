namespace EShop.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ADDIscolorToOption : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Option", "IsColor", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Option", "IsColor");
        }
    }
}
