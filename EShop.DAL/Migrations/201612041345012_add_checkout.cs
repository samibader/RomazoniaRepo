namespace EShop.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_checkout : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Address",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Address1 = c.String(),
                        Phone = c.String(),
                        City = c.String(),
                        PostCode = c.String(),
                        Country = c.String(),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.BillingAddress",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Address1 = c.String(),
                        Phone = c.String(),
                        City = c.String(),
                        PostCode = c.String(),
                        Country = c.String(),
                        UserId = c.String(),
                        OrderId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Order", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Order",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        UserId = c.String(),
                        ShippingAddressId = c.Long(nullable: false),
                        BillingAddressId = c.Long(nullable: false),
                        CouponCode = c.String(),
                        SubTotal = c.Double(nullable: false),
                        ShippingCost = c.Double(nullable: false),
                        Discount = c.Double(nullable: false),
                        Total = c.Double(nullable: false),
                        InvoiceId = c.Long(nullable: false),
                        TransactionId = c.Long(nullable: false),
                        TransactionDetails = c.String(),
                        Status = c.String(),
                        PaymentNotes = c.String(),
                        CreationDate = c.DateTime(nullable: false),
                        PaymentDate = c.DateTime(nullable: false),
                        ClosingDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OrderItem",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        OrderId = c.Long(nullable: false),
                        Name = c.String(),
                        ImageUrl = c.String(),
                        Quantity = c.Int(nullable: false),
                        UnitPrice = c.Double(nullable: false),
                        TotalPrice = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Order", t => t.OrderId)
                .Index(t => t.OrderId);
            
            CreateTable(
                "dbo.ShippingAddress",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Address1 = c.String(),
                        Phone = c.String(),
                        City = c.String(),
                        PostCode = c.String(),
                        Country = c.String(),
                        UserId = c.String(),
                        OrderId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Order", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Coupon",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Code = c.String(),
                        CreationDate = c.DateTime(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(),
                        Value = c.Int(nullable: false),
                        IsPercentage = c.Boolean(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        MaxNumOfUsage = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ShippingAddress", "Id", "dbo.Order");
            DropForeignKey("dbo.OrderItem", "OrderId", "dbo.Order");
            DropForeignKey("dbo.BillingAddress", "Id", "dbo.Order");
            DropForeignKey("dbo.Address", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.ShippingAddress", new[] { "Id" });
            DropIndex("dbo.OrderItem", new[] { "OrderId" });
            DropIndex("dbo.BillingAddress", new[] { "Id" });
            DropIndex("dbo.Address", new[] { "UserId" });
            DropTable("dbo.Coupon");
            DropTable("dbo.ShippingAddress");
            DropTable("dbo.OrderItem");
            DropTable("dbo.Order");
            DropTable("dbo.BillingAddress");
            DropTable("dbo.Address");
        }
    }
}
