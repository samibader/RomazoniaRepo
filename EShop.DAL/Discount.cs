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
    
    public partial class Discount
    {
        public long Id { get; set; }
        public Nullable<System.DateTime> DateStart { get; set; }
        public Nullable<System.DateTime> DateEnd { get; set; }
        public Nullable<System.DateTime> DateAdded { get; set; }
        public Nullable<System.DateTime> DateModefied { get; set; }
        public int Value { get; set; }
        public bool IsPercentage { get; set; }
        public long ProductId { get; set; }
    
        public virtual Product Product { get; set; }
    }
}
