namespace EShop.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class offfff : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ShippingCompany",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        ShippingCost = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Address", "IsDefault", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Address", "IsDefault");
            DropTable("dbo.ShippingCompany");
        }
    }
}
