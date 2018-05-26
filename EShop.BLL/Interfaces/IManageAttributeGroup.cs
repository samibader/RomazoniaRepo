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
    public interface IManageAttributeGroup
    {
        OperationDetails DeleteAttribGroup(long groupId);
        OperationDetails DeleteAttrib(long attribId);
        List<AttributeGroupDTO> GetAllAttributeGroups(Langs l);
        List<AttributeDTO> GetAttributeGroupAttributes(long groupId, Langs l);
        OperationDetails EditAttribute(AttributeDTO dto);
        OperationDetails AddAttribute(AttributeDTO dto);
        List<AttributeGroupDTO> Filter(string arabicName, string englishName, long id, Sorts s);
        OperationDetails AddAttributeGroup(AttributeGroupDTO dto);
        OperationDetails EditAttributeGroup(AttributeGroupDTO dto);
        AttributeGroupDTO GetAttributeGroupById(long Id);

        AttributeDTO GetAttributeById(long Id);
        List<AttributeGroupDTO> GetAllAttributeGroups();
        List<AttributeDTO> FilterAtrributes(string arabicName, long GroupId, string englishName, long id, string groupName, Sorts s);
    }
}
