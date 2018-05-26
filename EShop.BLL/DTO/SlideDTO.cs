using EShop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.BLL.DTO
{
    public class SlideDTO
    {
        public long Id { get; set; }
        public string ImageUrl { get; set; }
        public string Url { get; set; }
        public WebSites WebSite { get; set; }

    }
}
