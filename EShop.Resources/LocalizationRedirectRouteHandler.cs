using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using EShop.Common;

namespace EShop.Resources
{
    public class LocalizationRedirectRouteHandler : IRouteHandler
    {
        public IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            var routeValues = requestContext.RouteData.Values;

            //var cookieLocale = requestContext.HttpContext.Request.Cookies["locale"];
            //var cookieWebsite = requestContext.HttpContext.Request.Cookies["website"];

            //if (cookieLocale != null)
            //{
            //    routeValues["culture"] = cookieLocale.Value;
            //}
            //else
            //{
            //    routeValues["culture"] = "ar-SA";
            //    //routeValues["culture"] = "en-US";
            //}

            if (routeValues["culture"] == null)
            {
                routeValues["culture"] = "ar-SA";
            }
            if (routeValues["website"] == null)
            {
                routeValues["website"] = "Fashion";
                routeValues.Add("fl","new");
            }
            else
            {
                var queryString = requestContext.HttpContext.Request.QueryString;
                foreach (var key in queryString.AllKeys)
                {
                    if (!routeValues.ContainsKey(key))
                    {
                        routeValues.Add(key, queryString[key]);
                    }
                }
            }


            var redirectUrl = new UrlHelper(requestContext).RouteUrl(routeValues);
            return new RedirectHandler(redirectUrl);

        }
    }
}
