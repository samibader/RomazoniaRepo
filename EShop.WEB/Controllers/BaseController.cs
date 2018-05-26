using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EShop.Common;
using MvcFlashMessages;
using EShop.WEB.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.IO;
using EShop.BLL.Infrastructure;

namespace EShop.WEB.Controllers
{
    public class BaseController : Controller
    {
        private ApplicationUserManager _userManager;
        public const int PAGE_SIZE = 9;
        public BaseController()
        {
            
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        //protected override ViewResult View(IView view, object model)
        //{
        //    if (User.Identity.IsAuthenticated)
        //        this.ViewBag.FullName = UserManager.FindById(User.Identity.GetUserId()).FirstName + " " + UserManager.FindById(User.Identity.GetUserId()).LastName;
        //    else
        //        this.ViewBag.FullName = "jk";
        //    return base.View(view, model);
        //}

        protected override PartialViewResult PartialView(string viewName, object model)
        {
            if (User.Identity.IsAuthenticated)
                this.ViewBag.FullName = UserManager.FindById(User.Identity.GetUserId()).FirstName + " " + UserManager.FindById(User.Identity.GetUserId()).LastName;
            else
                this.ViewBag.FullName = "jk";
            return base.PartialView(viewName, model);
        }

        public Currency CurrentCurrency
        {
            get
            {
                var cookieLocale = HttpContext.Request.Cookies["Currency"];
                if (cookieLocale == null)
                {
                     cookieLocale = new HttpCookie("Currency", Currency.Rial.ToString()) {HttpOnly = true};

                    Response.AppendCookie(cookieLocale);
                }
               
                return cookieLocale.Values[0].ToLower()==Currency.Rial.ToString().ToLower()?Currency.Rial : Currency.Dollar;
            }
        }
        public WebSites CurrentWebsite
        {
            get
            {
                var cookieLocale = HttpContext.Response.Cookies["website"];
                if (cookieLocale == null)
                {
                    cookieLocale = new HttpCookie("website", WebSites.Mobile.ToString()) { HttpOnly = true };

                    Response.AppendCookie(cookieLocale);
                }

                return cookieLocale.Values[0].ToLower() == WebSites.Fashion.ToString().ToLower() ? WebSites.Fashion : cookieLocale.Values[0].ToLower() == WebSites.Gym.ToString().ToLower() ? WebSites.Gym : WebSites.Mobile;
            }
        }
        
        public Langs CurrentLanguage
        {
            get
            {
                var cookieLocale = HttpContext.Response.Cookies["locale"];
                if (cookieLocale == null)
                    return Langs.Arabic;
                if (cookieLocale.Values[0].ToLower() == "ar-sa")
                    return Langs.Arabic;
                return Langs.English;

            }
        }

        public void Success(string message)
        {
            AddAlert(AlertStyles.Success, message);
        }

        public void Information(string message)
        {
            AddAlert(AlertStyles.Information, message);
        }

        public void Warning(string message)
        {
            AddAlert(AlertStyles.Warning, message);
        }
        public void Danger(string message)
        {
            AddAlert(AlertStyles.Danger, message);
        }
        private void AddAlert(string alertStyle, string message)
        {
            var alerts = TempData.ContainsKey(Alert.TempDataKey)
                ? (List<Alert>)TempData[Alert.TempDataKey]
                : new List<Alert>();

            alerts.Add(new Alert
            {
                AlertStyle = alertStyle,
                Message = message,
            });

            TempData[Alert.TempDataKey] = alerts;
        }
        [HttpPost]
        public ActionResult UploadFiles()
        {
            string fileName = "";

            // Checking no of files injected in Request object  
            if (Request.Files.Count > 0)
            {
                try
                {
                    //  Get all files from Request object  
                    HttpFileCollectionBase files = Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        //string path = AppDomain.CurrentDomain.BaseDirectory + "Uploads/";  
                        //string filename = Path.GetFileName(Request.Files[i].FileName);  

                        HttpPostedFileBase file = files[i];
                        string fname;

                        // Checking for Internet Explorer  
                        if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                        {
                            string[] testfiles = file.FileName.Split(new char[] { '\\' });
                            fname = testfiles[testfiles.Length - 1];
                            fileName = fname;
                        }
                        else
                        {
                            fname = file.FileName;
                            var fileExtension = Path.GetExtension(fname).ToLower();
                            Guid g = Guid.NewGuid();
                            fname = g.ToString() + fileExtension;
                            fileName = fname;
                           
                        }

                        // Get the complete folder path and store the file inside it.  
                        fname = Path.Combine(Server.MapPath("~/uploads/"), fname);
                        file.SaveAs(fname);
                    }
                    // Returns message that successfully uploaded  
                    return Json(new { message = "File Uploaded Successfully!", name = fileName });
                }
                catch (Exception ex)
                {
                    return Json(new { message = "Error occurred. Error details: " + ex.Message, name = "" });
                }
            }
            else
            {
                return Json(new { message = "No files selected.", name = "" });
            }
        }

        [HttpPost]
        public ActionResult UploadFilesMultiple()
        {
            string fileName = "";
            List<string> filesNames = new List<string>();
            // Checking no of files injected in Request object  
            if (Request.Files.Count > 0)
            {
                try
                {
                    //  Get all files from Request object  
                    HttpFileCollectionBase files = Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        //string path = AppDomain.CurrentDomain.BaseDirectory + "Uploads/";  
                        //string filename = Path.GetFileName(Request.Files[i].FileName);  

                        HttpPostedFileBase file = files[i];
                        string fname;

                        // Checking for Internet Explorer  
                        if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                        {
                            string[] testfiles = file.FileName.Split(new char[] { '\\' });
                            fname = testfiles[testfiles.Length - 1];
                            fileName = fname;
                            filesNames.Add(fname);
                        }
                        else
                        {
                            fname = file.FileName;
                            var fileExtension = Path.GetExtension(fname).ToLower();
                            Guid g = Guid.NewGuid();
                             fname = g.ToString() + fileExtension; 
                            fileName = fname;
                            filesNames.Add(fileName);

                        }

                        // Get the complete folder path and store the file inside it.  
                        fileName = Path.Combine(Server.MapPath("~/uploads/"), fileName);
                        file.SaveAs(fileName);
                    }
                    // Returns message that successfully uploaded  
                    return Json(new { message = "File Uploaded Successfully!", name = fileName });
                }
                catch (Exception ex)
                {
                    return Json(new { message = "Error occurred. Error details: " + ex.Message, name = "" });
                }
            }
            else
            {
                return Json(new { message = "No files selected.", name = "" });
            }
        }

        //protected override void OnException(ExceptionContext filterContext)
        //{
        //    //var exceptionType = filterContext.Exception.GetType();
        //    //if (exceptionType == typeof(AppException))
        //    //{
        //    //    //filterContext.ExceptionHandled = true;
        //    //    //this.Flash("danger", filterContext.Exception.Message);
        //    //}
        //    //base.OnException(filterContext);
        //    Exception exception = filterContext.Exception;
        //    //Logging the Exception
        //    Elmah.ErrorSignal.FromCurrentContext().Raise(exception);
        //    filterContext.ExceptionHandled = true;


        //    ViewBag.Message = exception.Message;
        //    ViewBag.Trace = exception.StackTrace;

        //    var Result = this.View("Error", new HandleErrorInfo(exception,
        //        filterContext.RouteData.Values["controller"].ToString(),
        //        filterContext.RouteData.Values["action"].ToString()));

        //    filterContext.Result = Result;
        //}

        public JsonResult SignalJsonSuccess(string message)
        {
            return Json(new OperationDetails(true, message, ""));
        }

        public JsonResult SignalJsonError(string message,string property="")
        {
            Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
            return Json(new OperationDetails(false, message, property));
        }
        [HttpPost]
        public ActionResult DeleteUploadedImage(string name)
        {
            var path = Path.Combine(HttpContext.Server.MapPath("~/uploads/"), name);
            FileInfo info = new FileInfo(path);
            info.Delete();
            return Json("OK");
        }
    }
}