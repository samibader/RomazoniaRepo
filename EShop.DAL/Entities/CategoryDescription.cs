using EShop.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.DAL.Entities
{
    public class CategoryDescription :IEntityBase
    {
        #region Virtual properties
        public virtual Category Category { get; set; }
        public virtual Language Language { get; set; }
        #endregion
      
        public long Id { get; set; }
        public long LanguageId { get; set; }
        public long CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string MetaDescription { get; set; }
        public string MetaKeyWord { get; set; }
        public DateTime? DateAdded { get; set; }
        public DateTime? DateModefied { get; set; }
    }
}
