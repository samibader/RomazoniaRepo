using AutoMapper;
using EShop.BLL.DTO;
using EShop.BLL.Infrastructure;
using EShop.BLL.Interfaces;
using EShop.WEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using EShop.Resources.Views;
using EShop.WEB.Simulation_Data;
using MvcFlashMessages;
using Newtonsoft.Json;
using EShop.Common;

namespace EShop.WEB.Controllers
{
    public class HomeController : BaseController
    {
        ISampleService sampleService;
        private ICategoryService _categoryService;
        private IProductService _productService;
        private ISliderService _sliderService;
        public HomeController(ISampleService serv, ICategoryService categoryService, IProductService productService,ISliderService sliderService)
        {
            sampleService = serv;
            _categoryService = categoryService;
            _productService = productService;
            _sliderService = sliderService;
        }

        public ViewResult Test()
        {
            ViewBag.asda = "sss";
            return View();
        }

        public ViewResult Index()
        {
            //IEnumerable<SampleDTO> sampleDtos = sampleService.GetAll();
            //Mapper.Initialize(cfg => cfg.CreateMap<SampleDTO, SampleViewModel>());
            //var samples = Mapper.Map<IEnumerable<SampleDTO>, List<SampleViewModel>>(sampleDtos);
            //return View(samples);
            ViewBag.Title =EShopResource.Home;
            return View();
        }

        public PartialViewResult GetRandomCategory()
        {
            var randomCatDto = _categoryService.GetRandomCategory(CurrentLanguage, CurrentWebsite);
            if (randomCatDto == null)
                return PartialView("_BlankPartial");
            Mapper.Initialize(cfg => cfg.CreateMap<CategoryMenuDTO, CategoryMenuVM>());

            CategoryMenuVM vm = Mapper.Map<CategoryMenuDTO, CategoryMenuVM>(randomCatDto);
            return PartialView("_RandomCategory", vm);
        }
        public PartialViewResult GetLeattestCategory()
        {
            var LeattestCatDto = _categoryService.GetLatestCategory(CurrentLanguage,CurrentWebsite);
            if (LeattestCatDto == null)
            {
                return PartialView("_BlankPartial");
            }
            Mapper.Initialize(cfg => cfg.CreateMap<CategoryMenuDTO, CategoryMenuVM>());

            CategoryMenuVM vm = Mapper.Map<CategoryMenuDTO, CategoryMenuVM>(LeattestCatDto);
            return PartialView("_RandomCategory", vm);
        }
        public PartialViewResult GetMainPageProducts()
        {
            var result = _productService.GetMainPageProducts(CurrentLanguage, CurrentCurrency,CurrentWebsite);
            if (result == null)
                return PartialView("_BlankPartial");
            return PartialView("_MainPageProducts", result);
        }

        public ActionResult AddSample()
        {
            return View();
        }

        public ActionResult DisplaySample(long? id)
        {
            try
            {
                SampleDTO sample = sampleService.GetSample(id);
                Mapper.Initialize(cfg => cfg.CreateMap<SampleDTO, SampleViewModel>());

                var model = Mapper.Map<SampleDTO, SampleViewModel>(sample);
                return View(model);
            }
            catch (ValidationException ex)
            {
                return Content(ex.Message);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddSample(SampleViewModel model)
        {
            try
            {
                Mapper.Initialize(cfg => cfg.CreateMap<SampleViewModel, SampleDTO>());
                var sampleDto = Mapper.Map<SampleViewModel, SampleDTO>(model);
                OperationDetails result = sampleService.AddSample(sampleDto);
                if (result.Succedeed)
                    return RedirectToAction("Index");
                else
                    ModelState.AddModelError(result.Property, result.Message);
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError(ex.Property, ex.Message);
            }

            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        private async Task<bool> RecaptchaIsValid(string captchaResponse)
        {
            var requestUrl =
                String.Format(
                    "https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}&remoteip={2}",
                    "6Lf3RhQUAAAAAPuuQvw6vOFv4qHcAffVffM5EuW1",
                    captchaResponse,
                    Request.UserHostAddress);
            string result;
            using (var client = new HttpClient())
            {
                result = await client.GetStringAsync(requestUrl);
            }
            if (!String.IsNullOrWhiteSpace(result))
            {
                var obj = JsonConvert.DeserializeObject<RecaptchaResponse>(result);
                if (obj.Success)
                {
                    return true;
                }
            }
            return false;
        }
        private class RecaptchaResponse
        {
            public bool Success { get; set; }
            [JsonProperty("error-codes")]
            public ICollection<string> ErrorCodes { get; set; }
            public RecaptchaResponse()
            {
                ErrorCodes = new HashSet<string>();
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Contact(ContactUsVM model)
        {
            if (ModelState.IsValid)
            {
                if (await RecaptchaIsValid(Request.Form["g-recaptcha-response"]))
                {
                    //do your processing here
                    var body = "<p>Email From: {0} ({1})</p><p>Phone:</p><p>{2}</p><p>Website:</p><p>{3}</p><p>Message:</p><p>{4}</p>";
                    var message = new MailMessage();
                    message.To.Add(new MailAddress("info@romazonia.com")); //replace with valid value
                    message.Subject = "رسالة من صفحة اتصل بنا";
                    message.Body = string.Format(body, model.Name, model.Email, model.Phone, model.Website, model.ContactMessage);
                    message.IsBodyHtml = true;
                    using (var smtp = new SmtpClient())
                    {
                        await smtp.SendMailAsync(message);
                        ViewBag.flag = true;
                        return View();
                    }

                }
                this.Flash("error", "لم يتم إرسال الرسالة بنجاح");
                ModelState.AddModelError(
                    "invalid-recaptcha-response",
                    "قم بالنقر على كود التحقق من فضلك");
            }
            return View();
        }
        
        public PartialViewResult MenuArea()
        {
            //MenuCategoryDTO m = new MenuCategoryDTO();
            //m = m.Create(3);
            //Mapper.Initialize(cfg => cfg.CreateMap<MenuCategoryDTO, MenuCategoryVM>());
            //MenuCategoryVM vm = Mapper.Map<MenuCategoryDTO, MenuCategoryVM>(m);
            CategoryMenuDTO menuDto = _categoryService.GetAll(CurrentLanguage, CurrentWebsite);

            Mapper.Initialize(cfg =>cfg.CreateMap<CategoryMenuDTO, CategoryMenuVM>()
                );

            CategoryMenuVM vm = Mapper.Map<CategoryMenuDTO, CategoryMenuVM>(menuDto);
            return PartialView("_MenuArea", vm);
        }
        //public PartialViewResult HeaderArea()
        //{
        //    MenuCategoryDTO m = new MenuCategoryDTO();
        //    m = m.Create(3);
        //    Mapper.Initialize(cfg => cfg.CreateMap<MenuCategoryDTO, CategoryMenuVM>());
        //    CategoryMenuVM vm = Mapper.Map<MenuCategoryDTO, CategoryMenuVM>(m);
        //    return PartialView("_Header", vm);
        //}
        protected override void Dispose(bool disposing)
        {
            sampleService.Dispose();
            base.Dispose(disposing);
        }
        public ActionResult ChangeLang(string lang, string returnUrl)
        {
            //var langCookie = new HttpCookie("locale", lang) { HttpOnly = true };
            //Response.AppendCookie(langCookie);
            var oldLang = lang.ToLower() == "ar-sa" ? "en-us" : "ar-sa";
            var newUrl = returnUrl.ToLower().Replace(oldLang, lang);
            return Redirect(HttpUtility.UrlDecode(newUrl));
        }
        public ActionResult ChangeCurrency(string currency,string returnUrl)
        {
            var currencyCookie = new HttpCookie("Currency", currency) { HttpOnly = true };
            Response.AppendCookie(currencyCookie);
            return Redirect(HttpUtility.UrlDecode(returnUrl));
        }
        public ActionResult ChangeWebsite(string website, string returnUrl)
        {
            //var websiteCookie = new HttpCookie("website", website) { HttpOnly = true };
            //Response.AppendCookie(websiteCookie);
            return RedirectToAction("Index");
        }
        public ActionResult Header()
        {
            return PartialView("_Header");
        }

        public ActionResult WebsiteSlider()
        {
            var vm = _sliderService.GetSlidesByWebSite(CurrentWebsite);
            Mapper.Initialize(c => c.CreateMap<SlideDTO, SlideVM>());
            List<SlideVM> slides = Mapper.Map<List<SlideDTO>, List<SlideVM>>(vm);
            return PartialView("_WebsiteSlider", slides);
        }
    }
}