using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.DAL.Entities
{
    public class AttributeGroup
    {
        #region virtual Props

        public virtual ICollection<AttributeGroupDescription> AttributeGroupDescriptions { get; set; }
        public virtual ICollection<Attrib> Attributes { get; set; }


        #endregion

        //[Key]
        //public Int64 AttributeId { get; set; }
        public int SortOrder { set; get; }
        public DateTime? DateAdded { get; set; }
        public DateTime? DateModefied { get; set; }


        public long Id { get; set; }
    }
}
