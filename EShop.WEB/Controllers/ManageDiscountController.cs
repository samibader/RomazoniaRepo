using AutoMapper;
using EShop.BLL.DTO;
using EShop.BLL.Infrastructure;
using EShop.BLL.Interfaces;
using EShop.Common;
using EShop.WEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;

namespace EShop.WEB.Controllers
{
    [Authorize(Roles = "AdminRole")]
    public class ManageDiscountController : BaseController
    {
        private readonly IDiscount _discountService;

        public ManageDiscountController(IDiscount _discountService)
        {
            this._discountService = _discountService;
        }
        //
        // GET: /ManageDiscount/
        public ActionResult Index(String an, String en,bool? per, int PageSize = PAGE_SIZE, int page = 1, Sorts sb = Sorts.IdUp, Int32 Id = 0)
        {
            //List<OptionManagerDTO> optionsDTOs = _productManagerService.GetAllOptions();
            List<DiscountDTO> discountsDTOs = _discountService.Filter(an, en,per,Id, sb,CurrentLanguage,CurrentCurrency);
            DiscountIndexVM discounts = new DiscountIndexVM
            {
                Discounts = discountsDTOs.ToPagedList(page, PageSize),
                filters = new DiscountFiltersVM
                {
                    ArabicName = an,
                    EnglishName = en,
                    PageNum = page,
                    PageSize = PageSize,
                    Id = Id,
                    IsPercentage = per,
                    SortBy = sb
                }
            };
       
            return View(discounts);
        }
        
        [HttpPost]
        public ActionResult Index(String an, String en, bool? per, int PageSize = PAGE_SIZE, int page = 1, Int32 Id = 0,Sorts sb = Sorts.IdUp)
        {
            //List<OptionManagerDTO> optionsDTOs = _productManagerService.GetAllOptions();
            List<DiscountDTO> discountsDTOs = _discountService.Filter(an, en, per, Id, sb, CurrentLanguage, CurrentCurrency);
            DiscountIndexVM discounts = new DiscountIndexVM
            {
                Discounts = discountsDTOs.ToPagedList(page, PageSize),
                filters = new DiscountFiltersVM
                {
                    ArabicName = an,
                    EnglishName = en,
                    PageNum = page,
                    PageSize = PageSize,
                    Id = Id,
                    IsPercentage = per,
                    SortBy = sb
                }
            };

            return View(discounts);
        }
        public ActionResult Add()
        {
            DiscountVM discount = new DiscountVM();
            return View(discount);
        }

        [HttpPost]
        public ActionResult Add(DiscountVM discount)
        {
            if (ModelState.IsValid)
            {
                Mapper.Initialize(c => c.CreateMap<DiscountVM, DiscountDTO>());
                DiscountDTO dto = Mapper.Map<DiscountVM, DiscountDTO>(discount);

                OperationDetails op = _discountService.AddDiscount(dto);
                return Json(new { Succedeed = op.Succedeed, message = op.Message, prop = op.Property });
            }
            else
                return View(discount);
        }

        public ActionResult Edit(long Id)
        {
            DiscountDTO discountDto = _discountService.GetDiscount(Id, CurrentLanguage, CurrentCurrency);
            Mapper.Initialize(c => c.CreateMap<DiscountDTO, DiscountVM>());
            DiscountVM discountVM = Mapper.Map<DiscountDTO, DiscountVM>(discountDto);

            return View(discountVM);
        }

        [HttpPost]
        public ActionResult Edit(DiscountVM discount)
        {
            if (ModelState.IsValid)
            {
                Mapper.Initialize(c => c.CreateMap<DiscountVM, DiscountDTO>());
                DiscountDTO dto = Mapper.Map<DiscountVM, DiscountDTO>(discount);

                OperationDetails op = _discountService.EditDiscount(dto);
                return Json(new { Succedeed = op.Succedeed, message = op.Message, prop = op.Property });
            }
            else
                return View(discount);
        }

        [HttpPost]
        public JsonResult DeleteSkuFromDiscount(long skuId, long DiscountId)
        {
            OperationDetails op = _discountService.DeleteSkuFromDiscount(skuId, DiscountId);
            return Json(new { Succedeed = op.Succedeed, message = op.Message, prop = op.Property });
        }

        [HttpPost]
        public ActionResult GetProductSkusForDiscount(long ProductId)
        {

           List<ProductDTO> fullSkus = _discountService.GetProductSKUs(ProductId, CurrentLanguage, CurrentCurrency);
           Mapper.Initialize(c => c.CreateMap<ProductDTO,ProductSKUDTO>());
           List<ProductSKUDTO> skus = Mapper.Map<List<ProductDTO>, List<ProductSKUDTO>>(fullSkus);
           return Json(skus);
        }
         [HttpPost]
        public JsonResult AddDiscountToSkus(List<long> SkuIds,long discountId)
        {
             OperationDetails op = _discountService.AddDiscountToSkus(SkuIds, discountId);
             return Json(new { Succedeed = op.Succedeed, message = op.Message, prop = op.Property });
             
        }

         public JsonResult Delete(long Id)
         {
             OperationDetails op = _discountService.DeleteDiscount(Id);
             return Json(new { Succedeed = op.Succedeed, message = op.Message, prop = op.Property });
         }
	}
}