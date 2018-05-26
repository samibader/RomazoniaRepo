using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.DAL.Entities
{
    public class ProductDiscount
    {
        #region virtual Props

        public virtual  Discount Discount { get; set; }
        public virtual ProductSKU ProductSKU { get; set; }

        #endregion

        public long Id { get; set; }

        public long DiscountId { get; set; }

        public long SKUId { get; set; }
        public long ProductId { get; set; }

        public DateTime? DateAdded { get; set; }
        public DateTime? DateModefied { get; set; }

    }
}
