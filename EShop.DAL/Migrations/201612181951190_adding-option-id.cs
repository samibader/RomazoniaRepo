namespace EShop.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingoptionid : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ColorHelper", "OptionId", c => c.Long(nullable: false));
            AddColumn("dbo.SizeHelper", "OptionId", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SizeHelper", "OptionId");
            DropColumn("dbo.ColorHelper", "OptionId");
        }
    }
}
