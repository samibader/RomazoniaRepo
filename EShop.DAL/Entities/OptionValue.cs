using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.DAL.Entities
{
    public class OptionValue
    {
        [Key]
        [Column(Order = 1)]
        public long ProductId { get; set; }
        [Key]
        [Column(Order = 2)]
        public long OptionId { get; set; }
        [Key]
        [Column(Order = 3)]
        public long ValueId { get; set; }
        public string Value { get; set; }
        public string ImageUrl { get; set; }
        public virtual Option Option { get; set; }
        public virtual Product Product { get; set; }
        public virtual ICollection<OptionValueDescription> OptionValueDescriptions { get; set; }
        public virtual ICollection<Image> Images { get; set; }
        public virtual ICollection<ProductSKUOptionValue> ProductSKUOptionValues { get; set; }


    }

}
