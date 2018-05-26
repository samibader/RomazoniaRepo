using EShop.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.DAL.Entities
{
    public class Designer : IEntityBase
    {
        #region virtual Props

        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<DesignerDescription> Descriptions { get; set; }
        

        #endregion
        
        public long Id { get; set; }
       
        public String Name { set; get; }
        public String ImageUrl { get; set; }
        public String ImageThumb { get; set; }

         public DateTime? DateAdded { get; set; }
         public DateTime? DateModefied { get; set; }
        
        
    }
}
