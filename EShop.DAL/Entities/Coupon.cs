using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.DAL.Entities
{
    public class Coupon
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int Value { get; set; }
        public bool  IsPercentage { get; set; }
        public bool IsActive { get; set; }
        public int MaxNumOfUsage { get; set; }
    }
}
