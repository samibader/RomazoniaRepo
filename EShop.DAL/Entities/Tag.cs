using EShop.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.DAL.Entities
{
    public class Tag:IEntityBase
    {
        #region Virtual Props
        public virtual Language Language { get; set; }
        public virtual ICollection<ProductSKU> ProductSKUs { get; set; }
       
        
        #endregion

        public long Id { get; set; }
        public String Text { set; get; }
        public long LanguageId { get; set; }
        public DateTime? DateAdded { get; set; }
        public DateTime? DateModefied { get; set; }
       
        

    }
}
