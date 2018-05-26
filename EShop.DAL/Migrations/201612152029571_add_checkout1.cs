namespace EShop.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_checkout1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CheckOut",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        SelectedShippingAddress = c.Long(nullable: false),
                        SelectedBillingAddress = c.Long(nullable: false),
                        SelectedShippingCompany = c.Long(nullable: false),
                        SelectedShippingMethod = c.Long(nullable: false),
                        SelectedBillingMethod = c.Long(nullable: false),
                        Step = c.Int(nullable: false),
                        CouponCode = c.String(),
                        IsSame = c.Boolean(nullable: false),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CheckOut", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.CheckOut", new[] { "User_Id" });
            DropTable("dbo.CheckOut");
        }
    }
}
