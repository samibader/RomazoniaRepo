using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.BLL.DTO
{
    public class CategoryManegerDTO
    {
        public long Id { get; set; }
        public int SortOrder { get; set; }
        public bool Status { get; set; }
        public String EnglishName { set; get; }
        public String ArabicName { set; get; }
        public String EnglishDescription { set; get; }
        public String ArabicDescription { set; get; }
        public String EnglishMetaDescription { set; get; }
        public String ArabicMetaDescription { set; get; }
        public long ParentId { set; get; }

        public String ArabicPath { get; set; }
        public String EnglishPath { get; set; }
        public String ImageUrl { get; set; }
        public String BannerImage { get; set; }

        public String ImageThumb { get; set; }
        public DateTime? DateCreation { get; set; }
        public DateTime? DateModefied { get; set; }
    }
}
