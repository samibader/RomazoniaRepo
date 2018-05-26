using EShop.DAL.Entities;

using EShop.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        //ApplicationUserManager UserManager{ get;}
        //ApplicationRoleManager RoleManager{ get;}
        GenericRepository<Language> LanguageRepository{ get;}
        GenericRepository<CategoryDescription> CategoryDescriptionRepository{ get;}
        GenericRepository<Category> CategoryRepository { get; }
        GenericRepository<Sample> SampleRepository { get; }
        GenericRepository<Attrib> AttribRepository { get; }
        
         GenericRepository<AttributeDescription> AttributeDescriptionRepository{ get;}
        
         GenericRepository<Designer> DesignerRepository{ get;}
       
         GenericRepository<Discount> DiscountRepository{ get;}
        
         GenericRepository<Image> ImageRepository{ get;}
        
        
         GenericRepository<Product> ProductRepository{ get;}
        
         GenericRepository<ProductDescription> ProductDescriptionRepository{ get;}

         GenericRepository<ProductAttributeValue> ProductAttributeValueRepository { get; }
       
         GenericRepository<ProductRate> ProductRateRepository{ get;}
        
         GenericRepository<Tag> TagRepository{ get;}
       
        //GenericRepository<TagDescription> TagDescriptionRepository{ get;}

        GenericRepository<ProductSKU> ProductSKURepository { get; }

        GenericRepository<ProductSKUOptionValue> ProductSKUOptionValueRepository { get; }
        GenericRepository<Option> OptionRepository { get; }

        GenericRepository<OptionValue> OptionValueRepository { get; }
        GenericRepository<OptionValueDescription> OptionValueDescriptionRepository { get; }
        GenericRepository<OptionDescription> OptionDescriptionRepository { get; }
        GenericRepository<ShoppingCart> ShoppingCartRepository { get; }
        GenericRepository<Favorite> FavoriteRepository { get; }

        GenericRepository<Address> AddressRepository { get; }
        GenericRepository<Coupon> CouponRepository { get; }
        GenericRepository<Order> OrderRepository { get; }

        GenericRepository<OrderItem> OrderItemRepository { get; }
        GenericRepository<ShippingAddress> ShippingAddressRepository { get; }
        GenericRepository<BillingAddress> BillingAddressRepository { get; }

        GenericRepository<ShippingCompany> ShippingCompanyRepository { get; }

        GenericRepository<ShippingCompanyDescription> ShippingCompanyDescriptionRepository { get; }
        GenericRepository<ShippingBillingMethod> ShippingBillingMethodRepository { get; }
        GenericRepository<ShippingBillingMethodDescription> ShippingBillingMethodDescriptionRepository { get; }

        GenericRepository<OrderHistory> OrderHistoryRepository { get; }
        GenericRepository<CheckOut> CheckOutRepository { get; }

        GenericRepository<OptionHelper> OptionHelperRepository { get; }

        GenericRepository<SizeHelper> SizeHelperRepository { get; }
        GenericRepository<ColorHelper> ColorHelperRepository { get; }
        GenericRepository<AttributeGroup> AttributeGroupRepsitory { get; }
        GenericRepository<AttributeGroupDescription> AttributeGroupDescriptionRepsitory { get; }
        GenericRepository<DesignerDescription> DesignerDescriptionRepository { get; }
        GenericRepository<SizeAttribute> SizeAttributeRepository { get; }
        GenericRepository<SizeAttributeDescription> SizeAttributeDescriptionRepository { get; }
        //GenericRepository<SizeAttributeMedian> SizeAttributeMedianRepository { get; }
        GenericRepository<ProductSizeAttribute> ProductSizeAttributeRepository { get; }
        GenericRepository<DiscountDescription> DiscountDescriptionRepository { get; }
        GenericRepository<ProductDiscount> ProductDiscountRepository { get; }
        GenericRepository<Slide> SlideRepository { get; }

        
        Task SaveAsync();
        void Save();
    }
}
