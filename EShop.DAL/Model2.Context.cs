﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EShop.DAL
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class Entities : DbContext
    {
        public Entities()
            : base("name=Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<C__MigrationHistory> C__MigrationHistory { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<Attrib> Attrib { get; set; }
        public virtual DbSet<AttributeDescription> AttributeDescription { get; set; }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<CategoryDescription> CategoryDescription { get; set; }
        public virtual DbSet<Designer> Designer { get; set; }
        public virtual DbSet<Discount> Discount { get; set; }
        public virtual DbSet<Image> Image { get; set; }
        public virtual DbSet<Language> Language { get; set; }
        public virtual DbSet<Option> Option { get; set; }
        public virtual DbSet<OptionDescription> OptionDescription { get; set; }
        public virtual DbSet<OptionValue> OptionValue { get; set; }
        public virtual DbSet<OptionValueDescription> OptionValueDescription { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<ProductAttributeValue> ProductAttributeValue { get; set; }
        public virtual DbSet<ProductDescription> ProductDescription { get; set; }
        public virtual DbSet<ProductRate> ProductRate { get; set; }
        public virtual DbSet<ProductSKU> ProductSKU { get; set; }
        public virtual DbSet<ProductSKUOptionValue> ProductSKUOptionValue { get; set; }
        public virtual DbSet<Tag> Tag { get; set; }
        public virtual DbSet<TagDescription> TagDescription { get; set; }
    }
}
