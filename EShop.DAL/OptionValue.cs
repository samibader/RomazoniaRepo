//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EShop.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class OptionValue
    {
        public OptionValue()
        {
            this.OptionValueDescription = new HashSet<OptionValueDescription>();
            this.ProductSKUOptionValue = new HashSet<ProductSKUOptionValue>();
        }
    
        public long ProductId { get; set; }
        public long OptionId { get; set; }
        public long ValueId { get; set; }
    
        public virtual Option Option { get; set; }
        public virtual Product Product { get; set; }
        public virtual ICollection<OptionValueDescription> OptionValueDescription { get; set; }
        public virtual ICollection<ProductSKUOptionValue> ProductSKUOptionValue { get; set; }
    }
}
