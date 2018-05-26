using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EShop.DAL.Entities
{
    public class SizeAttributeMedian
    {
        public long SizeAttributeId { set; get; }
        public long SizeValueId { set; get; }
        public int Value { set; get; }
        public DateTime? DateAdded { get; set; }
        public DateTime? DateModefied { get; set; }

        public long Id { get; set; }
    }
}
