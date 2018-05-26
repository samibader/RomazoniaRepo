using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.DAL.Entities
{
    public class OptionDescription
    {
        [Key]
        [ForeignKey("Option")]
        [Column(Order = 1)]
        public long ProductId { get; set; }
        [Key]
        [ForeignKey("Option")]
        [Column(Order = 2)]
        public long OptionId { get; set; }
        [Key]
        [Column(Order = 3)]
        public long LanguageId { get; set; }
        public string Name { get; set; }

        public virtual Option Option { get; set; }
        public virtual Language Language { get; set; }
        public virtual Product Product { get; set; }

        
    }
}
