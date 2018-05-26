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
    public interface IProductManagerService
    {
        //OperationDetails DeleteOptionValue(long optionId, long valueId);
        OperationDetails DeleteProduct(long productId);
        OperationDetails EditProduct(long productId, AddProductDTO productDto);
        AddProductDTO GetEditProduct(long productId,Langs l);
        OperationDetails AddProduct(AddProductDTO productDto);
        List<CategoryDTO> GetSubCategories(long parentId, Langs l);
        List<OptionManagerDTO> GetAllSizeOptions();
        List<SizeValueDTO> GetSizesOfCat(long? sizeCatId);
        List<ColorValuesDTO> GetAllColors();
        List<SizeValueDTO> GetAllSizes();
        List<DesignerDTO> GetAllDesigners(Langs l);
        List<SizeCategoryDTO> GetSizeCategories(Langs l);
        List<TagDTO> GetTagByTerm(string query);
        List<ProductIndexManagerDTO> FilterProducts(string arabicName, string englishName, Double? minPrice, Double? maxPrice, Boolean? status, long id, Sorts s, Langs l, Currency cu,WebSites w); ProductIndexManagerDTO GetProductByID(long id, Langs l, Currency cu);
        OperationDetails DeleteOptionValue(long optionId, long optionValueId);
        OperationDetails DeleteOption(long optionId);
        OperationDetails EditOption(OptionManagerDTO optionDto);
        OperationDetails EditOptionValue(OptionValueManagerDTO optionValueDto);
        List<OptionManagerDTO> Filter(String arabicName, String englishName, long optionId, Sorts s);
        OperationDetails AddOptionValue(OptionValueManagerDTO optionValue);
        OperationDetails AddOption(string arabicName, string englishName);
        List<OptionValueManagerDTO> GetOptionValuesByOptionId(long optionId);
        List<OptionManagerDTO> GetAllOptions();
        OptionManagerDTO GetOptionById(long id);
    }
}
