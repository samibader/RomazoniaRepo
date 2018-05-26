using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Web;

namespace EShop.WEB.Simulation_Data
{
    public class SizeValueDTO
    {
        public long OptionId { get; set; }
        public long ProductId { get; set; }
        public long OptionValueId { get; set; }
        public bool Available { get; set; }
        public String Name { get; set; }
    }
}