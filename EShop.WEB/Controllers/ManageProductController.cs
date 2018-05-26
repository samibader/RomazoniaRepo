using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EShop.BLL.Interfaces;
using EShop.BLL.Services;
using EShop.BLL.DTO;
using EShop.WEB.Models;
using AutoMapper;
using EShop.BLL.Infrastructure;
using EShop.Common;
using PagedList.Mvc;
using PagedList;
using System.Web.Script.Serialization;


namespace EShop.WEB.Controllers
{

    [Authorize(Roles = "AdminRole")]
    public class ManageProductController : BaseController
    {
        private IProductManagerService _productManagerService;
        private IProductService _productService;
        private IManageDesigner _DesignerService;
        private IManageAttributeGroup _attributeGroupService;
        private IManageSizeAttributes _manageSizeAttributesService;
        private ICategoryService _categoryService;

        public ManageProductController(IProductManagerService _productManagerService, IProductService _productService, IManageDesigner _DesignerService, IManageAttributeGroup _attributeGroupService, IManageSizeAttributes _manageSizeAttributesService, ICategoryService categoryService)
        {
            this._productManagerService = _productManagerService;
            this._productService = _productService;
            this._DesignerService = _DesignerService;
            this._attributeGroupService = _attributeGroupService;
            this._manageSizeAttributesService = _manageSizeAttributesService;
            this._categoryService = categoryService;
        }
        //
        // GET: /ManageProduct/
        public ActionResult Index(String an, String en, Boolean? s, Double? sp, Double? ep, long Id = 0, int PageSize = PAGE_SIZE, int page = 1, Sorts sb = Sorts.IdUp)
        {
            page = page > 1 ? page : 1;
            PageSize = PageSize > 0 ? PageSize : PAGE_SIZE;
            List<ProductIndexManagerDTO> productDTOs = _productManagerService.FilterProducts(an, en, sp, ep, s, Id, sb, CurrentLanguage, CurrentCurrency, CurrentWebsite);
            Mapper.Initialize(c => c.CreateMap<ProductIndexManagerDTO, ProductIndexManagerVM>());
            List<ProductIndexManagerVM> products = Mapper.Map<List<ProductIndexManagerDTO>, List<ProductIndexManagerVM>>(productDTOs);
            ProductIndexVM productIndexVM = new ProductIndexVM();

            productIndexVM.Products = products.ToPagedList(page, PageSize);
            productIndexVM.SizeCategories = _productManagerService.GetSizeCategories(CurrentLanguage);
            productIndexVM.filters = new ProductManagerFiltersVM
            {
                ArabicName = an,
                EnglishName = en,
                StartPrice = sp,
                EndPrice = ep,
                PageNum = page,
                PageSize = PageSize,
                SortBy = sb,
                Status = s

            };

            return View(productIndexVM);
        }

        [HttpPost]
        public ActionResult Index(String en, String an, Double? sp, Boolean? s, Double? ep, long Id = 0, int PageSize = PAGE_SIZE, int page = 1, Sorts sb = Sorts.IdUp)
        {


            //page = page > 1 ? page : 1;
            //PageSize = PageSize > 0 ? PageSize : PAGE_SIZE;
            List<ProductIndexManagerDTO> productDTOs = _productManagerService.FilterProducts(an, en, sp, ep, s, Id, sb, CurrentLanguage, CurrentCurrency, CurrentWebsite);
            Mapper.Initialize(c => c.CreateMap<ProductIndexManagerDTO, ProductIndexManagerVM>());
            List<ProductIndexManagerVM> products = Mapper.Map<List<ProductIndexManagerDTO>, List<ProductIndexManagerVM>>(productDTOs);
            ProductIndexVM productIndexVM = new ProductIndexVM
            {
                Products = products.ToPagedList(page, PageSize),
                SizeCategories = _productManagerService.GetSizeCategories(CurrentLanguage),
                filters = new ProductManagerFiltersVM
                {
                    ArabicName = an,
                    EnglishName = en,
                    StartPrice = sp,
                    EndPrice = ep,
                    PageNum = page,
                    PageSize = PageSize,
                    Status = s,
                    SortBy = sb

                }
            };

            return View(productIndexVM);
        }

        public ActionResult Add(int choice, long? Id)
        {
            //  bool? color, bool? size,long? sizeCategoryId
            AddProductVM product = new AddProductVM();
            product.SizeCategoryId = Id;
            product.Colors = _productManagerService.GetAllColors();
            product.Sizes = _productManagerService.GetSizesOfCat(Id);
            product.Designers = _DesignerService.GetAllDesigners(CurrentLanguage);
            
            //product.BaseCategories = _productManagerService.GetSubCategories((long)CurrentWebsite, CurrentLanguage);
            product.BaseCategories = _categoryService.GetAllPathsByWebsite(CurrentWebsite, CurrentLanguage);
            // List<DesignerDTO> designers = _DesignerService.GetAllDesigners(CurrentLanguage);
            // product.Designers = designers;

            List<AttributeGroupDTO> attribueGroups = _attributeGroupService.GetAllAttributeGroups(CurrentLanguage);
            product.AttributesGroups = attribueGroups;
            if (choice == 1 || choice == 3)
                product.HasColor = true;
            else
                product.HasColor = false;

            if (choice == 2 || choice == 3)
                product.HasSize = true;
            else
                product.HasSize = false;
            if (product.HasSize)
                product.AllSizeAttributes = _manageSizeAttributesService.GetSizeAttributesBySizeCaategoryId(Id.Value, CurrentLanguage);
            return View(product);
        }

        [HttpPost]
        public ActionResult Add(AddProductVM product)
        {
            // To exclude the size or color from validation when the product has only color or only size
            if (product.HasSize || product.HasColor)
            {
                if (!product.HasColor)
                {
                    for (int i = 0; i < product.ProductOptionValues.Count; i++)
                    {
                        this.ModelState.Remove("ProductOptionValues[" + i + "].ColorValueId");
                    }

                }
                if (!product.HasSize)
                {
                    for (int i = 0; i < product.ProductOptionValues.Count; i++)
                    {
                        this.ModelState.Remove("ProductOptionValues[" + i + "].SizeValueId");
                    }

                }
            }
            //////////////

            if (ModelState.IsValid)
            {
                Mapper.Initialize(c => c.CreateMap<AddProductVM, AddProductDTO>());
                AddProductDTO productdto = Mapper.Map<AddProductVM, AddProductDTO>(product);
                OperationDetails op = _productManagerService.AddProduct(productdto);


                return Json(new { Succedeed = op.Succedeed, message = op.Message, prop = op.Property });
            }
            else
            {
                var errors = ModelState.Select(x => x.Value.Errors)
                           .Where(y => y.Count > 0)
                           .ToList();
                product.Colors = _productManagerService.GetAllColors();
                product.Sizes = _productManagerService.GetSizesOfCat(product.SizeCategoryId);
                product.Designers = _DesignerService.GetAllDesigners(CurrentLanguage);
                //product.BaseCategories = _productManagerService.GetSubCategories((long)CurrentWebsite, CurrentLanguage);
                product.BaseCategories = _categoryService.GetAllPathsByWebsite(CurrentWebsite, CurrentLanguage);
                List<AttributeGroupDTO> attribueGroups = _attributeGroupService.GetAllAttributeGroups(CurrentLanguage);
                product.AttributesGroups = attribueGroups;
                if (product.HasSize)
                    product.AllSizeAttributes = _manageSizeAttributesService.GetSizeAttributesBySizeCaategoryId(product.SizeCategoryId.Value, CurrentLanguage);
                return View(product);
            }
        }

        public JsonResult GetTags(string query)
        {
            List<TagDTO> tags = _productManagerService.GetTagByTerm(query);
            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(tags);

            return Json(json, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Edit(long Id)
        {
            AddProductDTO product = _productManagerService.GetEditProduct(Id, CurrentLanguage);
            product.Colors = _productManagerService.GetAllColors();
            product.Sizes = _productManagerService.GetSizesOfCat(product.SizeCategoryId);
            product.Designers = _DesignerService.GetAllDesigners(CurrentLanguage);
           
            List<AttributeGroupDTO> attribueGroups = _attributeGroupService.GetAllAttributeGroups(CurrentLanguage);
            product.AttributesGroups = attribueGroups;

            if (product.HasSize)
            {
                product.AllSizeAttributes = _manageSizeAttributesService.GetSizeAttributesBySizeCaategoryId(product.SizeCategoryId.Value, CurrentLanguage);
            }
            Mapper.Initialize(c => c.CreateMap<AddProductDTO, AddProductVM>());
            AddProductVM productVM = Mapper.Map<AddProductDTO, AddProductVM>(product);

            productVM.BaseCategories = _categoryService.GetAllPathsByWebsite(CurrentWebsite, CurrentLanguage);
            return View(productVM);
        }

        [HttpPost]
        public ActionResult Edit(AddProductVM product)
        {
            Mapper.Initialize(c => c.CreateMap<AddProductVM, AddProductDTO>());
            AddProductDTO productDTO = Mapper.Map<AddProductVM, AddProductDTO>(product);
            OperationDetails op = _productManagerService.EditProduct(productDTO.Id, productDTO);
            return Json(new { Succedeed = op.Succedeed, message = op.Message, prop = op.Property });

        }

        public JsonResult GetSubCategories(long Id)
        {
            List<CategoryDTO> categories = _productManagerService.GetSubCategories(Id, CurrentLanguage);
            SelectList subCategories = new SelectList(categories, "Id", "Name", 0);
            return Json(subCategories);
        }
        [HttpPost]
        public ActionResult Delete(long Id)
        {
            OperationDetails op = _productManagerService.DeleteProduct(Id);
            return Json(new { Succedeed = op.Succedeed, message = op.Message, prop = op.Property });
        }


        public ActionResult TestValidation()
        {
            var vm = new TestValidationVM();
            return View(vm);
        }

        [HttpPost]
        public ActionResult TestValidation(TestValidationVM vm)
        {
            if (ModelState.IsValid)
            {
                return Content("model is valid");
            }
            else
            {
                return View(vm);
            }
        }
    }
}