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
    public interface IManageSizeAttributes
    {
        OperationDetails DeleteSizeAttribute(long Id);
        List<SizeAttributeDTO> GetSizeAttributesBySizeCaategoryId(long SizecatId, Langs l);
        List<SizeAttributeDTO> GetAllSizeAttributes(Langs l);
        List<SizeAttributeDTO> Filter(String arabicName, String englishName, long optionId, Sorts s);
        OperationDetails EditSizeAttribute(string arabicName, string englishName, long Id, long SizeCatId);
        //OperationDetails EditSizeAttributeValue(SizeAttributeValueDTO sizeAttributeValue);
        AddSizeAttributeDTO GetSizeAttributeById(long Id);
        //List<SizeAttributeValueDTO> GetSizeAttributeValuesBySizeAttributeId(long Id);
        OperationDetails AddSizeAttribute(string arabicName, string englishName,long SizeCategoryId);
        //OperationDetails AddSizeAttributeValue(SizeAttributeValueDTO sizeAttributeValue);
        AddSizeAttributeDTO GetAllSizesToAddSizeAttribute();

    }
}
