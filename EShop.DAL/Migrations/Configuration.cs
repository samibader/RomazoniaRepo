namespace EShop.DAL.Migrations
{
    using EShop.DAL.Repositories;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Microsoft.AspNet.Identity;
    using EShop.DAL.Entities;
    using EShop.Common;
    internal sealed class Configuration : DbMigrationsConfiguration<EShop.DAL.EF.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(EShop.DAL.EF.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            if (!context.Roles.Any(r => r.Name == "AdminRole"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "AdminRole" };

                manager.Create(role);
            }
            if (!(context.Users.Any(u => u.UserName == "admin@eshop.com")))
            {
                var userStore = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(userStore);
                var userToInsert = new ApplicationUser
                {
                    UserName = "admin@eshop.com",
                    PhoneNumber = "05005500550",
                    Email = "admin@eshop.com",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    FirstName = "Admin",
                    LastName = "Admin"
                };
                userManager.Create(userToInsert, "P@ssw0rd");
                userManager.AddToRole(userToInsert.Id, "AdminRole");
            }

            //SeedData(context);

        }

//        void SeedData(EShop.DAL.EF.ApplicationDbContext context)
//        {
//            #region Languages

//            context.Languages.AddOrUpdate(c => c.Code,
//                new Language
//                {
//                    Id = (long)Langs.Arabic,
//                    ImageUrl = "~/Assets/images/flag-arabic.jpg",
//                    Name = "العربية",
//                    Status = 1,
//                    Code = "ar-sa",
//                    SortOrder = 1,
//                    DateAdded = DateTime.Now
//                },
//                new Language
//                {
//                    Id = (long)Langs.English,
//                    ImageUrl = "~/Assets/images/flag-english.jpg",
//                    Name = "English",
//                    Status = 1,
//                    Code = "en-us",
//                    SortOrder = 1,
//                    DateAdded = DateTime.Now
//                }
//            );

//            #endregion

//            #region Category

//            context.Categories.AddOrUpdate(c => new { c.ImageUrl, c.ImageThumb, c.SortOrder, c.Status }
//                ,
//                //Women
//                new Category()
//                {
//                    Id = 1,
//                    ParentId = null,
//                    ImageUrl = "/Assets/images/banner4.jpg",
//                    SortOrder = 1,
//                    Status = true,
//                    DateAdded = DateTime.Now
//                },
//                //Men
//                new Category()
//                {
//                    Id = 2,
//                    ParentId = null,
//                    ImageUrl = "/Assets/images/banner5.jpg",
//                    SortOrder = 2,
//                    Status = true,
//                    DateAdded = DateTime.Now
//                },
//                //Childs
//                new Category()
//                {
//                    Id = 3,
//                    ParentId = null,
//                    ImageUrl = "/Assets/images/banner6.jpg",
//                    SortOrder = 3,
//                    Status = true,
//                    DateAdded = DateTime.Now
//                },
//                //Women -- clothing
//                new Category()
//                {
//                    Id = 4,
//                    ParentId = 1,
//                    ImageUrl = "/uploads/JAME2Sizes1.jpg",
//                    SortOrder = 4,
//                    Status = true,
//                    DateAdded = DateTime.Now
//                },
//                //Women -- Dresses
//                new Category()
//                {
//                    Id = 5,
//                    ParentId = 4,
//                    ImageUrl = "/uploads/JAME2Sizes1.jpg",
//                    SortOrder = 4,
//                    Status = true,
//                    DateAdded = DateTime.Now
//                },
//                // Women -- shoes
//                new Category()
//                {
//                    Id = 6,
//                    ParentId = 1,
//                    ImageUrl = "/uploads/JOIEs1.jpg",
//                    SortOrder = 5,
//                    Status = true,
//                    DateAdded = DateTime.Now
//                },
//                //women -- trausers
//                new Category()
//                {
//                    Id = 7,
//                    ParentId = 4,
//                    ImageUrl = "/uploads/JOE1.jpg",
//                    SortOrder = 6,
//                    Status = true,
//                    DateAdded = DateTime.Now
//                },
//                //women-boots
//                new Category()
//                {
//                    Id = 8,
//                    ParentId = 6,
//                    ImageUrl = "/uploads/JCAM2.jpg",
//                    SortOrder = 7,
//                    Status = true,
//                    DateAdded = DateTime.Now
//                },
//                //women-flats
//                new Category()
//                {
//                    Id = 9,
//                    ParentId = 6,
//                    ImageUrl = "/uploads/JOIEs1.jpg",
//                    SortOrder = 7,
//                    Status = true,
//                    DateAdded = DateTime.Now
//                }
//            );

//            #endregion

//            #region CategoryDescription

//            context.CategoryDescriptions.AddOrUpdate(cd => cd.Name,
//                new CategoryDescription()
//                {
//                    Id = 1,
//                    LanguageId = (long)Langs.Arabic,
//                    CategoryId = 1,
//                    Name = "نسائي",
//                    Description = "فئة النساء",
//                    MetaDescription = "النسوان",
//                    DateAdded = DateTime.Now
//                },
//                new CategoryDescription()
//                {
//                    Id = 2,
//                    LanguageId = (long)Langs.English,
//                    CategoryId = 1,
//                    Name = "Women",
//                    Description = "Womens Category",
//                    MetaDescription = "Ladies",
//                    DateAdded = DateTime.Now
//                },
//                new CategoryDescription()
//                {
//                    Id = 3,
//                    LanguageId = (long)Langs.Arabic,
//                    CategoryId = 2,
//                    Name = "رجالي",
//                    Description = "فئة الرجال",
//                    MetaDescription = "زلمة",
//                    DateAdded = DateTime.Now
//                },
//                new CategoryDescription()
//                {
//                    Id = 4,
//                    LanguageId = (long)Langs.English,
//                    CategoryId = 2,
//                    Name = "Menn",
//                    Description = "Men Category",
//                    MetaDescription = "Guys",
//                    DateAdded = DateTime.Now
//                },
//                new CategoryDescription()
//                {
//                    Id = 5,
//                    LanguageId = (long)Langs.Arabic,
//                    CategoryId = 3,
//                    Name = "أطفال",
//                    Description = "فئة الاطفال",
//                    MetaDescription = "أطفال",
//                    DateAdded = DateTime.Now
//                },
//                new CategoryDescription()
//                {
//                    Id = 6,
//                    LanguageId = (long)Langs.English,
//                    CategoryId = 3,
//                    Name = "Childrens",
//                    Description = "Childrens Category",
//                    MetaDescription = "Babys",
//                    DateAdded = DateTime.Now
//                },
//                new CategoryDescription()
//                {
//                    Id = 7,
//                    LanguageId = (long)Langs.Arabic,
//                    CategoryId = 4,
//                    Name = "ملابس",
//                    Description = "فئة الملابس",
//                    MetaDescription = "ملابس",
//                    DateAdded = DateTime.Now
//                },
//                new CategoryDescription()
//                {
//                    Id = 8,
//                    LanguageId = (long)Langs.English,
//                    CategoryId = 4,
//                    Name = "clothes",
//                    Description = "clothes Category",
//                    MetaDescription = "clothes",
//                    DateAdded = DateTime.Now
//                },
//                new CategoryDescription()
//                {
//                    Id = 9,
//                    LanguageId = (long)Langs.Arabic,
//                    CategoryId = 6,
//                    Name = "أحذية",
//                    Description = "فئة الأحذية",
//                    MetaDescription = "الاحذية",
//                    DateAdded = DateTime.Now
//                },
//                new CategoryDescription()
//                {
//                    Id = 10,
//                    LanguageId = (long)Langs.English,
//                    CategoryId = 6,
//                    Name = "Shoes",
//                    Description = "Shoes Category",
//                    MetaDescription = "Shoe",
//                    DateAdded = DateTime.Now
//                },
//                new CategoryDescription()
//                {
//                    Id = 11,
//                    LanguageId = (long)Langs.Arabic,
//                    CategoryId = 5,
//                    Name = "فساتين",
//                    Description = "فئة الفساتين",
//                    MetaDescription = "فستان",
//                    DateAdded = DateTime.Now
//                },
//                new CategoryDescription()
//                {
//                    Id = 12,
//                    LanguageId = (long)Langs.English,
//                    CategoryId = 5,
//                    Name = "Dresses",
//                    Description = "Dresses Category",
//                    MetaDescription = "Dresses",
//                    DateAdded = DateTime.Now
//                },
//                new CategoryDescription()
//                {
//                    Id = 13,
//                    LanguageId = (long)Langs.Arabic,
//                    CategoryId = 7,
//                    Name = "بنطال",
//                    Description = "فئة بنطال",
//                    MetaDescription = "بنطال",
//                    DateAdded = DateTime.Now
//                },
//                new CategoryDescription()
//                {
//                    Id = 14,
//                    LanguageId = (long)Langs.English,
//                    CategoryId = 7,
//                    Name = "trausers",
//                    Description = "trausers Categories",
//                    MetaDescription = "trausers",
//                    DateAdded = DateTime.Now
//                },
//                 new CategoryDescription()
//                 {
//                     Id = 15,
//                     LanguageId = (long)Langs.Arabic,
//                     CategoryId = 8,
//                     Name = "جزم",
//                     Description = "فئة الجزم",
//                     MetaDescription = "جزمة",
//                     DateAdded = DateTime.Now
//                 },
//                new CategoryDescription()
//                {
//                    Id = 16,
//                    LanguageId = (long)Langs.English,
//                    CategoryId = 8,
//                    Name = "Boots",
//                    Description = "Boots Categories",
//                    MetaDescription = "Boot",
//                    DateAdded = DateTime.Now
//                },
//                 new CategoryDescription()
//                 {
//                     Id = 17,
//                     LanguageId = (long)Langs.Arabic,
//                     CategoryId = 9,
//                     Name = "مشايات",
//                     Description = "فئة المشايات",
//                     MetaDescription = "مشاية",
//                     DateAdded = DateTime.Now
//                 },
//                new CategoryDescription()
//                {
//                    Id = 18,
//                    LanguageId = (long)Langs.English,
//                    CategoryId = 9,
//                    Name = "Flats",
//                    Description = "Flats Categories",
//                    MetaDescription = "Flat",
//                    DateAdded = DateTime.Now
//                }
//            );

//            #endregion

//            #region Designer

//            context.Designers.AddOrUpdate(d => d.Name,
//                new Designer() { Id = 1, ImageUrl = "/Assets/Images/faq-banner.png", DateAdded = DateTime.Now, Name = "Issam" },

//                new Designer() { Id = 2, ImageUrl = "/Assets/Images/products/img03.jpg", DateAdded = DateTime.Now, Name = "Marco" });

//            #endregion



//            #region Products

//            context.Products.AddOrUpdate(p => p.Name,
//                new Product()
//                {
//                    Id = 1,
                   
//                    Price = 200,
//                    CategoryId = 5,
//                    DesignerId = 1,
                   
//                    Status = true,
//                    NumberOfViews = 3,
//                    DateAdded = DateTime.Now

//                },
//                new Product()
//                {
//                    Id = 6,
//                    Name = "بلوزة بوهو سوداء",
//                    Price = 250,
//                    CategoryId = 5,
//                    DesignerId = 1,
//                    SortOrder = 6,
//                    Status = true,
//                    NumberOfViews = 3,
//                    DateAdded = DateTime.Now
//                },
//                new Product()
//                {
//                    Id = 2,
//                    Name = "فستان James ",
//                    Price = 100,
//                    CategoryId = 5,
//                    DesignerId = 1,
//                    SortOrder = 2,
//                    Status = true,
//                    NumberOfViews = 5,
//                    DateAdded = DateTime.Now.AddDays(-15)
//                },
//                 new Product()
//                 {
//                     Id = 3,
//                     Name = "فستان KISA ",
//                     Price = 100,
//                     CategoryId = 5,
//                     DesignerId = 1,
//                     SortOrder = 7,
//                     Status = true,
//                     NumberOfViews = 5,
//                     DateAdded = DateTime.Now.AddDays(-15)
//                 },
//                new Product()
//                {
//                    Id = 4,
//                    Name = "فستان LIMA",
//                    Price = 100,
//                    CategoryId = 5,
//                    DesignerId = 1,
//                    SortOrder = 3,
//                    Status = true,
//                    NumberOfViews = 5,
//                    DateAdded = DateTime.Now.AddDays(-15)
//                },
//                new Product()
//                {
//                    Id = 5,
//                    Name = "فستان LIXA",
//                    Price = 100,
//                    CategoryId = 5,
//                    DesignerId = 1,
//                    SortOrder = 5,
//                    Status = true,
//                    NumberOfViews = 5,
//                    DateAdded = DateTime.Now
//                },

//                new Product()

//                {
//                    Id = 7,
//                    Name = "بنطال JOE",
//                    Price = 100,
//                    CategoryId = 7,
//                    DesignerId = 1,
//                    SortOrder = 6,
//                    Status = true,
//                    NumberOfViews = 5,
//                    DateAdded = DateTime.Now.AddDays(-15)
//                },
//                new Product()

//                {
//                    Id = 8,
//                    Name = "بنطال JOET",
//                    Price = 100,
//                    CategoryId = 7,
//                    DesignerId = 1,
//                    SortOrder = 6,
//                    Status = true,
//                    NumberOfViews = 5,
//                    DateAdded = DateTime.Now
//                },
//                new Product()

//                {
//                    Id = 9,
//                    Name = "جزمة JCAM",
//                    Price = 100,
//                    CategoryId = 8,
//                    DesignerId = 2,
//                    SortOrder = 6,
//                    Status = true,
//                    NumberOfViews = 5,
//                    DateAdded = DateTime.Now.AddDays(-15)
//                },
//                new Product()

//                {
//                    Id = 10,
//                    Name = "جزمة SOL",
//                    Price = 100,
//                    CategoryId = 8,
//                    DesignerId = 1,
//                    SortOrder = 6,
//                    Status = true,
//                    NumberOfViews = 5,
//                    DateAdded = DateTime.Now
//                },
//                new Product()

//                {
//                    Id = 11,
//                    Name = "مشاية JOIS",
//                    Price = 100,
//                    CategoryId = 9,
//                    DesignerId = 1,
//                    SortOrder = 6,
//                    Status = true,
//                    NumberOfViews = 5,
//                    DateAdded = DateTime.Now.AddDays(-15)
//                },
//                new Product()

//                {
//                    Id = 12,
//                    Name = "مشاية DMAR",
//                    Price = 100,
//                    CategoryId = 9,
//                    DesignerId = 2,
//                    SortOrder = 6,
//                    Status = true,
//                    NumberOfViews = 5,
//                    DateAdded = DateTime.Now
//                }
//            );

//            #endregion

//            #region Discount

//            context.Discounts.AddOrUpdate(d => new { d.IsPercentage, d.Value },
//                new Discount()
//                {
//                    DateAdded = DateTime.Now,
//                    DateEnd = DateTime.Now.AddYears(1),
//                    Value = 10,
//                    IsPercentage = true,
//                    ProductId = 1,
//                    SKUId = 1
//                });
//            #endregion
//            #region Product Descrption

//            context.ProductDescriptions.AddOrUpdate(pd => pd.Text,
//                new ProductDescription()
//                {
//                    LanguageId = (long)Langs.Arabic,
//                    ProductId = 1,
//                    Text = "بلوزة من تصميم عالمي ",
//                    MetaDescriptions = "راقية ويمكن ارتداءها لمختلف المناسبات ",
//                    MetaKeywords = "ببلوزة راقية",
//                    DateAdded = DateTime.Now
//                },
//                new ProductDescription()
//                {
//                    LanguageId = (long)Langs.English,
//                    ProductId = 1,
//                    Text = "Very Cool TShirt",
//                    MetaDescriptions = "Best Dress For Parties",
//                    MetaKeywords = "Party Dress",
//                    DateAdded = DateTime.Now
//                },
//                new ProductDescription()
//                {
//                    LanguageId = (long)Langs.Arabic,
//                    ProductId = 2,
//                    Text = "فستان راقي من شركة هوغو",
//                    MetaDescriptions = "مناسب للمناسبات الرسمية",
//                    MetaKeywords = "Party Dress",
//                    DateAdded = DateTime.Now
//                },
//                new ProductDescription()
//                {
//                    LanguageId = (long)Langs.English,
//                    ProductId = 2,
//                    Text = "Very deluxe Dress",
//                    MetaDescriptions = "Sweeeet",
//                    MetaKeywords = "Party Dress",
//                    DateAdded = DateTime.Now
//                },
//                new ProductDescription()
//                {
//                    LanguageId = (long)Langs.Arabic,
//                    ProductId = 3,
//                    Text = "فستان راقي من شركة هوغو",
//                    MetaDescriptions = "رائعة",
//                    MetaKeywords = "Party Dress",
//                    DateAdded = DateTime.Now
//                },
//                new ProductDescription()
//                {
//                    LanguageId = (long)Langs.English,
//                    ProductId = 3,
//                    Text = "very nice Dress from Booho",
//                    MetaDescriptions = "Very Nice Dress",
//                    MetaKeywords = "Party Dress",
//                    DateAdded = DateTime.Now
//                },
//                new ProductDescription()
//                {
//                    LanguageId = (long)Langs.Arabic,
//                    ProductId = 4,
//                    Text = "فستان راقي من شركة ليما",
//                    MetaDescriptions = "مصنوع من افخر انواع الاقمشة",
//                    MetaKeywords = "Party Dress",
//                    DateAdded = DateTime.Now
//                },
//                new ProductDescription()
//                {
//                    LanguageId = (long)Langs.English,
//                    ProductId = 4,
//                    Text = "Very Nice Dress From LIMA",
//                    MetaDescriptions = "made from very good quality",
//                    MetaKeywords = "Party Dress",
//                    DateAdded = DateTime.Now
//                },
//                                new ProductDescription()
//                                {
//                                    LanguageId = (long)Langs.Arabic,
//                                    ProductId = 5,
//                                    Text = "فستان راقي من شركة ليكسا",
//                                    MetaDescriptions = "مصنوع من افخر انواع الاقمشة",
//                                    MetaKeywords = "Party Dress",
//                                    DateAdded = DateTime.Now
//                                }, new ProductDescription()
//                                {
//                                    LanguageId = (long)Langs.English,
//                                    ProductId = 5,
//                                    Text = "Dress from Lixa",
//                                    MetaDescriptions = "made from very good quality",
//                                    MetaKeywords = "Party Dress",
//                                    DateAdded = DateTime.Now
//                                },
//                           new ProductDescription()
//                           {
//                               LanguageId = (long)Langs.Arabic,
//                               ProductId = 6,
//                               Text = "بلوزة راقية من شركة بوهو العالمية",
//                               MetaDescriptions = "مصنوع من افخر انواع الاقمش2",
//                               MetaKeywords = "Party Dress",
//                               DateAdded = DateTime.Now
//                           }, new ProductDescription()
//                           {
//                               LanguageId = (long)Langs.English,
//                               ProductId = 6,
//                               Text = "Booho Vey good shirt",
//                               MetaDescriptions = "made from very good quality",
//                               MetaKeywords = "Party Dress",
//                               DateAdded = DateTime.Now
//                           },
//                                                      new ProductDescription()
//                                                      {
//                                                          LanguageId = (long)Langs.Arabic,
//                                                          ProductId = 7,
//                                                          Text = "بنطال من شركة JOE",
//                                                          MetaDescriptions = "مصنوع من افخر انواع الجلود",
//                                                          MetaKeywords = "Party Dress",
//                                                          DateAdded = DateTime.Now
//                                                      }, new ProductDescription()
//                                                      {
//                                                          LanguageId = (long)Langs.English,
//                                                          ProductId = 7,
//                                                          Text = "very nice JOE trauser",
//                                                          MetaDescriptions = "made from very good quality leather",
//                                                          MetaKeywords = "Party Dress",
//                                                          DateAdded = DateTime.Now
//                                                      },
//                                                         new ProductDescription()
//                                                         {
//                                                             LanguageId = (long)Langs.Arabic,
//                                                             ProductId = 8,
//                                                             Text = "بنطال من شركة JOET",
//                                                             MetaDescriptions = "مصنوع من افخر انواع الجلود",
//                                                             MetaKeywords = "Party Dress",
//                                                             DateAdded = DateTime.Now
//                                                         }, new ProductDescription()
//                                                         {
//                                                             LanguageId = (long)Langs.English,
//                                                             ProductId = 8,
//                                                             Text = "very nice JOET trauser",
//                                                             MetaDescriptions = "made from very good quality leather",
//                                                             MetaKeywords = "Party Dress",
//                                                             DateAdded = DateTime.Now
//                                                         },
//                                                            new ProductDescription()
//                                                            {
//                                                                LanguageId = (long)Langs.Arabic,
//                                                                ProductId = 9,
//                                                                Text = "جزمة من شركة JCAM",
//                                                                MetaDescriptions = "مصنوع من افخر انواع الجلود",
//                                                                MetaKeywords = "Party Dress",
//                                                                DateAdded = DateTime.Now
//                                                            }, new ProductDescription()
//                                                            {
//                                                                LanguageId = (long)Langs.English,
//                                                                ProductId = 9,
//                                                                Text = "very nice JCAM Boot",
//                                                                MetaDescriptions = "made from very good quality leather",
//                                                                MetaKeywords = "Party Dress",
//                                                                DateAdded = DateTime.Now
//                                                            },
//                                                            new ProductDescription()
//                                                            {
//                                                                LanguageId = (long)Langs.Arabic,
//                                                                ProductId = 10,
//                                                                Text = "جزمة من شركة SOL",
//                                                                MetaDescriptions = "مصنوع من افخر انواع الجلود",
//                                                                MetaKeywords = "Party Dress",
//                                                                DateAdded = DateTime.Now
//                                                            }, new ProductDescription()
//                                                            {
//                                                                LanguageId = (long)Langs.English,
//                                                                ProductId = 10,
//                                                                Text = "very nice SOL Boot",
//                                                                MetaDescriptions = "made from very good quality leather",
//                                                                MetaKeywords = "Party Dress",
//                                                                DateAdded = DateTime.Now
//                                                            },
//                                                            new ProductDescription()
//                                                            {
//                                                                LanguageId = (long)Langs.Arabic,
//                                                                ProductId = 11,
//                                                                Text = "مشاية JOIS",
//                                                                MetaDescriptions = "مصنوع من افخر انواع الجلود",
//                                                                MetaKeywords = "Party Dress",
//                                                                DateAdded = DateTime.Now
//                                                            }, new ProductDescription()
//                                                            {
//                                                                LanguageId = (long)Langs.English,
//                                                                ProductId = 11,
//                                                                Text = "very nice JOUS FLAT",
//                                                                MetaDescriptions = "made from very good quality leather",
//                                                                MetaKeywords = "Party Dress",
//                                                                DateAdded = DateTime.Now
//                                                            },
//                                                            new ProductDescription()
//                                                            {
//                                                                LanguageId = (long)Langs.Arabic,
//                                                                ProductId = 12,
//                                                                Text = "مشاية من شركة DMAR",
//                                                                MetaDescriptions = "مصنوع من افخر انواع الجلود",
//                                                                MetaKeywords = "Party Dress",
//                                                                DateAdded = DateTime.Now
//                                                            }, new ProductDescription()
//                                                            {
//                                                                LanguageId = (long)Langs.English,
//                                                                ProductId = 12,
//                                                                Text = "very nice DMAR FLAT",
//                                                                MetaDescriptions = "made from very good quality leather",
//                                                                MetaKeywords = "Party Dress",
//                                                                DateAdded = DateTime.Now
//                                                            }

//            );

//            #endregion

//            #region Option

//            context.Options.AddOrUpdate(o => new { o.OptionId, o.ProductId },
//                //Color
//                //new Option() { ProductId = 1, OptionId = 1, IsColor = true },
//                ////Size
//                //new Option() { ProductId = 1, OptionId = 2, IsColor = false },
//                //Color
//                new Option() { ProductId = 6, OptionId = 1, IsColor = false },

//                new Option() { ProductId = 2, OptionId = 1, IsColor = false },

//                new Option() { ProductId = 3, OptionId = 1, IsColor = true },
//                new Option() { ProductId = 3, OptionId = 2, IsColor = false },

//                new Option() { ProductId = 4, OptionId = 1, IsColor = false },

//                new Option() { ProductId = 9, OptionId = 1, IsColor = false },

//                new Option() { ProductId = 12, OptionId = 1, IsColor = false }




//                );

//            #endregion

//            #region OptionDescription

//            context.OptionDescriptions.AddOrUpdate(op => new { op.OptionId, op.ProductId, op.LanguageId },
//                ////Color
//                //new OptionDescription() { ProductId = 1, OptionId = 1, LanguageId = (long)Langs.Arabic, Name = "اللون" },
//                ////Size
//                //new OptionDescription() { ProductId = 1, OptionId = 2, LanguageId = (long)Langs.Arabic, Name = "القياس" },
//                //Color
//                //new OptionDescription() { ProductId = 2, OptionId = 1, LanguageId = (long)Langs.Arabic, Name = "اللون" },
//                //Size
//                new OptionDescription() { ProductId = 6, OptionId = 1, LanguageId = (long)Langs.Arabic, Name = "القياس" },

//                new OptionDescription() { ProductId = 2, OptionId = 1, LanguageId = (long)Langs.Arabic, Name = "القياس" },
//                new OptionDescription() { ProductId = 3, OptionId = 1, LanguageId = (long)Langs.Arabic, Name = "اللون" },


//                new OptionDescription() { ProductId = 3, OptionId = 2, LanguageId = (long)Langs.Arabic, Name = "القياس" },

//                new OptionDescription() { ProductId = 4, OptionId = 1, LanguageId = (long)Langs.Arabic, Name = "القياس" },

//                new OptionDescription() { ProductId = 9, OptionId = 1, LanguageId = (long)Langs.Arabic, Name = "القياس" },

//                new OptionDescription() { ProductId = 12, OptionId = 1, LanguageId = (long)Langs.Arabic, Name = "القياس" },



//                 new OptionDescription() { ProductId = 6, OptionId = 1, LanguageId = (long)Langs.English, Name = "Size" },

//                new OptionDescription() { ProductId = 2, OptionId = 1, LanguageId = (long)Langs.English, Name = "Size" },
//                new OptionDescription() { ProductId = 3, OptionId = 1, LanguageId = (long)Langs.English, Name = "Color" },


//                new OptionDescription() { ProductId = 3, OptionId = 2, LanguageId = (long)Langs.English, Name = "Size" },

//                new OptionDescription() { ProductId = 4, OptionId = 1, LanguageId = (long)Langs.English, Name = "Size" },

//                new OptionDescription() { ProductId = 9, OptionId = 1, LanguageId = (long)Langs.English, Name = "Size" },

//                new OptionDescription() { ProductId = 12, OptionId = 1, LanguageId = (long)Langs.English, Name = "Size" }



//                );


//            #endregion

//            #region OptionValue

//            context.OptionValues.AddOrUpdate(op => new { op.OptionId, op.ProductId, op.ValueId },

//                //Product 1 Color Red
//                //new OptionValue() { ValueId = 1, ProductId = 1, OptionId = 1 },
//                ////Product 1 Color Gold
//                //new OptionValue() { ValueId = 2, ProductId = 1, OptionId = 1 },
//                ////P1 Size Small
//                //new OptionValue() { ValueId = 1, ProductId = 1, OptionId = 2 },
//                //P1 size large
//                new OptionValue() { ValueId = 1, ProductId = 6, OptionId = 1 },
//                new OptionValue() { ValueId = 2, ProductId = 6, OptionId = 1 },

//               new OptionValue() { ValueId = 1, ProductId = 2, OptionId = 1 },
//                new OptionValue() { ValueId = 2, ProductId = 2, OptionId = 1 },

//                 new OptionValue() { ValueId = 1, ProductId = 3, OptionId = 1, Value = "#475767" },
//                new OptionValue() { ValueId = 2, ProductId = 3, OptionId = 1, Value = "#b72959" },
//                 new OptionValue() { ValueId = 1, ProductId = 3, OptionId = 2 },
//                new OptionValue() { ValueId = 2, ProductId = 3, OptionId = 2 },

//                new OptionValue() { ValueId = 1, ProductId = 4, OptionId = 1 },
//                new OptionValue() { ValueId = 2, ProductId = 4, OptionId = 1 },

//                //p5 Size Small 
//                new OptionValue() { ValueId = 1, ProductId = 9, OptionId = 1 },
//                //p5 Size Large
//                new OptionValue() { ValueId = 2, ProductId = 9, OptionId = 1 },

//                new OptionValue() { ValueId = 1, ProductId = 12, OptionId = 1 },
//                new OptionValue() { ValueId = 2, ProductId = 12, OptionId = 1 }
//                );
//            #endregion

//            #region OptionValueDescription

//            context.OptionValueDescriptions.AddOrUpdate(
//                op => new { op.ValueId, op.ProductId, op.OptionId, op.LanguageId },

//                //Product 1 Color Red
//                new OptionValueDescription()
//                {
//                    ValueId = 1,
//                    ProductId = 6,
//                    OptionId = 1,
//                    LanguageId = (long)Langs.Arabic,
//                    Name = "صغير"
//                },
//                new OptionValueDescription()
//                {
//                    ValueId = 1,
//                    ProductId = 6,
//                    OptionId = 1,
//                    LanguageId = (long)Langs.English,
//                    Name = "Small"
//                },

//                new OptionValueDescription()
//                {
//                    ValueId = 2,
//                    ProductId = 6,
//                    OptionId = 1,
//                    LanguageId = (long)Langs.Arabic,
//                    Name = "كبير"
//                },
//                new OptionValueDescription()
//                {
//                    ValueId = 2,
//                    ProductId = 6,
//                    OptionId = 1,
//                    LanguageId = (long)Langs.English,
//                    Name = "Big"
//                },

//                new OptionValueDescription()
//                {
//                    ValueId = 1,
//                    ProductId = 2,
//                    OptionId = 1,
//                    LanguageId = (long)Langs.Arabic,
//                    Name = "صغير"
//                },
//                new OptionValueDescription()
//                {
//                    ValueId = 1,
//                    ProductId = 2,
//                    OptionId = 1,
//                    LanguageId = (long)Langs.English,
//                    Name = "Small"
//                },

//                new OptionValueDescription()
//                {
//                    ValueId = 2,
//                    ProductId = 2,
//                    OptionId = 1,
//                    LanguageId = (long)Langs.Arabic,
//                    Name = "كبير"
//                },
//                new OptionValueDescription()
//                {
//                    ValueId = 2,
//                    ProductId = 2,
//                    OptionId = 1,
//                    LanguageId = (long)Langs.English,
//                    Name = "Big"
//                },


//                 new OptionValueDescription()
//                 {
//                     ValueId = 1,
//                     ProductId = 3,
//                     OptionId = 2,
//                     LanguageId = (long)Langs.Arabic,
//                     Name = "صغير"
//                 },
//                new OptionValueDescription()
//                {
//                    ValueId = 1,
//                    ProductId = 3,
//                    OptionId = 2,
//                    LanguageId = (long)Langs.English,
//                    Name = "Small"
//                },

//                new OptionValueDescription()
//                {
//                    ValueId = 2,
//                    ProductId = 3,
//                    OptionId = 2,
//                    LanguageId = (long)Langs.Arabic,
//                    Name = "كبير"
//                },
//                new OptionValueDescription()
//                {
//                    ValueId = 2,
//                    ProductId = 3,
//                    OptionId = 2,
//                    LanguageId = (long)Langs.English,
//                    Name = "Big"
//                },

//                new OptionValueDescription()
//                {
//                    ValueId = 1,
//                    ProductId = 3,
//                    OptionId = 1,
//                    LanguageId = (long)Langs.Arabic,
//                    Name = "أحمر"
//                },
//                new OptionValueDescription()
//                {
//                    ValueId = 1,
//                    ProductId = 3,
//                    OptionId = 1,
//                    LanguageId = (long)Langs.English,
//                    Name = "Red"
//                },

//                new OptionValueDescription()
//                {
//                    ValueId = 2,
//                    ProductId = 3,
//                    OptionId = 1,
//                    LanguageId = (long)Langs.Arabic,
//                    Name = "أسود"
//                },
//                new OptionValueDescription()
//                {
//                    ValueId = 2,
//                    ProductId = 3,
//                    OptionId = 1,
//                    LanguageId = (long)Langs.English,
//                    Name = "Black"
//                },




//                new OptionValueDescription()
//                {
//                    ValueId = 1,
//                    ProductId = 4,
//                    OptionId = 1,
//                    LanguageId = (long)Langs.Arabic,
//                    Name = "صغير"
//                },
//                new OptionValueDescription()
//                {
//                    ValueId = 1,
//                    ProductId = 4,
//                    OptionId = 1,
//                    LanguageId = (long)Langs.English,
//                    Name = "Small"
//                },

//                new OptionValueDescription()
//                {
//                    ValueId = 2,
//                    ProductId = 4,
//                    OptionId = 1,
//                    LanguageId = (long)Langs.Arabic,
//                    Name = "كبير"
//                },
//                new OptionValueDescription()
//                {
//                    ValueId = 2,
//                    ProductId = 4,
//                    OptionId = 1,
//                    LanguageId = (long)Langs.English,
//                    Name = "Big"
//                },




//                new OptionValueDescription()
//                {
//                    ValueId = 1,
//                    ProductId = 9,
//                    OptionId = 1,
//                    LanguageId = (long)Langs.Arabic,
//                    Name = "صغير"
//                },
//                new OptionValueDescription()
//                {
//                    ValueId = 1,
//                    ProductId = 9,
//                    OptionId = 1,
//                    LanguageId = (long)Langs.English,
//                    Name = "Small"
//                },

//                new OptionValueDescription()
//                {
//                    ValueId = 2,
//                    ProductId = 9,
//                    OptionId = 1,
//                    LanguageId = (long)Langs.Arabic,
//                    Name = "كبير"
//                },
//                new OptionValueDescription()
//                {
//                    ValueId = 2,
//                    ProductId = 9,
//                    OptionId = 1,
//                    LanguageId = (long)Langs.English,
//                    Name = "Big"
//                },



//                new OptionValueDescription()
//                {
//                    ValueId = 1,
//                    ProductId = 12,
//                    OptionId = 1,
//                    LanguageId = (long)Langs.Arabic,
//                    Name = "صغير"
//                },
//                new OptionValueDescription()
//                {
//                    ValueId = 1,
//                    ProductId = 12,
//                    OptionId = 1,
//                    LanguageId = (long)Langs.English,
//                    Name = "Small"
//                },

//                new OptionValueDescription()
//                {
//                    ValueId = 2,
//                    ProductId = 12,
//                    OptionId = 1,
//                    LanguageId = (long)Langs.Arabic,
//                    Name = "كبير"
//                },
//                new OptionValueDescription()
//                {
//                    ValueId = 2,
//                    ProductId = 12,
//                    OptionId = 1,
//                    LanguageId = (long)Langs.English,
//                    Name = "Big"
//                }

//                );
//            #endregion

//            #region OptionValueImages

//            context.Images.AddOrUpdate(img => img.ImageUrl,
//                //Product 1 Color Red
//                //new Image()
//                //{
//                //    OptionValueValueId = 1,
//                //    OptionValueProductId = 1,
//                //    OptionValueOptionId = 1,
//                //    ImageUrl = "/Assets/images/products/img01.jpg"
//                //},
//                ////Product 1 Color Gold
//                //new Image()
//                //{
//                //    OptionValueValueId = 2,
//                //    OptionValueProductId = 1,
//                //    OptionValueOptionId = 1,
//                //    ImageUrl = "/Assets/images/products/img02.jpg"
//                //},

//                new Image()
//                {
//                    ProductId = 6,
//                    ImageUrl = "buhoShirt1.jpg"
//                },
//                new Image()
//                {
//                    ProductId = 6,
//                    ImageUrl = "buhoShirt2.jpg"
//                },
//                new Image()
//                {
//                    ProductId = 6,
//                    ImageUrl = "buhoShirt3.jpg"
//                },
//                new Image()
//                {
//                    ProductId = 1,
//                    ImageUrl = "AXParis1.jpg"
//                },
//                new Image()
//                {
//                    ProductId = 1,
//                    ImageUrl = "AXParis2.jpg"
//                },
//                new Image()
//                {
//                    ProductId = 1,
//                    ImageUrl = "AXParis3.jpg"
//                },
//                //p2 Color Black
//                new Image()
//                {
//                    ProductId = 2,
//                    ImageUrl = "JAME2Sizes1.jpg"
//                },

//                //p3 Color pink
//                new Image()
//                {
//                    OptionValueValueId = 2,
//                    OptionValueProductId = 3,
//                    OptionValueOptionId = 1,
//                    ImageUrl = "KISA1.jpg"
//                },
//                new Image()
//                {
//                    OptionValueValueId = 2,
//                    OptionValueProductId = 3,
//                    OptionValueOptionId = 1,
//                    ImageUrl = "KISA2.jpg"
//                },
//                new Image()
//                {
//                    OptionValueValueId = 2,
//                    OptionValueProductId = 3,
//                    OptionValueOptionId = 1,
//                    ImageUrl = "KISA3.jpg"
//                },

//                new Image()//p3 Color blue
//                {
//                    OptionValueValueId = 1,
//                    OptionValueProductId = 3,
//                    OptionValueOptionId = 1,
//                    ImageUrl = "KISABlue1.jpg"
//                },
//                new Image()//p3 Color blue
//                {
//                    OptionValueValueId = 1,
//                    OptionValueProductId = 3,
//                    OptionValueOptionId = 1,
//                    ImageUrl = "KISABlue2.jpg"
//                },
//                new Image()//p3 Color blue
//                {
//                    OptionValueValueId = 1,
//                    OptionValueProductId = 3,
//                    OptionValueOptionId = 1,
//                    ImageUrl = "KISABlue3.jpg"
//                },
//                new Image()
//                {
//                    ProductId = 4,
//                    ImageUrl = "LimaToColorOneSize1.jpg"
//                },
//                new Image()
//                {
//                    ProductId = 4,
//                    ImageUrl = "LimaToColorOneSize2.jpg"
//                },
//                new Image()
//                {
//                    ProductId = 4,
//                    ImageUrl = "LimaToColorOneSize3.jpg"
//                },
//                new Image()
//                {
//                    ProductId = 5,
//                    ImageUrl = "LEAXOneColorOneSize1.jpg"
//                },
//                  new Image()
//                  {
//                      ProductId = 5,
//                      ImageUrl = "LEAXOneColorOneSize2.jpg"
//                  },
//                new Image()
//                {
//                    ProductId = 5,
//                    ImageUrl = "LEAXOneColorOneSize3.jpg"
//                },
//                new Image()
//                {
//                    ProductId = 7,
//                    ImageUrl = "JOE1.jpg"
//                },
//                new Image()
//                {
//                    ProductId = 7,
//                    ImageUrl = "JOE2.jpg"
//                },
//                new Image()
//                {
//                    ProductId = 7,
//                    ImageUrl = "JOE3.jpg"
//                },
//                new Image()
//                {
//                    ProductId = 8,
//                    ImageUrl = "JOEt1.jpg"
//                },
//                new Image()
//                {
//                    ProductId = 8,
//                    ImageUrl = "JOEt2.jpg"
//                },
//                new Image()
//                {
//                    ProductId = 8,
//                    ImageUrl = "JOEt3.jpg"
//                },
//                new Image()
//                {
//                    ProductId = 9,
//                    ImageUrl = "JCAM1.jpg"
//                },
//                new Image()
//                {
//                    ProductId = 10,
//                    ImageUrl = "SOLS1.jpg"
//                },
//                new Image()
//                {
//                    ProductId = 10,
//                    ImageUrl = "SOLS2.jpg"
//                },
//                new Image()
//                {
//                    ProductId = 10,
//                    ImageUrl = "SOLS3.jpg"
//                },
//                new Image()
//                {
//                    ProductId = 11,
//                    ImageUrl = "JOIEs1.jpg"
//                },
//                new Image()
//                {
//                    ProductId = 11,
//                    ImageUrl = "JOIEs2.jpg"
//                }, new Image()
//                {
//                    ProductId = 11,
//                    ImageUrl = "JOIEs3.jpg"
//                },
//                new Image()
//                {
//                    ProductId = 12,
//                    ImageUrl = "DMAR1.jpg"
//                },
//                new Image()
//                {
//                    ProductId = 12,
//                    ImageUrl = "DMAR2.jpg"
//                },
//                new Image()
//                {
//                    ProductId = 12,
//                    ImageUrl = "DMAR3jpg"
//                }

//);

//            #endregion


//            #region ProductSKU
//            context.ProductSKUs.AddOrUpdate(p => new { p.ProductId, p.SKUId },
//                new ProductSKU() { ProductId = 1, SKUId = 1, SKU = "P1", Quentity = 5 },

//                new ProductSKU() { ProductId = 6, SKUId = 2, SKU = "P6Ss", Quentity = 5 },
//                new ProductSKU() { ProductId = 6, SKUId = 3, SKU = "P6Sl", Quentity = 5 },

//                new ProductSKU() { ProductId = 2, SKUId = 4, SKU = "P2Ss", Quentity = 5 },
//                new ProductSKU() { ProductId = 2, SKUId = 5, SKU = "P2Sl", Quentity = 5 },

//                new ProductSKU() { ProductId = 3, SKUId = 6, SKU = "P3CbSs", Quentity = 5 },
//                new ProductSKU() { ProductId = 3, SKUId = 7, SKU = "P3CbSl", Quentity = 5 },
//                new ProductSKU() { ProductId = 3, SKUId = 8, SKU = "P3CpSs", Quentity = 5 },


//                 new ProductSKU() { ProductId = 4, SKUId = 9, SKU = "P4Ss", Quentity = 5 },
//                new ProductSKU() { ProductId = 4, SKUId = 10, SKU = "P4Sl", Quentity = 5 },

//                 new ProductSKU() { ProductId = 5, SKUId = 11, SKU = "P5", Quentity = 5 },

//                  new ProductSKU() { ProductId = 7, SKUId = 12, SKU = "P7", Quentity = 5 },

//                   new ProductSKU() { ProductId = 8, SKUId = 13, SKU = "P8", Quentity = 5 },

//                 new ProductSKU() { ProductId = 9, SKUId = 14, SKU = "P9Ss", Quentity = 5 },
//                new ProductSKU() { ProductId = 9, SKUId = 15, SKU = "P9Sl", Quentity = 5 },

//                 new ProductSKU() { ProductId = 10, SKUId = 16, SKU = "P10", Quentity = 5 },

//                  new ProductSKU() { ProductId = 11, SKUId = 17, SKU = "P11", Quentity = 5 },

//                 new ProductSKU() { ProductId = 12, SKUId = 18, SKU = "P12Ss", Quentity = 5 },
//                new ProductSKU() { ProductId = 12, SKUId = 19, SKU = "P12Sl", Quentity = 5 }

//                );


//            #endregion


//            #region ProductSKU OpthionValue
//            context.ProductSKUOptionValues.AddOrUpdate(p => new { p.ProductId, p.SKUId, p.OptionId, p.ValueId },
//                //P1Cr               
//                //new ProductSKUOptionValue() { ProductId = 1, SKUId = 1, OptionId = 1, ValueId = 1 },
//                ////P1Ss
//                //new ProductSKUOptionValue() { ProductId = 1, SKUId = 1, OptionId = 2, ValueId = 1 },
//                ////p1cr
//                new ProductSKUOptionValue() { ProductId = 6, SKUId = 2, OptionId = 1, ValueId = 1 },
//                new ProductSKUOptionValue() { ProductId = 6, SKUId = 3, OptionId = 1, ValueId = 2 },

//                 new ProductSKUOptionValue() { ProductId = 2, SKUId = 4, OptionId = 1, ValueId = 1 },
//                new ProductSKUOptionValue() { ProductId = 2, SKUId = 5, OptionId = 1, ValueId = 2 },

//                new ProductSKUOptionValue() { ProductId = 3, SKUId = 6, OptionId = 1, ValueId = 1 },
//                new ProductSKUOptionValue() { ProductId = 3, SKUId = 6, OptionId = 2, ValueId = 1 },
//                new ProductSKUOptionValue() { ProductId = 3, SKUId = 7, OptionId = 1, ValueId = 1 },
//                new ProductSKUOptionValue() { ProductId = 3, SKUId = 7, OptionId = 2, ValueId = 2 },
//                new ProductSKUOptionValue() { ProductId = 3, SKUId = 8, OptionId = 1, ValueId = 2 },
//                new ProductSKUOptionValue() { ProductId = 3, SKUId = 8, OptionId = 2, ValueId = 1 },

//                new ProductSKUOptionValue() { ProductId = 4, SKUId = 9, OptionId = 1, ValueId = 1 },
//                new ProductSKUOptionValue() { ProductId = 4, SKUId = 10, OptionId = 1, ValueId = 2 },

//                new ProductSKUOptionValue() { ProductId = 9, SKUId = 14, OptionId = 1, ValueId = 1 },
//                new ProductSKUOptionValue() { ProductId = 9, SKUId = 15, OptionId = 1, ValueId = 2 },

//                //p1Sl
//                new ProductSKUOptionValue() { ProductId = 12, SKUId = 18, OptionId = 1, ValueId = 1 },
//                //p1Cg
//                new ProductSKUOptionValue() { ProductId = 12, SKUId = 19, OptionId = 1, ValueId = 2 }




//                );


//            #endregion

//            #region Images

//            context.Images.AddOrUpdate(img => img.ImageUrl,

//            new Image() { ImageUrl = "/Assets/images/products/img01.jpg", ProductId = 4 },
//            new Image() { ImageUrl = "/Assets/images/products/img01.jpg", ProductId = 5 }
//            );
//            #endregion
//        }
    }
}
