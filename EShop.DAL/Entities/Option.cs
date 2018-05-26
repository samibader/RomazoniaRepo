using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.DAL.Entities
{
    public class Option
    {
        [Key]
        [Column(Order=1)]
        public long ProductId { get; set; }
        [Key]
        [Column(Order = 2)]
        public long OptionId { get; set; }
        public bool IsColor { get; set; }

        public virtual ICollection<OptionDescription> OptionDescriptions { get; set; }
        public virtual ICollection<OptionValue> OptionValues { get; set; }
        public virtual Product Product { get; set; }

        public virtual ICollection<ProductSKUOptionValue> ProductSKUOptionValues { get; set; }
    }
}
