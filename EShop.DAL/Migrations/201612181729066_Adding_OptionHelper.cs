namespace EShop.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Adding_OptionHelper : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OptionHelper",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        ArabicName = c.String(),
                        EnglishName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.OptionHelper");
        }
    }
}
