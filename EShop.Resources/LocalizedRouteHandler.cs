using System;
using System.Globalization;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using EShop.Common;

namespace EShop.Resources
{
    public class LocalizedRouteHandler : MvcRouteHandler
    {
        //protected override IHttpHandler GetHttpHandler(RequestContext requestContext)
        //{
        //    var urlLocale = requestContext.RouteData.Values["culture"] as string;
        //    var urlWebsite = requestContext.RouteData.Values["website"] as string;
        //   


        //    var cookieLocale = requestContext.HttpContext.Request.Cookies["locale"];
        //    var cookieWebsite = requestContext.HttpContext.Request.Cookies["website"];
        //    if (cookieLocale != null)
        //    {
        //        // if request contains locale cookie, we need to put higher priority than url locale
        //        // user might click the link from somewhere but he/she already set different locale
        //        if (!cookieLocale.Value.Equals(urlLocale, StringComparison.OrdinalIgnoreCase))
        //        {
        //            // if cookie locale and url cookie are different,
        //            // we should redirect with cookie locale
        //            var routeValues = requestContext.RouteData.Values;
        //            routeValues["culture"] = cookieLocale.Value;

        //            var queryString = requestContext.HttpContext.Request.QueryString;
        //            foreach (var key in queryString.AllKeys)
        //            {
        //                if (!routeValues.ContainsKey(key))
        //                {
        //                    routeValues.Add(key, queryString[key]);
        //                }
        //            }

        //            return new RedirectHandler(new UrlHelper(requestContext).RouteUrl(routeValues));
        //        }
        //        else
        //        {
        //            cultureName = cookieLocale.Value;
        //        }
        //    }

        //    if (cultureName == "")
        //    {
        //        return GetDefaultLocaleRedirectHandler(requestContext);
        //    }

        //    try
        //    {
        //        var culture = CultureInfo.GetCultureInfo(cultureName);
        //        Thread.CurrentThread.CurrentCulture = culture;
        //        Thread.CurrentThread.CurrentUICulture = culture;
        //    }
        //    catch (CultureNotFoundException)
        //    {
        //        // if CultureInfo.GetCultureInfo throws exception
        //        // we should redirect with default locale
        //        return GetDefaultLocaleRedirectHandler(requestContext);
        //    }

        //    if (!String.IsNullOrWhiteSpace(urlWebsite))
        //    {
        //        var website=urlWebsite=="C"?WebSites.Clothes.ToString():urlWebsite=="T"?WebSites.Tech.ToString():WebSites.Gym.ToString();
        //        var websiteCookie = new HttpCookie("website", website) { HttpOnly = true };
        //        requestContext.HttpContext.Response.AppendCookie(websiteCookie);
        //    }

        //    if (cookieLocale == null)
        //    {
        //        requestContext.HttpContext.Response.AppendCookie(new HttpCookie("locale", cultureName));
        //    }
        //    /**
        //     * Currency Cookie  Append
        //     */
        //    var cookieCurrency = requestContext.HttpContext.Request.Cookies["Currency"];
        //    if (cookieCurrency == null)
        //    {
        //        requestContext.HttpContext.Response.AppendCookie(new HttpCookie("Currency", "USD"));

        //    }
        //    return base.GetHttpHandler(requestContext);
        //}
        protected override IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            var urlLocale = requestContext.RouteData.Values["culture"] as string;
            var cultureName = urlLocale ?? "";
            var urlWebsite = requestContext.RouteData.Values["website"] as string;

            //var cookieLocale = requestContext.HttpContext.Request.Cookies["locale"];

            //if (cookieLocale != null)
            //{
            //    // if request contains locale cookie, we need to put higher priority than url locale
            //    // user might click the link from somewhere but he/she already set different locale
            //    if (!cookieLocale.Value.Equals(urlLocale, StringComparison.OrdinalIgnoreCase))
            //    {
            //        // if cookie locale and url cookie are different,
            //        // we should redirect with cookie locale
            //        var routeValues = requestContext.RouteData.Values;
            //        routeValues["culture"] = cookieLocale.Value;

            //        var queryString = requestContext.HttpContext.Request.QueryString;
            //        foreach (var key in queryString.AllKeys)
            //        {
            //            if (!routeValues.ContainsKey(key))
            //            {
            //                routeValues.Add(key, queryString[key]);
            //            }
            //        }

            //        return new RedirectHandler(new UrlHelper(requestContext).RouteUrl(routeValues));
            //    }
            //    else
            //    {
            //        cultureName = cookieLocale.Value;
            //    }

            //}

            if (cultureName == "")
            {
                return GetDefaultLocaleRedirectHandler(requestContext);
            }

            try
            {
                var culture = CultureInfo.GetCultureInfo(cultureName);
                Thread.CurrentThread.CurrentCulture = culture;
                Thread.CurrentThread.CurrentUICulture = culture;
            }
            catch (CultureNotFoundException)
            {
                // if CultureInfo.GetCultureInfo throws exception
                // we should redirect with default locale
                return GetDefaultLocaleRedirectHandler(requestContext);
            }

            //if (cookieLocale == null)
            //{
                requestContext.HttpContext.Response.AppendCookie(new HttpCookie("locale", cultureName));
            //}
             if (!String.IsNullOrWhiteSpace(urlWebsite))
             {
                 var website = urlWebsite == "Fashion" ? WebSites.Clothes.ToString() : urlWebsite == "Gym" ? WebSites.Tech.ToString() : WebSites.Gym.ToString();
                 var websiteCookie = new HttpCookie("website", website) { HttpOnly = true };
                 requestContext.HttpContext.Response.AppendCookie(websiteCookie);
             }
             else
             {
                 var routeValues = requestContext.RouteData.Values;
                 routeValues["website"] = "Fashion";
                 var websiteCookie = new HttpCookie("website", WebSites.Clothes.ToString()) { HttpOnly = true };
                 requestContext.HttpContext.Response.AppendCookie(websiteCookie);

                 routeValues.Add("fl", "new");
                 var queryString = requestContext.HttpContext.Request.QueryString;
                 foreach (var key in queryString.AllKeys)
                 {
                     if (!routeValues.ContainsKey(key))
                     {
                         routeValues.Add(key, queryString[key]);
                     }
                 }

                 return new RedirectHandler(new UrlHelper(requestContext).RouteUrl(routeValues));
             }
            var cookieCurrency = requestContext.HttpContext.Request.Cookies["Currency"];
            if (cookieCurrency == null)
            {
                requestContext.HttpContext.Response.AppendCookie(new HttpCookie("Currency", "USD"));

            }
            return base.GetHttpHandler(requestContext);
        }

        private static IHttpHandler GetDefaultLocaleRedirectHandler(RequestContext requestContext)
        {
            var uiCulture = CultureInfo.CurrentUICulture;
            var routeValues = requestContext.RouteData.Values;
            routeValues["culture"] = uiCulture.Name;
            requestContext.HttpContext.Response.AppendCookie(new HttpCookie("locale", uiCulture.Name));
            return new RedirectHandler(new UrlHelper(requestContext).RouteUrl(routeValues));
        }
    }
}
