using EShop.DAL.Entities;
using EShop.DAL.EF;
using EShop.DAL.Interfaces;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin.Security;

namespace EShop.DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext db;

        #region Repos
        //private ApplicationUserManager _userManager;
        //private ApplicationRoleManager _roleManager;
        //private ApplicationSignInManager _signInManager;
        private GenericRepository<Language> _languageRepository;
        private GenericRepository<CategoryDescription> _categoryDescriptionRepository;
        private GenericRepository<Category> _categoryRepository;
        private GenericRepository<Sample> _sampleRepository;
        private GenericRepository<Attrib> _attribRepository;
        private GenericRepository<AttributeDescription> _attributeDescriptionRepository;
        private GenericRepository<Designer> _designerRepository;
        private GenericRepository<Discount> _discountRepository;
        private GenericRepository<Image> _imageRepository;
        private GenericRepository<Product> _productRepository;
        private GenericRepository<ProductAttributeValue> _productAttributeValueRepository;
        private GenericRepository<ProductDescription> _productDescriptionRepository;
        private GenericRepository<ProductRate> _productRateRepository;
        private GenericRepository<Tag> _tagRepository;
       // private GenericRepository<TagDescription> _tagDescriptionRepository;
        private GenericRepository<ProductSKU> _productSKURepository;
        private GenericRepository<ProductSKUOptionValue> _productSKUOptionValueRepository;
        private GenericRepository<OptionValue> _optionValueRepository;
        private GenericRepository<Option> _optionRepository;
        private GenericRepository<OptionDescription> _optionDescriptionRepository;
        private GenericRepository<OptionValueDescription> _optionValueDescriptionRepository;
        private GenericRepository<ShoppingCart> _shoppingCartRepository;
        private GenericRepository<Favorite> _favoriteRepository;
        private GenericRepository<Address> _addressRepository;
        private GenericRepository<Coupon> _couponRepository;
        private GenericRepository<Order> _orderRepository;
        private GenericRepository<OrderItem> _orderItemRepository;
        private GenericRepository<ShippingAddress> _shippingAddressRepository;
        private GenericRepository<BillingAddress> _billingAddressRepository;

        private GenericRepository<ShippingCompany> _shippingCompanyRepository;

        private GenericRepository<ShippingCompanyDescription> _shippingCompanyDescriptionRepository;
        private GenericRepository<ShippingBillingMethod> _shippingBillingMethodRepository;
        private GenericRepository<ShippingBillingMethodDescription> _shippingBillingMethodDescriptionRepository;

        private GenericRepository<OrderHistory> _orderHistoryRepository;
        private GenericRepository<CheckOut> _checkOutRepository;

        private GenericRepository<OptionHelper> _optionHelperRepository;
        private GenericRepository<SizeHelper> _sizeHelperRepository;
        private GenericRepository<ColorHelper> _colorHelperRepository;
        private GenericRepository<AttributeGroup> _attributeGroupRepository;
        private GenericRepository<AttributeGroupDescription> _attributeGroupDescriptionRepository;
        private GenericRepository<DesignerDescription> _designerDescriptionRepository;
        private GenericRepository<SizeAttribute> _sizeAttributeRepository;
        private GenericRepository<SizeAttributeDescription> _sizeAttributeDescriptionRepository;
        //private GenericRepository<SizeAttributeMedian> _sizeAttributeMedianRepository;
        private GenericRepository<ProductSizeAttribute> _productSizeAttributeRepository;
        private GenericRepository<DiscountDescription> _discountDescriptionRepository;
        private GenericRepository<ProductDiscount> _productDiscountRepository;
        private GenericRepository<Slide> _SlideRepository;

        #endregion

        public UnitOfWork(string connectionString)
        {
            db = new ApplicationDbContext(connectionString);
        }

        #region Props
        //public ApplicationUserManager UserManager
        //{
        //    get
        //    {

        //        if (this._userManager == null)
        //        {
        //            this._userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(db));
        //        }
        //        return _userManager;
        //    }
        //}
        //public ApplicationRoleManager RoleManager
        //{
        //    get
        //    {

        //        if (this._roleManager == null)
        //        {
        //            this._roleManager = new ApplicationRoleManager(new RoleStore<ApplicationRole>(db));
        //        }
        //        return _roleManager;
        //    }
        //}


        //public ApplicationSignInManager SignInManager
        //{
        //    get
        //    {
        //        //if (this._signInManager == null)
        //        //{
        //        //    this._signInManager = new ApplicationSignInManager(UserManagernager,new 
        //        //}
        //        //return _roleManager;
        //        return _signInManager;
        //    }
        //    private set
        //    {
        //        _signInManager = value;
        //    }
        //}
        public GenericRepository<Language> LanguageRepository
        {
            get
            {

                if (this._languageRepository == null)
                {
                    this._languageRepository = new GenericRepository<Language>(db);
                }
                return _languageRepository;
            }
        }
        public GenericRepository<ShoppingCart> ShoppingCartRepository
        {
            get
            {

                if (this._shoppingCartRepository == null)
                {
                    this._shoppingCartRepository = new GenericRepository<ShoppingCart>(db);
                }
                return _shoppingCartRepository;
            }
        }
        public GenericRepository<Favorite> FavoriteRepository
        {
            get
            {

                if (this._favoriteRepository == null)
                {
                    this._favoriteRepository = new GenericRepository<Favorite>(db);
                }
                return _favoriteRepository;
            }
        }
        public GenericRepository<CategoryDescription> CategoryDescriptionRepository
        {
            get
            {

                if (this._categoryDescriptionRepository == null)
                {
                    this._categoryDescriptionRepository = new GenericRepository<CategoryDescription>(db);
                }
                return _categoryDescriptionRepository;
            }
        }
        public GenericRepository<Category> CategoryRepository
        {
            get
            {

                if (this._categoryRepository == null)
                {
                    this._categoryRepository = new GenericRepository<Category>(db);
                }
                return _categoryRepository;
            }
        }
        public GenericRepository<Sample> SampleRepository
        {
            get
            {

                if (this._sampleRepository == null)
                {
                    this._sampleRepository = new GenericRepository<Sample>(db);
                }
                return _sampleRepository;
            }
        }
        public GenericRepository<Attrib> AttribRepository
        {
            get
            {

                if (this._attribRepository == null)
                {
                    this._attribRepository = new GenericRepository<Attrib>(db);
                }
                return _attribRepository;
            }
        }
        public GenericRepository<AttributeDescription> AttributeDescriptionRepository
        {
            get
            {

                if (this._attributeDescriptionRepository == null)
                {
                    this._attributeDescriptionRepository = new GenericRepository<AttributeDescription>(db);
                }
                return _attributeDescriptionRepository;
            }
        }
        public GenericRepository<Designer> DesignerRepository
        {
            get
            {

                if (this._designerRepository == null)
                {
                    this._designerRepository = new GenericRepository<Designer>(db);
                }
                return _designerRepository;
            }
        }

        public GenericRepository<Discount> DiscountRepository
        {
            get
            {

                if (this._discountRepository == null)
                {
                    this._discountRepository = new GenericRepository<Discount>(db);
                }
                return _discountRepository;
            }
        }

        public GenericRepository<Image> ImageRepository
        {
            get
            {

                if (this._imageRepository == null)
                {
                    this._imageRepository = new GenericRepository<Image>(db);
                }
                return _imageRepository;
            }
        }

        public GenericRepository<Product> ProductRepository
        {
            get
            {

                if (this._productRepository == null)
                {
                    this._productRepository = new GenericRepository<Product>(db);
                }
                return _productRepository;
            }
        }
        public GenericRepository<ProductDescription> ProductDescriptionRepository
        {
            get
            {

                if (this._productDescriptionRepository == null)
                {
                    this._productDescriptionRepository = new GenericRepository<ProductDescription>(db);
                }
                return _productDescriptionRepository;
            }
        }
        public GenericRepository<ProductAttributeValue> ProductAttributeValueRepository
        {
            get
            {
                if (this._productAttributeValueRepository == null)
                {
                    this._productAttributeValueRepository = new GenericRepository<ProductAttributeValue>(db);
                }
                return _productAttributeValueRepository;
            }
        }
        public GenericRepository<ProductRate> ProductRateRepository
        {
            get
            {
                if (this._productRateRepository == null)
                {
                    this._productRateRepository = new GenericRepository<ProductRate>(db);
                }
                return _productRateRepository;
            }
        }
        public GenericRepository<Tag> TagRepository
        {
            get
            {
                if (this._tagRepository == null)
                {
                    this._tagRepository = new GenericRepository<Tag>(db);
                }
                return _tagRepository;
            }
        }
        //public GenericRepository<TagDescription> TagDescriptionRepository
        //{
        //    get
        //    {
        //        if (this._tagDescriptionRepository == null)
        //        {
        //            this._tagDescriptionRepository = new GenericRepository<TagDescription>(db);
        //        }
        //        return _tagDescriptionRepository;
        //    }
        //}
        public GenericRepository<ProductSKU> ProductSKURepository
        {
            get
            {
                if (this._productSKURepository == null)
                {
                    this._productSKURepository = new GenericRepository<ProductSKU>(db);
                }
                return _productSKURepository;
            }
        }
        public GenericRepository<ProductSKUOptionValue> ProductSKUOptionValueRepository
        {
            get
            {
                if (this._productSKUOptionValueRepository  == null)
                {
                    this._productSKUOptionValueRepository = new GenericRepository<ProductSKUOptionValue>(db);
                }
                return _productSKUOptionValueRepository;
            }
        }
        public GenericRepository<Option> OptionRepository
        {
            get
            {
                if (this._optionRepository == null)
                {
                    this._optionRepository = new GenericRepository<Option>(db);
                }
                return _optionRepository;
            }
        }
        public GenericRepository<OptionValue> OptionValueRepository
        {
            get
            {
                if (this._optionValueRepository == null)
                {
                    this._optionValueRepository = new GenericRepository<OptionValue>(db);
                }
                return _optionValueRepository;
            }
        }
        public GenericRepository<OptionDescription> OptionDescriptionRepository
        {
            get
            {
                if (this._optionDescriptionRepository == null)
                {
                    this._optionDescriptionRepository = new GenericRepository<OptionDescription>(db);
                }
                return _optionDescriptionRepository;
            }
        }
        public GenericRepository<OptionValueDescription> OptionValueDescriptionRepository
        {
            get
            {
                if (this._optionValueDescriptionRepository == null)
                {
                    this._optionValueDescriptionRepository = new GenericRepository<OptionValueDescription>(db);
                }
                return _optionValueDescriptionRepository;
            }
        }
        public GenericRepository<Coupon> CouponRepository
        {
            get
            {
                if (this._couponRepository == null)
                {
                    this._couponRepository = new GenericRepository<Coupon>(db);
                }
                return _couponRepository;
            }
        }
        public GenericRepository<Address> AddressRepository
        {
            get
            {
                if (this._addressRepository == null)
                {
                    this._addressRepository = new GenericRepository<Address>(db);
                }
                return _addressRepository;
            }
        }
        public GenericRepository<Order> OrderRepository
        {
            get
            {
                if (this._orderRepository == null)
                {
                    this._orderRepository = new GenericRepository<Order>(db);
                }
                return _orderRepository;
            }
        }
        public GenericRepository<OrderItem> OrderItemRepository
        {
            get
            {
                if (this._orderItemRepository == null)
                {
                    this._orderItemRepository = new GenericRepository<OrderItem>(db);
                }
                return _orderItemRepository;
            }
        }
        public GenericRepository<ShippingAddress> ShippingAddressRepository
        {
            get
            {
                if (this._shippingAddressRepository == null)
                {
                    this._shippingAddressRepository = new GenericRepository<ShippingAddress>(db);
                }
                return _shippingAddressRepository;
            }
        }
        public GenericRepository<BillingAddress> BillingAddressRepository
        {
            get
            {
                if (this._billingAddressRepository == null)
                {
                    this._billingAddressRepository = new GenericRepository<BillingAddress>(db);
                }
                return _billingAddressRepository;
            }
        }
        public GenericRepository<ShippingCompany> ShippingCompanyRepository
        {
            get
            {
                if (this._shippingCompanyRepository == null)
                {
                    this._shippingCompanyRepository = new GenericRepository<ShippingCompany>(db);
                }
                return _shippingCompanyRepository;
            }
        }
        public GenericRepository<ShippingCompanyDescription> ShippingCompanyDescriptionRepository
        {
            get
            {
                if (this._shippingCompanyDescriptionRepository == null)
                {
                    this._shippingCompanyDescriptionRepository = new GenericRepository<ShippingCompanyDescription>(db);
                }
                return _shippingCompanyDescriptionRepository;
            }
        }
        public GenericRepository<ShippingBillingMethodDescription> ShippingBillingMethodDescriptionRepository
        {
            get
            {
                if (this._shippingBillingMethodDescriptionRepository == null)
                {
                    this._shippingBillingMethodDescriptionRepository = new GenericRepository<ShippingBillingMethodDescription>(db);
                }
                return _shippingBillingMethodDescriptionRepository;
            }
        }
        public GenericRepository<ShippingBillingMethod> ShippingBillingMethodRepository
        {
            get
            {
                if (this._shippingBillingMethodRepository == null)
                {
                    this._shippingBillingMethodRepository = new GenericRepository<ShippingBillingMethod>(db);
                }
                return _shippingBillingMethodRepository;
            }
        }
        public GenericRepository<OrderHistory> OrderHistoryRepository
        {
            get
            {
                if (this._orderHistoryRepository == null)
                {
                    this._orderHistoryRepository = new GenericRepository<OrderHistory>(db);
                }
                return _orderHistoryRepository;
            }
        }
        public GenericRepository<CheckOut> CheckOutRepository
        {
            get
            {
                if (this._checkOutRepository == null)
                {
                    this._checkOutRepository = new GenericRepository<CheckOut>(db);
                }
                return _checkOutRepository;
            }
        }
        public GenericRepository<OptionHelper> OptionHelperRepository
        {
            get
            {
                if (this._optionHelperRepository == null)
                {
                    this._optionHelperRepository = new GenericRepository<OptionHelper>(db);
                }
                return _optionHelperRepository;
            }
        }
        public GenericRepository<SizeHelper> SizeHelperRepository
        {
            get
            {
                if (this._sizeHelperRepository == null)
                {
                    this._sizeHelperRepository = new GenericRepository<SizeHelper>(db);
                }
                return _sizeHelperRepository;
            }
        }
        public GenericRepository<ColorHelper> ColorHelperRepository
        {
            get
            {
                if (this._colorHelperRepository == null)
                {
                    this._colorHelperRepository = new GenericRepository<ColorHelper>(db);
                }
                return _colorHelperRepository;
            }
        }
        public GenericRepository<AttributeGroup> AttributeGroupRepsitory
        {
            get
            {
                if (this._attributeGroupRepository == null)
                {
                    this._attributeGroupRepository = new GenericRepository<AttributeGroup>(db);
                }
                return _attributeGroupRepository;
            }
        }
        public GenericRepository<AttributeGroupDescription> AttributeGroupDescriptionRepsitory
        {
            get
            {
                if (this._attributeGroupDescriptionRepository == null)
                {
                    this._attributeGroupDescriptionRepository = new GenericRepository<AttributeGroupDescription>(db);
                }
                return _attributeGroupDescriptionRepository;
            }
        }

        public GenericRepository<DesignerDescription> DesignerDescriptionRepository
        {
            get
            {
                if (this._designerDescriptionRepository == null)
                {
                    this._designerDescriptionRepository = new GenericRepository<DesignerDescription>(db);
                }
                return _designerDescriptionRepository;
            }
        }
        public GenericRepository<SizeAttribute> SizeAttributeRepository
        {
            get
            {
                if (this._sizeAttributeRepository == null)
                {
                    this._sizeAttributeRepository = new GenericRepository<SizeAttribute>(db);
                }
                return _sizeAttributeRepository;
            }
        }
        public GenericRepository<SizeAttributeDescription> SizeAttributeDescriptionRepository
        {
            get
            {
                if (this._sizeAttributeDescriptionRepository == null)
                {
                    this._sizeAttributeDescriptionRepository = new GenericRepository<SizeAttributeDescription>(db);
                }
                return _sizeAttributeDescriptionRepository;
            }
        }
        public GenericRepository<ProductSizeAttribute> ProductSizeAttributeRepository
        {
            get
            {
                if (this._productSizeAttributeRepository == null)
                {
                    this._productSizeAttributeRepository = new GenericRepository<ProductSizeAttribute>(db);
                }
                return _productSizeAttributeRepository;
            }
        }
        public GenericRepository<DiscountDescription> DiscountDescriptionRepository
        {
            get
            {
                if (this._discountDescriptionRepository == null)
                {
                    this._discountDescriptionRepository = new GenericRepository<DiscountDescription>(db);
                }
                return _discountDescriptionRepository;
            }
        }

        public GenericRepository<ProductDiscount> ProductDiscountRepository
        {
            get
            {
                if (this._productDiscountRepository == null)
                {
                    this._productDiscountRepository = new GenericRepository<ProductDiscount>(db);
                }
                return _productDiscountRepository;
            }
        }
        public GenericRepository<Slide> SlideRepository
        {
            get
            {
                if (this._SlideRepository == null)
                {
                    this._SlideRepository = new GenericRepository<Slide>(db);
                }
                return _SlideRepository;
            }
        }
        //public GenericRepository<SizeAttributeMedian> SizeAttributeMedianRepository
        //{
        //    get
        //    {
        //        if (this._sizeAttributeMedianRepository == null)
        //        {
        //            this._sizeAttributeMedianRepository = new GenericRepository<SizeAttributeMedian>(db);
        //        }
        //        return _sizeAttributeMedianRepository;
        //    }
        //}
        #endregion

        public async Task SaveAsync()
        {
            await db.SaveChangesAsync();
        }

        public void Save()
        {
            db.SaveChanges();
        }

        #region IDisposable
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }
        #endregion

    }
}
