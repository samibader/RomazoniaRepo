using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EShop.WEB.Models
{
    public class BankInformationVM
    {
        [AllowHtml]
        public string Description { get; set; }
    }
}