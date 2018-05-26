using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EShop.WEB.Models
{
    public class TestVM
    {
        public List<TestItemVM> Items { get; set; }
    }
    public class TestItemVM
    {
        public string Name { get; set; }
        public long Id { get; set; }
        public List<TestItemStockVM> Items { get; set; }


    }
    public class TestItemStockVM
    {
        public string Name { get; set; }
        public long Id { get; set; }


    }
}