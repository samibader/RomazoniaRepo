using EShop.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.DAL.Entities
{
    public class ProductDescription : IEntityBase
    {
        #region Virtual Props

        public virtual Product Product { get; set; }
        public virtual Language Language { get; set; }
        
        #endregion
        
        public long Id { get; set; }
        
        public long LanguageId { set; get; }
        public long ProductId { set; get; }
        
        public String Text { set; get; }
        public String Name { set; get; }
        public String MetaDescriptions { set; get; }
        public String MetaKeywords { set; get; }
       
        public DateTime? DateAdded { get; set; }
        public DateTime? DateModefied { get; set; }

    }
}
