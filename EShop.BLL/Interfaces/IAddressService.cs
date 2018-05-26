using EShop.BLL.DTO;
using EShop.BLL.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.BLL.Interfaces
{
    public interface IAddressService
    {
        AddressDTO GetAddressById(long addressId);
        List<AddressDTO> GetUserAddresses(string userId);
        AddressDTO GetDefaultUserAddress(string userId);
        bool SetDefaultAddress(long addressId);
        OperationDetails AddAddress(String userId, AddressDTO addressDTO);
        OperationDetails EditAddress(long addressId, AddressDTO addressDTO);
        OperationDetails RemoveAddress(long addressId);
    }
}
