using EShop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EShop.WEB.Models.Filters
{

    public class SpecificWebsiteViewActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            dynamic ViewBag = filterContext.Controller.ViewBag;
            var cookieLocale = filterContext.HttpContext.Response.Cookies["website"];
                if (cookieLocale == null)
                {
                    ViewBag.CurrentWebsite = WebSites.Fashion;
                }
                else
                {
                    try
                    {
                        ViewBag.CurrentWebsite = cookieLocale.Values[0].ToLower() ==
                                                 WebSites.Fashion.ToString().ToLower()
                            ? WebSites.Fashion
                            : cookieLocale.Values[0].ToLower() == WebSites.Gym.ToString().ToLower()
                                ? WebSites.Gym
                                : WebSites.Mobile;
                    }
                    catch (Exception e)
                    {
                        
                    }
                }
            
        }
    }
}