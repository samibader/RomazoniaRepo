namespace EShop.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MakeDateNullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Product", "DateAdded", c => c.DateTime());
            AlterColumn("dbo.Product", "DateModefied", c => c.DateTime());
            AlterColumn("dbo.Product", "DateAvailables", c => c.DateTime());
            AlterColumn("dbo.ProductDescription", "DateAdded", c => c.DateTime());
            AlterColumn("dbo.ProductDescription", "DateModefied", c => c.DateTime());
            AlterColumn("dbo.ProductRate", "CreationDate", c => c.DateTime());
            AlterColumn("dbo.ProductRate", "DateAdded", c => c.DateTime());
            AlterColumn("dbo.ProductRate", "DateModefied", c => c.DateTime());
            AlterColumn("dbo.Tag", "DateAdded", c => c.DateTime());
            AlterColumn("dbo.Tag", "DateModefied", c => c.DateTime());
            AlterColumn("dbo.TagDescription", "DateAdded", c => c.DateTime());
            AlterColumn("dbo.TagDescription", "DateModefied", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TagDescription", "DateModefied", c => c.DateTime(nullable: false));
            AlterColumn("dbo.TagDescription", "DateAdded", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Tag", "DateModefied", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Tag", "DateAdded", c => c.DateTime(nullable: false));
            AlterColumn("dbo.ProductRate", "DateModefied", c => c.DateTime(nullable: false));
            AlterColumn("dbo.ProductRate", "DateAdded", c => c.DateTime(nullable: false));
            AlterColumn("dbo.ProductRate", "CreationDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.ProductDescription", "DateModefied", c => c.DateTime(nullable: false));
            AlterColumn("dbo.ProductDescription", "DateAdded", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Product", "DateAvailables", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Product", "DateModefied", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Product", "DateAdded", c => c.DateTime(nullable: false));
        }
    }
}
