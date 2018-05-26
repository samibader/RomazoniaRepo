using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.DAL.Entities
{
    public class ProductSKUOptionValue
    {
        [Key]

        [Column(Order = 1)]
        public long ProductId { get; set; }
        [Key]
        [Column(Order = 2)]
        public long SKUId { get; set; }
        [Key]
        [Column(Order = 3)]
        public long OptionId { get; set; }
        [Key]
        [Column(Order = 4)]
        public long ValueId { get; set; }

        public virtual Product Product { get; set; }
        public virtual ProductSKU ProductSKU { get; set; }
        public virtual Option Option { get; set; }
        public virtual OptionValue OptionValue { get; set; }
    }
}
