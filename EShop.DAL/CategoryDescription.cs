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
    
    public partial class CategoryDescription
    {
        public long Id { get; set; }
        public long LanguageId { get; set; }
        public long CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string MetaDescription { get; set; }
        public string MetaKeyWord { get; set; }
        public Nullable<System.DateTime> DateAdded { get; set; }
        public Nullable<System.DateTime> DateModefied { get; set; }
    
        public virtual Category Category { get; set; }
        public virtual Language Language { get; set; }
    }
}