using EShop.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.DAL.Entities
{
    public class Product : IEntityBase
    {
        #region Virtual Props
      
        public virtual Designer Designer { set; get; }
        public virtual Category Category { set; get; }
        public virtual ICollection<Tag> Tags { set; get; }

        public virtual ICollection<ProductSizeAttribute> SizeAttributes { set; get; }


        public virtual ICollection<Image> Images { set; get; }
        public virtual ICollection<Option> Options { set; get; }
        public virtual ICollection<ProductSKU> ProductSKUs { get; set; }
        public virtual ICollection<ProductRate> ProductRates { get; set; }
        public virtual ICollection<ProductDescription> ProductDescriptions { get; set; }
        public virtual ICollection<ProductAttributeValue> ProductAttributeValues { get; set; }
        

        #endregion
      
        public long Id { get; set; }
        public double Price { get; set; }
        public long CategoryId { get; set; }
        public long DesignerId { get; set; }
        public int Minimum { get; set; }
        public bool Status { get; set; }
        public long NumberOfViews { get; set; }

        public string Comment { get; set; }
        public DateTime? DateAdded { get; set; }
        public DateTime? DateModefied { get; set; }
    }
}
