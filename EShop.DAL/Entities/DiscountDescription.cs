using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.DAL.Entities
{
    public class DiscountDescription
    {
        #region virtual Props

        public virtual Language Language { set; get; }
        public virtual Discount Discount { set; get; }

        #endregion

        public long Id { get; set; }
        public long LanguageId { set; get; }
        public long DiscountId { set; get; }
        public string Name { set; get; }
        public DateTime? DateAdded { get; set; }
        public DateTime? DateModefied { get; set; }
    }
}
