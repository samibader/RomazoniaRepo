using System.Diagnostics.CodeAnalysis;
using System.Web;

namespace EShop.Resources
{
    class RedirectHandler : IHttpHandler
    {
        private readonly string _newUrl;

        [SuppressMessage(category: "Microsoft.Design", checkId: "CA1054:UriParametersShouldNotBeStrings", MessageId = "0#",
            Justification = "We just use string since HttpResponse.Redirect only accept as string parameter.")]
        public RedirectHandler(string newUrl)
        {
           _newUrl = newUrl;
        }

        public bool IsReusable
        {
            get { return true; }
        }

        public void ProcessRequest(HttpContext context)
        {
            context.Response.Redirect(_newUrl);
        }
    }
}
