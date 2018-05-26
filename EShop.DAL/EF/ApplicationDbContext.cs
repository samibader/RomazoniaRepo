using EShop.DAL.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.DAL.EF
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection")
        {
            
            //Database.SetInitializer<ApplicationDbContext>(new EShopDbInitializer());
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext("DefaultConnection");
        }



        public ApplicationDbContext(string connectionString) : base(connectionString = "DefaultConnection") { }
        public DbSet<Language> Languages { get; set; }
        public DbSet<CategoryDescription> CategoryDescriptions { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Attrib> Attribs { get; set; }
        public DbSet<AttributeDescription> AttributeDescriptions { get; set; }
        public DbSet<Designer> Designers { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Option> Options { get; set; }
        public DbSet<OptionDescription> OptionDescriptions { get; set; }
        public DbSet<OptionValue> OptionValues { get; set; }
        public DbSet<OptionValueDescription> OptionValueDescriptions { get; set; }
        public DbSet<ProductSKU> ProductSKUs { get; set; }
        public DbSet<ProductSKUOptionValue> ProductSKUOptionValues { get; set; }
        public DbSet<Product> Products { get; set; }
       // public DbSet<ProductAttributeValue> ProductAttributeValues { get; set; }
        public DbSet<ProductDescription> ProductDescriptions { get; set; }
        public DbSet<ProductRate> ProductRates { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
       // public DbSet<TagDescription> TagDescriptions { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Coupon> Coupons { get; set; }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<ShippingAddress> ShippingAddresses { get; set; }
        public DbSet<BillingAddress> BillingAddresses { get; set; }
        public DbSet<ShippingCompany> ShippingCompanies { get; set; }
        public DbSet<ShippingCompanyDescription> ShippingCompanyDescriptions { get; set; }
        public DbSet<ShippingBillingMethod> ShippingBillingMethods { get; set; }
        public DbSet<ShippingBillingMethodDescription> ShippingBillingMethodDescriptions { get; set; }

        public DbSet<OrderHistory> OrderHistories { get; set; }

        public DbSet<CheckOut> CheckOuts { get; set; }
        public DbSet<ColorHelper> ColorsHelper { get; set; }

        public DbSet<SizeHelper> SizesHelper { get; set; }

        public DbSet<OptionHelper> OptionHelpers { get; set; }
        public DbSet<AttributeGroup> AttributeGroups { get; set; }
        public DbSet<AttributeGroupDescription> AttributeGroupDescriptions { get; set; }
        public DbSet<DesignerDescription> DesignerDescriptions { get; set; }
        public DbSet<SizeAttribute> SizeAttributes { get; set; }
        public DbSet<SizeAttributeDescription> SizeAttributeDescriptions { get; set; }
        //public DbSet<SizeAttributeMedian> SizeAttributeMedians { get; set; }
        public DbSet<ProductSizeAttribute> ProductSizeAttributes { get; set; }
        public DbSet<DiscountDescription> DiscountDescriptions { get; set; }
        public DbSet<ProductDiscount> ProductDiscount { get; set; }
        public DbSet<Slide> Slides { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Entity<Category>().
                    HasOptional(e => e.ParentCategory).
                    WithMany().
                    HasForeignKey(m => m.ParentId);
            modelBuilder.Entity<Order>()
            .HasOptional(f => f.BillingAddress)
            .WithRequired(s => s.Order);
            modelBuilder.Entity<Order>()
            .HasOptional(f => f.ShippingAddress)
            .WithRequired(s => s.Order);

        }

        //public class EShopDbInitializer : Dr<ApplicationDbContext>
        //{
        //    protected override void Seed(ApplicationDbContext db)
        //    {
        //        db.Phones.Add(new Phone { Name = "Nokia Lumia 630", Company = "Nokia", Price = 220 });
        //        db.Phones.Add(new Phone { Name = "iPhone 6", Company = "Apple", Price = 320 });
        //        db.Phones.Add(new Phone { Name = "LG G4", Company = "lG", Price = 260 });
        //        db.Phones.Add(new Phone { Name = "Samsung Galaxy S 6", Company = "Samsung", Price = 300 });
        //        db.SaveChanges();
        //    }
        //}
    }
}
