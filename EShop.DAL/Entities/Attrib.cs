using EShop.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.DAL.Entities
{
    public class Attrib:IEntityBase
    {
        #region virtual Props

        public virtual ICollection<AttributeDescription> AttributeDescriptions { get; set; }
        public virtual ICollection<ProductAttributeValue> ProductAttributeValues { get; set; }
        public virtual AttributeGroup Group { get; set; }


        #endregion

        //[Key]
        //public Int64 AttributeId { get; set; }
        public int SortOrder { set; get; }
         public DateTime? DateAdded { get; set; }
         public DateTime? DateModefied { get; set; }
         public long GroupId { get; set; }


        public long Id { get; set; }
    }
}
