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
    
    public partial class ProductRate
    {
        public long Id { get; set; }
        public long ProductId { get; set; }
        public byte Value { get; set; }
        public string Note { get; set; }
        public bool Status { get; set; }
        public System.DateTime CreationDate { get; set; }
        public System.DateTime DateAdded { get; set; }
        public System.DateTime DateModefied { get; set; }
    
        public virtual Product Product { get; set; }
    }
}
