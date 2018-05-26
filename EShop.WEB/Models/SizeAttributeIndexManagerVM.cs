using EShop.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PagedList;
using PagedList.Mvc;

namespace EShop.WEB.Models
{
    public class SizeAttributeIndexManagerVM
    {
        public IPagedList<SizeAttributeDTO> SizeAttributes { get; set; }
        public OptionFiltersDTO filters { get; set; }

    }
}
