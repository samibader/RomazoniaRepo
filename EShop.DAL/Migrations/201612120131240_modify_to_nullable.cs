namespace EShop.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modify_to_nullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Order", "CreationDate", c => c.DateTime());
            AlterColumn("dbo.Order", "DateModified", c => c.DateTime());
            AlterColumn("dbo.Order", "PaymentDate", c => c.DateTime());
            AlterColumn("dbo.Order", "ClosingDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Order", "ClosingDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Order", "PaymentDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Order", "DateModified", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Order", "CreationDate", c => c.DateTime(nullable: false));
        }
    }
}
