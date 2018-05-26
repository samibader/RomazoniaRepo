using EShop.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.DAL.Entities
{
    public class Discount : IEntityBase
    {
        #region virtual Props

        public virtual ICollection<DiscountDescription> Descriptions { get; set; }
        public virtual ICollection<ProductDiscount> DiscountProducts { get; set; }


        #endregion

        public long Id { get; set; }

        public DateTime? DateStart { set; get; }
        public DateTime? DateEnd { set; get; }
        public DateTime? DateAdded { get; set; }
        public DateTime? DateModefied { get; set; }

        public double Value { set; get; }
        public bool IsPercentage { set; get; }

    }

}
