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
    public interface IManageDesigner
    {
        OperationDetails DeleteDesigner(long Id);
        List<ManageDesignerDTO> Filter(string arabicName, string englishName, long id, Sorts s);
        OperationDetails AddDesigner(ManageDesignerDTO dto);
        OperationDetails EditDesigner(ManageDesignerDTO dto);
        ManageDesignerDTO GetDesignerById(long Id);
        List<DesignerDTO> GetAllDesigners(Langs l);
    }
}
