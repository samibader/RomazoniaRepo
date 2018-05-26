namespace EShop.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingModificationDateAndNullable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Order", "DateModified", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Order", "DateModified");
        }
    }
}
