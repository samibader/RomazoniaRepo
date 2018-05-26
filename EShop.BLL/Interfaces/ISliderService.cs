using EShop.BLL.DTO;
using EShop.BLL.Infrastructure;
using EShop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.BLL.Interfaces
{
    public interface ISliderService
    {

        OperationDetails AddSlide(SlideDTO dto);
        OperationDetails EditSlide(SlideDTO dto);
        OperationDetails DeleteSlide(long Id);
        List<SlideDTO> GetSlidesByWebSite(WebSites website);
        SlideDTO GetSlideById(long Id);
    }
}
