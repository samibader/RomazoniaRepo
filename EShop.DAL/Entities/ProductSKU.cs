using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.DAL.Entities
{



    public class ProductSKU
    {
        #region virtual Props
        public virtual ICollection<ProductDiscount> ProductSkuDiscounts { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }
        
        #endregion


        [Key]
        [Column(Order = 1)]
        public long ProductId { get; set; }
        [Key]
        [Column(Order = 2)]
        public long SKUId { get; set; }
        public string SKU { get; set; }
        public int Price { get; set; }
        public int Quentity { get; set; }

        public virtual Product Product { get; set; }
        public virtual ICollection<ProductSKUOptionValue> ProductSKUOptionValues { get; set; }
        public virtual ICollection<ShoppingCart> ShoppingCarts { get; set; }

    }

}
