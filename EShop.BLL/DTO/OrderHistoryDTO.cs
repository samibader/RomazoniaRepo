using EShop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.BLL.DTO
{
    public class OrderHistoryDTO
    {
        public long Id { get; set; }
        public long OrderId { get; set; }
        public OrderStates State { get; set; }
        public DateTime? DateAdded { get; set; }
        public string Description { get; set; }
        public bool NotifyUser { get; set; }
    }
}
