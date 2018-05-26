using EShop.BLL.DTO;
using EShop.BLL.Infrastructure;
using EShop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.BLL.Interfaces
{
    public interface IOrderService
    {
        BankInformationDTO GetBankInformation();
        OperationDetails DeleteOrder(long Id);
        OperationDetails addInvoiceNumberToOrder(string invoice, long orderId);
        OperationDetails ChangeState(OrderStates state, string desc, bool notifyUser, long orderId);
        OrderDTO GetOrderById(long id, Langs l,Currency c);
        List<OrderDTO> GetUserOrders(string userId,Langs l,Currency cu);
        OperationDetails AddOrder(string userId,string userIp,string userAgent, Langs l, Currency currency);
        OperationDetails AddOrderWithInfo(string userId,string userIp,string userAgent, Langs l, Currency currency,AddtionalInfoDTO info);
        List<OrderDTO> GetAllOrderDTO(Langs l,Currency cu);
        List<OrderDTO> GetUserOrdersByState(string userId, OrderStates state, Langs l,Currency cu);
        List<OrderDTO> Filter(long? orderId, OrderStates? state, string userId, double? total, DateTime? dateAdded, DateTime? dateModefied, Sorts s,string inv,Langs l,Currency cu);

        OperationDetails addBankInformation(BankInformationDTO dto);
    }
}
