using EShop.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.DAL.Entities
{
    public class Language : IEntityBase
    {
        #region Virtual Properties
        public virtual ICollection<CategoryDescription> CategoryDescriptions { get; set; }
        public virtual ICollection<AttributeDescription> AttributeDescriptions { get; set; }
        public virtual ICollection<ProductAttributeValue> ProductAttributeValues { get; set; }
       // public virtual ICollection<TagDescription> TagDescriptions { get; set; }
        public virtual ICollection<ProductDescription> ProductDescriptions { get; set; }
       

        #endregion
        
        public long Id { get; set; }

        public String ImageUrl { get; set; }
        public String ImageThumb { get; set; }

        
        public string Name { get; set; }
        public int Status { get; set; }
        public string Code { get; set; }
        public int SortOrder { get; set; }
        
         public DateTime? DateAdded { get; set; }
         public DateTime? DateModefied { get; set; }
    }
}
