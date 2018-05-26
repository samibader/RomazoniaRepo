using EShop.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.DAL.Entities
{
    public class ProductRate : IEntityBase
    {
        #region Virtual Props
        public virtual Product Product { get; set; }
        #endregion

        public long Id { get; set; }

        public long ProductId { set; get; }
        public string UserId { set; get; }


        public byte Value { get; set; }
        public string Note { get; set; }
        public bool Status { get; set; }

        public int PriceRate { get; set; }
        public int QualityRate { get; set; }
        public int ThirdRate { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? DateAdded { get; set; }
        public DateTime? DateModefied { get; set; }
        
       
       
    }

}
