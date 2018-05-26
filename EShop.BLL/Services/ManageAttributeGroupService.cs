using EShop.BLL.DTO;
using EShop.BLL.Infrastructure;
using EShop.BLL.Interfaces;
using EShop.Common;
using EShop.DAL.Entities;
using EShop.DAL.Interfaces;
using EShop.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.BLL.Services
{
   public class ManageAttributeGroupService:IManageAttributeGroup
    {
       private IUnitOfWork _unitOfWork;
       public ManageAttributeGroupService(IUnitOfWork _unitOfWork)
       {
           this._unitOfWork = _unitOfWork;
       }

       public OperationDetails DeleteAttribGroup(long groupId)
       {
           AttributeGroup attribGroup = _unitOfWork.AttributeGroupRepsitory.GetByID(groupId);
           if (attribGroup == null)
               return new OperationDetails(false, "المجموعة غير موجودة", "");

           if (attribGroup.Attributes.Any())
               return new OperationDetails(false, "لا يمكن حذف المجموعة لوجود واصفات مرتبطة", "");

           if (attribGroup.AttributeGroupDescriptions.Any())
           {
               List<AttributeGroupDescription> descs = _unitOfWork.AttributeGroupDescriptionRepsitory
                   .Get(c => c.GroupId == groupId).ToList();
               foreach (AttributeGroupDescription descr in descs)
               {
                   _unitOfWork.AttributeGroupDescriptionRepsitory.Delete(descr);
                   _unitOfWork.Save();
               }
           }
           _unitOfWork.AttributeGroupRepsitory.Delete(attribGroup);
           _unitOfWork.Save();
           return new OperationDetails(true, "تمت عملية الحذف", "");
       }
       public OperationDetails DeleteAttrib(long attribId)
       {
           Attrib attrib = _unitOfWork.AttribRepository.GetByID(attribId);
           if (attrib == null)
               return new OperationDetails(false, "الواصفة غير موجودة", "");
          var prodAttrs = _unitOfWork.ProductAttributeValueRepository.Get(c => c.AttributeId == attribId).FirstOrDefault();
           if (prodAttrs != null)
               return new OperationDetails(false, "لا يمكن حذف الواصفة لوجود منتجات مرتبطة", "");

           if (attrib.AttributeDescriptions.Any())
           {
               List<AttributeDescription> descs = _unitOfWork.AttributeDescriptionRepository
                   .Get(c => c.AttributeId == attribId).ToList();
               foreach (var desc in descs)
               {
                   _unitOfWork.AttributeDescriptionRepository.Delete(desc);
                   _unitOfWork.Save();
               }
           }
           _unitOfWork.AttribRepository.Delete(attrib);
           _unitOfWork.Save();
           return new OperationDetails(true, "تمت عملية الحذف", "");
       }
       public OperationDetails AddAttributeGroup(AttributeGroupDTO dto)
       {
           AttributeGroup group = new AttributeGroup();
           group.SortOrder = dto.SortOrder;
           group.DateAdded = DateTime.Now;
           group.DateModefied = DateTime.Now;
           group = _unitOfWork.AttributeGroupRepsitory.Insert(group);

           AttributeGroupDescription arabicDescription = new AttributeGroupDescription();
           AttributeGroupDescription englishDescription = new AttributeGroupDescription();

           arabicDescription.GroupId = group.Id;
           arabicDescription.Text = dto.ArabicName;
           arabicDescription.LanguageId = (long)Langs.Arabic;
           arabicDescription.DateAdded = DateTime.Now;
           arabicDescription.DateModefied = DateTime.Now;

           englishDescription.GroupId = group.Id;
           englishDescription.Text = dto.EnglishName;
           englishDescription.LanguageId = (long)Langs.English;
           englishDescription.DateAdded = DateTime.Now;
           englishDescription.DateModefied = DateTime.Now;
           _unitOfWork.AttributeGroupDescriptionRepsitory.Insert(arabicDescription);
           _unitOfWork.AttributeGroupDescriptionRepsitory.Insert(englishDescription);

           _unitOfWork.Save();
           return new OperationDetails(true, "تمت إضافة مجموعة الواصفات بنجاح", "");
       }

       public OperationDetails EditAttributeGroup(AttributeGroupDTO dto)
       {
           AttributeGroup group = _unitOfWork.AttributeGroupRepsitory.Get(c => c.Id == dto.Id).FirstOrDefault();
           group.SortOrder = dto.SortOrder;
           group.DateAdded = DateTime.Now;
           group.DateModefied = DateTime.Now;
           _unitOfWork.AttributeGroupRepsitory.Update(group);

           long arabicLang = (long)Langs.Arabic;
           long englishLang = (long)Langs.English;

           AttributeGroupDescription arabicDescription = _unitOfWork.AttributeGroupDescriptionRepsitory.Get(c =>c.GroupId == dto.Id && c.LanguageId == arabicLang).FirstOrDefault();

           AttributeGroupDescription englishDescription = _unitOfWork.AttributeGroupDescriptionRepsitory.Get(c => c.GroupId == dto.Id && c.LanguageId == englishLang).FirstOrDefault();

          
           arabicDescription.Text = dto.ArabicName;
           arabicDescription.LanguageId = (long)Langs.Arabic;
           arabicDescription.DateModefied = DateTime.Now;

           
           englishDescription.Text = dto.EnglishName;
           englishDescription.DateModefied = DateTime.Now;

           _unitOfWork.AttributeGroupDescriptionRepsitory.Update(arabicDescription);
           _unitOfWork.AttributeGroupDescriptionRepsitory.Update(englishDescription);

           _unitOfWork.Save();
           return new OperationDetails(true, "تمت تعديل مجموعة الواصفات بنجاح", "");
       }
      
       public AttributeGroupDTO GetAttributeGroupById(long Id)
       {
           AttributeGroup group = _unitOfWork.AttributeGroupRepsitory.Get(c => c.Id == Id).FirstOrDefault();

           long arabicLang = (long)Langs.Arabic;
           long englishLang = (long)Langs.English;

           AttributeGroupDTO groupDto = new AttributeGroupDTO();
           groupDto.Id = group.Id;
           groupDto.ArabicName = group.AttributeGroupDescriptions.Where(c => c.LanguageId == arabicLang).Select(n => n.Text).FirstOrDefault();
           groupDto.EnglishName = group.AttributeGroupDescriptions.Where(c => c.LanguageId == englishLang).Select(n => n.Text).FirstOrDefault();
           groupDto.SortOrder = group.SortOrder;
           return groupDto;

       }

       public List<AttributeGroupDTO> Filter(string arabicName, string englishName,  long id, Sorts s)
       {

           List<long> groupsIds = _unitOfWork.AttributeGroupRepsitory.Get(c => true).Select(c => c.Id).ToList();

           if (!String.IsNullOrEmpty(arabicName))
               FilterName(ref groupsIds, arabicName, Langs.Arabic);
           if (!String.IsNullOrEmpty(englishName))
               FilterName(ref groupsIds, englishName, Langs.English);
           //if (status != null)
           //    FilterStatus(ref categoryIds, status.Value);
           if (id != null && id != 0)
               FilterId(ref groupsIds, id);

           List<AttributeGroupDTO> groups = new List<AttributeGroupDTO>();

           for (int i = 0; i < groupsIds.Count; i++)
           {
               groups.Add(GetAttributeGroupById(groupsIds[i]));
           }
           sortGroups(ref groups, s);
           return groups;
       }

       private void FilterName(ref List<long> groupsIds, string name, Langs l)
       {
           long s = (long)Utils.getLanguage(l);
           List<long> ids = _unitOfWork.AttributeGroupDescriptionRepsitory.
                                       Get(c => c.Text.Contains(name) && c.LanguageId == s).
                                           Select(op => op.GroupId).Distinct().ToList();

           groupsIds = groupsIds.Intersect(ids).ToList();
       }

       private void FilterId(ref List<long> groupsIds, long id)
       {
           List<long> ids = _unitOfWork.AttributeGroupRepsitory.
                                       Get(c => c.Id == id).
                                           Select(op => op.Id).Distinct().ToList();
           groupsIds = groupsIds.Intersect(ids).ToList();
       }

       private void sortGroups(ref List<AttributeGroupDTO> groups, Sorts s)
       {
           switch (s)
           {
               case Sorts.ArabicNameDown: groups.Sort((a, b) => -1 * a.ArabicName.CompareTo(b.ArabicName));   //products.OrderBy(op => op.Name);
                   break;
               case Sorts.ArabicNameUp: groups.Sort((a, b) => a.ArabicName.CompareTo(b.ArabicName));
                   break;
               case Sorts.EnglishNameDown: groups.Sort((a, b) => -1 * a.EnglishName.CompareTo(b.EnglishName)); //products.OrderBy(op => op.Price);
                   break;
               case Sorts.EnglishNameUp: groups.Sort((a, b) => a.EnglishName.CompareTo(b.EnglishName));
                   break;
               case Sorts.IdDown: groups.Sort((a, b) => -1 * a.Id.CompareTo(b.Id));
                   break;
               case Sorts.IdUp: groups.Sort((a, b) => a.Id.CompareTo(b.Id));
                   break;
               //case Sorts.StatusDown: groups.Sort((a, b) => -1 * a.Status.CompareTo(b.Status));
               //    break;
               //case Sorts.StatusUp: groups.Sort((a, b) => a.Status.CompareTo(b.Status));
               //    break;
           }

       }

       public OperationDetails AddAttribute(AttributeDTO dto)
       {
           Attrib attribute = new Attrib();
           attribute.SortOrder = dto.SortOrder;
           attribute.DateAdded = DateTime.Now;
           attribute.DateModefied = DateTime.Now;
           attribute.GroupId = dto.GroupId;
           attribute = _unitOfWork.AttribRepository.Insert(attribute);
           _unitOfWork.Save();
           AttributeDescription arabicDescription = new AttributeDescription();
           AttributeDescription englishDescription = new AttributeDescription();

           arabicDescription.AttributeId = attribute.Id;
           arabicDescription.Text = dto.ArabicName;
           arabicDescription.LanguageId = (long)Langs.Arabic;
           arabicDescription.DateAdded = DateTime.Now;
           arabicDescription.DateModefied = DateTime.Now;

           englishDescription.AttributeId = attribute.Id;
           englishDescription.Text = dto.EnglishName;
           englishDescription.LanguageId = (long)Langs.English;
           englishDescription.DateAdded = DateTime.Now;
           englishDescription.DateModefied = DateTime.Now;
           _unitOfWork.AttributeDescriptionRepository.Insert(arabicDescription);
           _unitOfWork.AttributeDescriptionRepository.Insert(englishDescription);

           _unitOfWork.Save();
           return new OperationDetails(true, "تمت إضافة الواصفة بنجاح", "");
       }

       public OperationDetails EditAttribute(AttributeDTO dto)
       {
           Attrib attribute = _unitOfWork.AttribRepository.Get(c => c.Id == dto.Id).FirstOrDefault();
           attribute.SortOrder = dto.SortOrder;
           attribute.DateAdded = DateTime.Now;
           attribute.DateModefied = DateTime.Now;
           attribute.GroupId = dto.GroupId;
           _unitOfWork.AttribRepository.Update(attribute);

           long arabicLang = (long)Langs.Arabic;
           long englishLang = (long)Langs.English;

           AttributeDescription arabicDescription = _unitOfWork.AttributeDescriptionRepository.Get(c => c.AttributeId == dto.Id && c.LanguageId == arabicLang).FirstOrDefault();

           AttributeDescription englishDescription = _unitOfWork.AttributeDescriptionRepository.Get(c => c.AttributeId == dto.Id && c.LanguageId == englishLang).FirstOrDefault();


           arabicDescription.Text = dto.ArabicName;
           //arabicDescription.LanguageId = (long)Langs.Arabic;
           arabicDescription.DateModefied = DateTime.Now;


           englishDescription.Text = dto.EnglishName;
           englishDescription.DateModefied = DateTime.Now;

           _unitOfWork.AttributeDescriptionRepository.Update(arabicDescription);
           _unitOfWork.AttributeDescriptionRepository.Update(englishDescription);

           _unitOfWork.Save();
           return new OperationDetails(true, "تمت تعديل الواصفة بنجاح", "");
       }

       public List<AttributeGroupDTO> GetAllAttributeGroups()
       {
           List<AttributeGroupDTO> groupsDTOs = new List<AttributeGroupDTO>();
           List<AttributeGroup> groups = _unitOfWork.AttributeGroupRepsitory.Get(c =>true).ToList();
           foreach (var group in groups)
           {
               long arabic = (long)Langs.Arabic;
               long english = (long)Langs.English;

               AttributeGroupDTO groupDTO = new AttributeGroupDTO();
               groupDTO.ArabicName = group.AttributeGroupDescriptions.Where(c => c.LanguageId == arabic).Select(g => g.Text).FirstOrDefault();
               groupDTO.EnglishName = group.AttributeGroupDescriptions.Where(c => c.LanguageId == english).Select(g => g.Text).FirstOrDefault();
               groupDTO.SortOrder = group.SortOrder;
               groupDTO.Id = group.Id;
               groupsDTOs.Add(groupDTO);
           }
           return groupsDTOs;
       }

       public AttributeDTO GetAttributeById(long Id)
       {
           Attrib attribute = _unitOfWork.AttribRepository.Get(c => c.Id == Id).FirstOrDefault();

           long arabicLang = (long)Langs.Arabic;
           long englishLang = (long)Langs.English;

           AttributeDTO attributeDto = new AttributeDTO();
           attributeDto.Id = attribute.Id;
           attributeDto.GroupId = attribute.GroupId;
           attributeDto.GroupName = attribute.Group.AttributeGroupDescriptions.Where(c=>c.LanguageId==arabicLang).FirstOrDefault().Text;
           attributeDto.ArabicName = _unitOfWork.AttributeDescriptionRepository.Get(c => c.LanguageId == arabicLang && c.AttributeId == Id ).Select(n => n.Text).FirstOrDefault();
           attributeDto.EnglishName = _unitOfWork.AttributeDescriptionRepository.Get(c => c.LanguageId == englishLang && c.AttributeId == Id).Select(n => n.Text).FirstOrDefault();
           attributeDto.SortOrder = attribute.SortOrder;
           attributeDto.Groups = GetAllAttributeGroups();
           return attributeDto;

       }

       public List<AttributeDTO> FilterAtrributes(string arabicName, long GroupId, string englishName, long id,string groupName, Sorts s)
       {

           List<long> attributesIds = _unitOfWork.AttribRepository.Get(c => true).Select(c => c.Id).ToList();

           if (!String.IsNullOrEmpty(arabicName))
               FilterAttName(ref attributesIds, arabicName, Langs.Arabic);
           if (!String.IsNullOrEmpty(englishName))
               FilterAttName(ref attributesIds, englishName, Langs.English);
           if (!String.IsNullOrEmpty(groupName))
               FilterGroupName(ref attributesIds, groupName);
           //if (status != null)
           //    FilterStatus(ref categoryIds, status.Value);
           if (id != null && id != 0)
               FilterAttId(ref attributesIds, id);
           if (GroupId != null && GroupId != 0)
               FilterGroupId(ref attributesIds, GroupId);

           List<AttributeDTO> attributes = new List<AttributeDTO>();

           for (int i = 0; i < attributesIds.Count; i++)
           {
               attributes.Add(GetAttributeById(attributesIds[i]));
           }
           sortAttribute(ref attributes, s);
           return attributes;
       }

       private void FilterGroupId(ref List<long> attributesIds, long id)
       {
           List<long> ids = _unitOfWork.AttribRepository.
                                      Get(c => c.GroupId == id).
                                          Select(op => op.Id).Distinct().ToList();
           attributesIds = attributesIds.Intersect(ids).ToList();
       }

       private void FilterAttId(ref List<long> attributesIds, long id)
       {
           List<long> ids = _unitOfWork.AttribRepository.
                                      Get(c => c.Id == id).
                                          Select(op => op.Id).Distinct().ToList();
           attributesIds = attributesIds.Intersect(ids).ToList();
       }

       private void FilterAttName(ref List<long> attributesIds, string arabicName, Langs langs)
       {
           long s = (long)Utils.getLanguage(langs);
           List<long> ids = _unitOfWork.AttributeDescriptionRepository.
                                       Get(c => c.Text.Contains(arabicName) && c.LanguageId == s).
                                           Select(op => op.AttributeId).Distinct().ToList();

           attributesIds = attributesIds.Intersect(ids).ToList();
       }
       private void FilterGroupName(ref List<long> attributesIds, string groupName)
       {
           var groupsIds = _unitOfWork.AttributeGroupDescriptionRepsitory.Get(c => c.Text == groupName).Select(g => g.GroupId);
           var ids = _unitOfWork.AttribRepository.Get(c => groupsIds.Contains(c.GroupId)).Select(g => g.Id);
           //List<long> ids = _unitOfWork.AttributeDescriptionRepository.
           //                            Get(c => c.Text.Contains(arabicName) && c.LanguageId == s).
           //                                Select(op => op.AttributeId).Distinct().ToList();

           attributesIds = attributesIds.Intersect(ids).ToList();
       }
       private void sortAttribute(ref List<AttributeDTO> attribute, Sorts s)
       {
           switch (s)
           {
               case Sorts.ArabicNameDown: attribute.Sort((a, b) => -1 * a.ArabicName.CompareTo(b.ArabicName));   //products.OrderBy(op => op.Name);
                   break;
               case Sorts.ArabicNameUp: attribute.Sort((a, b) => a.ArabicName.CompareTo(b.ArabicName));
                   break;
               case Sorts.EnglishNameDown: attribute.Sort((a, b) => -1 * a.EnglishName.CompareTo(b.EnglishName)); //products.OrderBy(op => op.Price);
                   break;
               case Sorts.EnglishNameUp: attribute.Sort((a, b) => a.EnglishName.CompareTo(b.EnglishName));
                   break;
               case Sorts.IdDown: attribute.Sort((a, b) => -1 * a.Id.CompareTo(b.Id));
                   break;
               case Sorts.IdUp: attribute.Sort((a, b) => a.Id.CompareTo(b.Id));
                   break;
               case Sorts.AttributeGroupIdDown: attribute.Sort((a, b) => -1 * a.GroupId.CompareTo(b.GroupId));
                   break;
               case Sorts.AttributeGroupIdUp: attribute.Sort((a, b) => a.GroupId.CompareTo(b.GroupId));
                   break;
               case Sorts.NameDownUp: attribute.Sort((a, b) => -1 * a.GroupName.CompareTo(b.GroupName));
                   break;
               case Sorts.NameUpDown: attribute.Sort((a, b) => a.GroupName.CompareTo(b.GroupName));
                   break;
               //case Sorts.StatusDown: groups.Sort((a, b) => -1 * a.Status.CompareTo(b.Status));
               //    break;
               //case Sorts.StatusUp: groups.Sort((a, b) => a.Status.CompareTo(b.Status));
               //    break;
           }

       }

       public List<AttributeDTO> GetAttributeGroupAttributes(long groupId,Langs l)
       {
           AttributeGroup attributeGroup = _unitOfWork.AttributeGroupRepsitory.Get(c => c.Id == groupId).FirstOrDefault();
           List<AttributeDTO> attributesDtos = new List<AttributeDTO>();

           //long arabicLang = (long)Langs.Arabic;
           //long englishLang = (long)Langs.English;
           long Lang = Utils.getLanguage(l);


           if(attributeGroup != null)
           {
               List<Attrib> attributes = attributeGroup.Attributes.ToList();
               foreach (var attribute in attributes)
               {
                   AttributeDTO dto = new AttributeDTO();
                   dto.Id = attribute.Id;
                   dto.Name = _unitOfWork.AttributeDescriptionRepository.Get(c => c.LanguageId == Lang && c.AttributeId == attribute.Id).Select(g => g.Text).FirstOrDefault();
                   attributesDtos.Add(dto);
                 
               }
           }
           return attributesDtos;
       }

       public List<AttributeGroupDTO> GetAllAttributeGroups(Langs l)
       {
           List<AttributeGroup> groups = _unitOfWork.AttributeGroupRepsitory.Get(c => true).ToList();
           List<AttributeGroupDTO> groupsDtos = new List<AttributeGroupDTO>();

           long lang = Utils.getLanguage(l);

           foreach (var group in groups)
           {
               AttributeGroupDTO dto = new AttributeGroupDTO();
               dto.Id = group.Id;
               dto.Name = group.AttributeGroupDescriptions.Where(c => c.LanguageId == lang).Select(g => g.Text).FirstOrDefault();
               dto.Attributes = GetAttributeGroupAttributes(group.Id,l);
               groupsDtos.Add(dto);
           }
           return groupsDtos;
       }

    }
}
