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
    
    public partial class ProductDescription
    {
        public long Id { get; set; }
        public long LanguageId { get; set; }
        public long ProductId { get; set; }
        public string Text { get; set; }
        public string MetaDescriptions { get; set; }
        public string MetaKeywords { get; set; }
        public System.DateTime DateAdded { get; set; }
        public System.DateTime DateModefied { get; set; }
    
        public virtual Language Language { get; set; }
        public virtual Product Product { get; set; }
    }
}
