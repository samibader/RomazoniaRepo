using EShop.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EShop.DAL.Entities
{
    public class SizeAttributeDescription : IEntityBase
    {
        #region virtual Props
        public virtual Language Language { get; set; }

        public virtual SizeAttribute SizeAttribute { get; set; }


        #endregion

        //[Key]
        //public Int64 AttributeId { get; set; }
        public long SizeAttributeId { set; get; }
        public DateTime? DateAdded { get; set; }
        public DateTime? DateModefied { get; set; }
        public long LanguageId { get; set; }
        public string Name { get; set; }

        public long Id { get; set; }
    }
}
