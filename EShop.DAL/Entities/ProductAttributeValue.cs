using EShop.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.DAL.Entities
{
    public class ProductAttributeValue : IEntityBase
    {
        #region Virtual Props
        public virtual Product Product { get; set; }
        public virtual Attrib Attrib { get; set; }
        public virtual Language Language { get; set; }

       
        #endregion
        
        public long Id { get; set; }
        public long LanguageId { get; set; }
        
        public long ProductId { set; get; }
        public long AttributeId { set; get; }
        public string Text { set; get; }
        
        
    }

}
