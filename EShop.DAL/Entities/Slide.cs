using EShop.Common;
using EShop.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.DAL.Entities
{
    public class Slide : IEntityBase
    {
       
        public long Id { get; set; }
        public string ImageUrl { get; set; }
        public string Url { get; set; }
        public long WebSite { get; set; }

    }
}
