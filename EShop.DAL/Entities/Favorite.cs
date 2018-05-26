using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.DAL.Entities
{
    public class Favorite
    {
        public int Id { get; set; }

        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        [ForeignKey("ProductSKU")]
        [Column(Order = 1)]
        public long ProductId { get; set; }
        [ForeignKey("ProductSKU")]
        [Column(Order = 2)]
        public long SKUId { get; set; }

        public virtual ProductSKU ProductSKU { get; set; }
    }
}
