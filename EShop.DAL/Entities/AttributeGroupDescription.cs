using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EShop.DAL.Entities
{
    public class AttributeGroupDescription
    {
        #region virtual Props

        public virtual Language Language { set; get; }
        public virtual AttributeGroup Group { set; get; }

        #endregion

        public long Id { get; set; }
        public long LanguageId { set; get; }
        public long GroupId { set; get; }
        public string Text { set; get; }
        public DateTime? DateAdded { get; set; }
        public DateTime? DateModefied { get; set; }
    }
}
