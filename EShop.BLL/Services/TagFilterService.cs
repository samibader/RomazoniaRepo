using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EShop.BLL.DTO;
using EShop.BLL.Interfaces;
using EShop.Common;
using EShop.DAL.Interfaces;

namespace EShop.BLL.Services
{

    public class TagFilterService : ITagFilterService
    {
        private IUnitOfWork unitOfWork { get; set; }
        private IProductFilterService _productFilterService;
        public TagFilterService(IUnitOfWork uow, IProductFilterService productFilterService)
        {
            unitOfWork = uow;
            _productFilterService = productFilterService;
        }
        public FilterMenuDTO FilterFilters(String tagName, String[] d, String[] subCategories, String[] colorNames,
    String[] sizeNames, double lowPrice, double highPrice, Sorts s, Currency currency, Langs l)
        {
            return _productFilterService.FilterFilters("", d, subCategories, colorNames, sizeNames,
                new[] { tagName }, lowPrice, highPrice, s, currency, l);
        }

        public TagDTO GetTag(string tagName,Langs l)
        {
            //var t =
            //    unitOfWork.TagDescriptionRepository.Get(
            //        td => td.LanguageId == (long) Langs.English && td.Text == tagName).FirstOrDefault().Tag.TagDescriptions.Where(tdd=>tdd.LanguageId==(long)l).FirstOrDefault();
            //TagDTO tag = new TagDTO()
            //{
            //    EnglishName = tagName,
            //    Name = t.Text
            //};
            //return tag;
            return new TagDTO();
        }
    }
}
