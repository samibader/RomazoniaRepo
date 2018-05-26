﻿using EShop.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.DAL.Entities
{
    public class AttributeDescription : IEntityBase
    {
        #region virtual Props

        public virtual Language Language { set; get; }
        public virtual Attrib Attrib { set; get; }

        #endregion

        public long Id { get; set; }
        public long LanguageId { set; get; }
        public long AttributeId { set; get; }
        public string Text { set; get; }
         public DateTime? DateAdded { get; set; }
         public DateTime? DateModefied { get; set; }
    }

}
