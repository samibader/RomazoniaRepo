using EShop.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.DAL.Entities
{
    public class TagDescription:IEntityBase
    {
        #region Virtual Propss

        public virtual Language Language { get; set; }
        public virtual Tag Tag { set; get; }
        
        #endregion
      
        public long Id { get; set; }
        public String Text { set; get; }
        public long LanguageId { get; set; }
        public long TagId { get; set; }
       
        public DateTime? DateAdded { get; set; }
        public DateTime? DateModefied { get; set; }
        
    }
}
