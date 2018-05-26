using EShop.BLL.DTO;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EShop.WEB.Models
{
    public class OptionIndexManagerVM
    {
        public IPagedList<OptionManagerDTO> Options { get; set; }
        public OptionFiltersDTO filters { get; set; }
    }
}