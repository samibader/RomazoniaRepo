using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcFlashMessages;

namespace EShop.WEB.Controllers
{
    public class FlashController : BaseController
    {
        public ActionResult Error()
        {
            
            return View("Index");
        }

        public ActionResult Index()
        {
            this.Flash("success", "you successed");
            return View();
        }

        public ActionResult Info()
        {
            this.Flash("info", "This messages is providing you with some very important information.");
            return View("Index");
        }

        public ActionResult Success()
        {
            this.Flash("success", "Good job! Whatever you did must have worked!");
            return View("Index");
        }

        public ActionResult Warning()
        {
            this.Flash("warning", "Something almost broke. This is a warning. Whatever you just did, you probably shouldn't do it again.");
            return View("Index");
        }
	}
}