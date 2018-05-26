namespace EShop.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change_invoiceId_toString : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Order", "InvoiceId", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Order", "InvoiceId", c => c.Long(nullable: false));
        }
    }
}
