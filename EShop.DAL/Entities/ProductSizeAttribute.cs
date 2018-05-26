using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.DAL.Entities
{
   public class ProductSizeAttribute
    {

       public virtual Product Product { get; set; }
       public long Id { get; set; }
        public long ProductId { get; set; }
        public long SizeAttributeId { get; set; }
        public long SizeCategoryId { get; set; }
        public long ValueId { get; set; }
        public double Value { get; set; }
        public DateTime? DateAdded { get; set; }
        public DateTime? DateModified { get; set; }

    }
}
