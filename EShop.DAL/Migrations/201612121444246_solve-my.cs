namespace EShop.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class solvemy : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OrderHistory",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        OrderId = c.Long(nullable: false),
                        State = c.Long(nullable: false),
                        DateAdded = c.DateTime(),
                        Description = c.String(),
                        NotifyUser = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Order", t => t.OrderId)
                .Index(t => t.OrderId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderHistory", "OrderId", "dbo.Order");
            DropIndex("dbo.OrderHistory", new[] { "OrderId" });
            DropTable("dbo.OrderHistory");
        }
    }
}
