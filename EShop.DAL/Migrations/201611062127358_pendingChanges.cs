namespace EShop.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class pendingChanges : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OptionValue", "Value", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.OptionValue", "Value");
        }
    }
}
