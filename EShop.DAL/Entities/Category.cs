using EShop.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.DAL.Entities
{
    public class Category : IEntityBase
    {
        #region Virtual Props

        public virtual ICollection<CategoryDescription> CategoryDescriptions { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual Category ParentCategory { get; set; }
       

        #endregion
       
        public long Id { get; set; }

        public long? ParentId { get; set; }
        public String ImageUrl { get; set; }
        public String Banner { get; set; }
        public String ImageThumb { get; set; }

        public int SortOrder { get; set; }
        public bool Status { get; set; }
       
         public DateTime? DateAdded { get; set; }
         public DateTime? DateModified { get; set; }
        
    }
}
