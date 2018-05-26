using EShop.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.DAL.Entities
{
    public class SizeAttribute : IEntityBase
    {
        #region virtual Props

        public virtual ICollection<SizeAttributeDescription> SizeAttributeDescriptions { get; set; }
        public virtual OptionHelper SizeCategory { get; set; }
        #endregion

        public DateTime? DateAdded { get; set; }
        public DateTime? DateModefied { get; set; }
        public long Id { get; set; }
        public long SizeCatId { get; set; }

    }
}
