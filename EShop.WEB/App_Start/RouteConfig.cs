using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using EShop.Resources;
using System.Globalization;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using EShop.Common;

namespace EShop.WEB
{
    //class RedirectHandler : IHttpHandler
    //{
    //    private readonly string _newUrl;

    //    [SuppressMessage(category: "Microsoft.Design", checkId: "CA1054:UriParametersShouldNotBeStrings", MessageId = "0#",
    //        Justification = "We just use string since HttpResponse.Redirect only accept as string parameter.")]
    //    public RedirectHandler(string newUrl)
    //    {
    //        _newUrl = newUrl;
    //    }

    //    public bool IsReusable
    //    {
    //        get { return true; }
    //    }

    //    public void ProcessRequest(HttpContext context)
    //    {
    //        context.Response.Redirect(_newUrl);
    //    }
    //}
    public class RouteConfig
    {

        //private static IHttpHandler GetDefaultLocaleRedirectHandler(RequestContext requestContext)
        //{
        //    var uiCulture = CultureInfo.CurrentUICulture;
        //    var routeValues = requestContext.RouteData.Values;
        //    routeValues["culture"] = uiCulture.Name;
        //    requestContext.HttpContext.Response.AppendCookie(new HttpCookie("locale", uiCulture.Name));
        //    return new RedirectHandler(new UrlHelper(requestContext).RouteUrl(routeValues));
        //}
        public class CustomRouteHandler : MvcRouteHandler
        {
            protected override IHttpHandler GetHttpHandler(RequestContext requestContext)
            {
               
                var urlLocale = requestContext.RouteData.Values["culture"] as string;
                var urlWebsite = requestContext.RouteData.Values["website"] as string;
               

                var cookieLocale = requestContext.HttpContext.Request.Cookies["locale"];
                var cookieWebsite = requestContext.HttpContext.Request.Cookies["website"];

                bool shouldRedirect = false;
                var routeValues = requestContext.RouteData.Values;
                //if (requestContext.HttpContext.Request.RawUrl == "/")
                //    routeValues.Add("fl","new");
                if(String.IsNullOrEmpty(urlLocale))
                {
                    if (cookieLocale != null)
                        routeValues["culture"] = cookieLocale.Value;
                    else
                        routeValues["culture"] = "ar-sa";

                    urlLocale = routeValues["culture"].ToString();
                    shouldRedirect = true;
                }

                if (String.IsNullOrEmpty(urlWebsite))
                {
                    //if (cookieWebsite != null && !String.IsNullOrEmpty(cookieWebsite.Value))
                    //    routeValues["website"] = cookieWebsite.Value;
                    //else
                        routeValues["website"] = "Mobile";

                    urlWebsite = routeValues["website"].ToString();
                    shouldRedirect = true;
                }
                var cookieCurrency = requestContext.HttpContext.Request.Cookies["Currency"];
                if (cookieCurrency == null)
                {
                    requestContext.HttpContext.Response.AppendCookie(new HttpCookie("Currency", Currency.Rial.ToString().ToLower()));


                }
              //  requestContext.HttpContext.Response.AppendCookie(new HttpCookie("Currency", "USD"));
                requestContext.HttpContext.Response.AppendCookie(new HttpCookie("locale", urlLocale));
                requestContext.HttpContext.Response.AppendCookie(new HttpCookie("website", urlWebsite));

                if (shouldRedirect)
                {
                    var queryString = requestContext.HttpContext.Request.QueryString;
                    foreach (var key in queryString.AllKeys)
                    {
                        if (!routeValues.ContainsKey(key))
                        {
                            routeValues.Add(key, queryString[key]);
                        }
                    }
                    requestContext.HttpContext.Response.Redirect(new UrlHelper(requestContext).RouteUrl(routeValues));
                }

                var culture = CultureInfo.GetCultureInfo(urlLocale);
                Thread.CurrentThread.CurrentCulture = culture;
                Thread.CurrentThread.CurrentUICulture = culture;

                //var route = (Route)requestContext.RouteData.Route;

                //if (!route.Url.Contains("{culture}"))
                //    route.Url = "{culture}/" + route.Url;
                //if (route.Defaults == null)
                //{
                //    route.Defaults = new RouteValueDictionary();
                //    route.Defaults.Add("culture", "ar-sa");
                //}
                //else
                //{
                //    route.Defaults["lang"] = "ar-sa";
                //}

                return base.GetHttpHandler(requestContext);
            }
        }
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("{resource}.ashx/{*pathInfo}");
            routes.IgnoreRoute("LiveZilla/{*pathInfo}");

        //    routes.MapRoute(name: "DefaultWithCulture"
        //               , url: "{culture}/{controller}/{action}/{id}"
        //               , defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
        //               , constraints: new { culture = "[a-z]{2}-[a-z]{2}" }
        //).RouteHandler = new CustomRouteHandler(); 
        //    routes.MapRoute(name: "DefaultWithWebsite"
        //               , url: "{website}/{controller}/{action}/{id}"
        //               , defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
        //               , constraints: new { website = "[C,G,T]" }
        //).RouteHandler = new CustomRouteHandler();
            routes.MapRoute(name: "DefaultWithCultureAndWebsite"
                       , url: "{culture}/{website}/{controller}/{action}/{id}"
                       , defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
                       , constraints: new { culture = "[a-z]{2}-[a-z]{2}", website = "Fashion|Gym|Mobile" }
        ).RouteHandler = new CustomRouteHandler();
            routes.MapRoute(name: "Culture"
                       , url: "{culture}"
                       , defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
                       , constraints: new { culture = "[a-z]{2}-[a-z]{2}"}
        ).RouteHandler = new CustomRouteHandler();
            routes.MapRoute(name: "Website"
                       , url: "{Website}"
                       , defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
                       , constraints: new { website = "Fashion|Gym|Mobile" }
        ).RouteHandler = new CustomRouteHandler();
            routes.MapRoute(name: "Default"
                           , url: "{controller}/{action}/{id}"
                           , defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            ).RouteHandler = new CustomRouteHandler();

            //routes.IgnoreRoute("IPNPaypal/Receive");
           // routes.MapRoute(
           //    "IPNPaypal/Receive", "IPNPaypal/Receive}",
           //    new { controller = "IPNPaypal", Receive = "Index", filename = "" }
           //);
            // routes.MapRoute(
            //    "Image", "images/{filename}",
            //    new { controller = "Images", action = "Index", filename = "" }
            //);
            //routes.MapLocalizeRoute(
            //    name: "Product",
            //    url: "{culture}/Product/{SKU}",
            //    defaults: new { controller = "Product", action = "Browse", SKU = "" }

            //    , constraints: new { culture = "[a-zA-Z]{2}-[a-zA-Z]{2}" }
            // );
//            routes.MapLocalizeRoute(
//    name: "Fashion",
//    url: "Fashion",
//    defaults: new { controller = "Home", action = "Index", website = "C",culture="ar-SA" }
//    , constraints: new { culture = "[a-zA-Z]{2}-[a-zA-Z]{2}" }
// );
//            routes.MapLocalizeRoute(
//name: "Gym",
//url: "Gym",
//defaults: new { controller = "Home", action = "Index", website = "G", culture = "ar-SA" }
//, constraints: new { culture = "[a-zA-Z]{2}-[a-zA-Z]{2}" }
//);
//            routes.MapLocalizeRoute(
//name: "Mobile",
//url: "Mobile",
//defaults: new { controller = "Home", action = "Index", website = "T", culture = "ar-SA" }
//, constraints: new { culture = "[a-zA-Z]{2}-[a-zA-Z]{2}" }
//);
//            routes.MapLocalizeRoute("Default",
//               url: "{culture}/{website}/{controller}/{action}/{id}",
//               defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
//               constraints: new { culture = "[a-zA-Z]{2}-[a-zA-Z]{2}", website = "[C,G,T]" });

//            routes.MapRouteToLocalizeRedirect("RedirectToLocalize",
//                        url: "{controller}/{action}/{id}",
//                        defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional });

            
        }
    }
}
