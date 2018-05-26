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
    public class Image:IEntityBase
    {
        #region virtual Props

        public virtual Product Product { set; get; }
        public virtual OptionValue OptionValue { set; get; }

        #endregion
       
        public long Id { get; set; }
       
        public String ImageUrl { set; get;}
        public String ThumbUrl { set; get; }
        public String AlternateText { set; get; }

        public long? ProductId { get; set; }

        public long? DesignerId { get; set; }
        public long? CategoryId { get; set; }
        
        [ForeignKey("OptionValue")]
        [Column(Order = 1)]
        public long? OptionValueProductId { get; set; }
      
        [ForeignKey("OptionValue")]
        [Column(Order = 2)]
        public long? OptionValueOptionId { get; set; }
        
        [ForeignKey("OptionValue")]
        [Column(Order = 3)]
        public long? OptionValueValueId { get; set; }
       
         public DateTime? DateAdded { get; set; }
         public DateTime? DateModefied { get; set; }
        


    }
}
