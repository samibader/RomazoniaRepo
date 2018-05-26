using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EShop.BLL.DTO;
using EShop.BLL.Infrastructure;
using EShop.Common;

namespace EShop.BLL.Interfaces
{
    public interface IUserService
    {
        OperationDetails LockUser(string id, bool lockIt);
        List<UserDTO> GetAllUsers();
        List<UserDTO> Filter(string fName, string email, Sorts s);
        OperationDetails EditPersonalInformations(string userId, string firstName, string lastName);
        OperationDetails EditLoginInformations(string userId, string Email);
        UserDTO getuserById(string Id);
    }
}
