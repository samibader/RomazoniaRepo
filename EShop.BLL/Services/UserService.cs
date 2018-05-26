using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EShop.BLL.DTO;
using EShop.BLL.Infrastructure;
using EShop.BLL.Interfaces;
using EShop.DAL.Entities;
using EShop.DAL.Interfaces;
using Microsoft.AspNet.Identity;
using EShop.DAL.EF;

using EShop.Common;

namespace EShop.BLL.Services
{
    public class UserService : IUserService
    {
        private IUnitOfWork _unitOfWork;
        private ApplicationDbContext db;
        public UserService(IUnitOfWork uow)
        {
            _unitOfWork = uow;
            db = new ApplicationDbContext();
        }
        public List<UserDTO> GetAllUsers()
        {
            List<ApplicationUser> users = db.Users.ToList();
            var usersDtos = new List<UserDTO>();
            foreach (var user in users)
            {
                var userDto = new UserDTO();
                userDto.Email = user.Email;
                userDto.FirstName = user.FirstName;
                userDto.LastName = user.LastName;
                userDto.Id = user.Id;
                userDto.UserName = user.UserName;
                usersDtos.Add(userDto);
            }
            return usersDtos;
        }
        public List<UserDTO> Filter(string fName, string email, Sorts s)
        {
            List<string> usersIds = db.Users.ToList().Select(c => c.Id).ToList();

            if (!String.IsNullOrEmpty(fName))
            {
                List<string> ids = db.Users.ToList().Where(c => (c.FirstName + " " + c.LastName).ToLower().Contains(fName.ToLower())).Select(c => c.Id).ToList(); ;
                usersIds = usersIds.Intersect(ids).ToList();
            }
            if (!String.IsNullOrEmpty(email))
            {
                List<string> ids = db.Users.ToList().Where(c => c.Email.ToLower().Contains(email.ToLower())).Select(c => c.Id).ToList(); ;
                usersIds = usersIds.Intersect(ids).ToList();
            }
            var users = db.Users.ToList().Where(c => usersIds.Contains(c.Id)).ToList();

            var usersDtos = new List<UserDTO>();
            foreach (var user in users)
            {
                var userDto = new UserDTO();
                userDto.Email = user.Email;
                userDto.FirstName = user.FirstName;
                userDto.LastName = user.LastName;
                userDto.AccountLock = user.LockoutEnabled;
                userDto.AccountStatus = user.EmailConfirmed;
                userDto.Id = user.Id;
                userDto.UserName = user.UserName;
                usersDtos.Add(userDto);
            }
            // return usersDtos;
            sortUsers(ref usersDtos, s);
            return usersDtos;
        }
        private void sortUsers(ref List<UserDTO> users, Sorts s)
        {
            switch (s)
            {
                case Sorts.EmailDown: users.Sort((a, b) => -1 * a.Email.CompareTo(b.Email));
                    break;
                case Sorts.EmailUp: users.Sort((a, b) => a.Email.CompareTo(b.Email));
                    break;
                case Sorts.CustomerNameDown: users.Sort((a, b) => -1 * a.FullName.CompareTo(b.FirstName));
                    break;
                case Sorts.CustomerNameUp: users.Sort((a, b) => a.FullName.CompareTo(b.FirstName));
                    break;
                case Sorts.StatusUp: users.Sort((a, b) => a.AccountStatus.CompareTo(b.AccountStatus)); //products.OrderBy(op => op.Price);
                    break;
                case Sorts.StatusDown: users.Sort((a, b) => -1 * a.AccountStatus.CompareTo(b.AccountStatus));
                    break;
                case Sorts.lockedUp: users.Sort((a, b) => a.AccountLock.CompareTo(b.AccountLock)); //products.OrderBy(op => op.Price);
                    break;
                case Sorts.lockedDown: users.Sort((a, b) => -1 * a.AccountLock.CompareTo(b.AccountLock));
                    break;


            }

        }

        public UserDTO getuserById(string Id)
        {
            var user = db.Users.ToList().Where(c => c.Id == Id).FirstOrDefault();
            var userDto = new UserDTO();
            userDto.Email = user.Email;
            userDto.FirstName = user.FirstName;
            userDto.LastName = user.LastName;
            userDto.AccountLock = user.LockoutEnabled;
            userDto.AccountStatus = user.EmailConfirmed;
            userDto.Id = user.Id;
            userDto.UserName = user.UserName;

            return userDto;
        }

        public OperationDetails EditPersonalInformations(string userId, string firstName, string lastName)
        {
            var user = db.Users.ToList().Where(c => c.Id == userId).FirstOrDefault();
            if (user == null)
                return new OperationDetails(false, "المستخدم غير موجود", "");

            user.FirstName = firstName;
            user.LastName = lastName;
            db.Users.Attach(user);
            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();
            return new OperationDetails(true, "تمت عملية التعديل بنجاح", "");
        }

        public OperationDetails EditLoginInformations(string userId, string Email)
        {
            var user = db.Users.ToList().Where(c => c.Id == userId).FirstOrDefault();
            if (user == null)
                return new OperationDetails(false, "المستخدم غير موجود", "");

            user.Email = Email;
            user.UserName = Email;
            db.Users.Attach(user);
            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();
            return new OperationDetails(true, "تمت عملية التعديل بنجاح", "");
        }

        public OperationDetails DeleteUserData(string UserId)
        {
            var userFavourites = _unitOfWork.FavoriteRepository.Get(c => c.UserId == UserId).ToList();
            var userOrders = _unitOfWork.OrderRepository.Get(c => c.UserId == UserId).ToList();
            var userCheckouts = _unitOfWork.CheckOutRepository.Get(c => c.User.Id == UserId).ToList();
            var userAddresses = _unitOfWork.AddressRepository.Get(c => c.UserId == UserId).ToList();
            //var userBillinAddresses = _unitOfWork.BillingAddressRepository.Get(c => c.UserId == UserId).ToList();
            //var userShippingAddresses = _unitOfWork.ShippingAddressRepository.Get(c => c.UserId == UserId).ToList();


            foreach (var fav in userFavourites)
            {
                _unitOfWork.FavoriteRepository.Delete(fav);
                _unitOfWork.Save();
            }

            foreach (var check in userCheckouts)
            {
                _unitOfWork.CheckOutRepository.Delete(check);
                _unitOfWork.Save();
            }

            foreach (var address in userAddresses)
            {
                _unitOfWork.AddressRepository.Delete(address);
                _unitOfWork.Save();
            }

            //foreach (var userShipAdd in userShippingAddresses)
            //{
            //    _unitOfWork.ShippingAddressRepository.Delete(userShipAdd);
            //    _unitOfWork.Save();
            //}

            //foreach (var userBillingAdd in userBillinAddresses)
            //{
            //    _unitOfWork.BillingAddressRepository.Delete(userBillingAdd);
            //    _unitOfWork.Save();
            //}

            foreach (var order in userOrders)
            {
                var orderHistories = _unitOfWork.OrderHistoryRepository.Get(c => c.OrderId == order.Id).ToList();
                var orderItems = _unitOfWork.OrderItemRepository.Get(c => c.OrderId == order.Id).ToList();
                _unitOfWork.ShippingAddressRepository.Delete(order.ShippingAddress);
                _unitOfWork.Save();
                _unitOfWork.BillingAddressRepository.Delete(order.BillingAddress);
                _unitOfWork.Save();
                
                foreach (var history in orderHistories)
                {
                    _unitOfWork.OrderHistoryRepository.Delete(history);
                    _unitOfWork.Save();
                }
                foreach (var item in orderItems)
                {
                    _unitOfWork.OrderItemRepository.Delete(item);
                    _unitOfWork.Save();
                }
                _unitOfWork.OrderRepository.Delete(order);
                _unitOfWork.Save();
            }

            return new OperationDetails(true, "تمت عملية الحذف بنجاح", "");

        }

        public OperationDetails LockUser(string id,bool lockIt)
        {
            ApplicationUser user = db.Users.ToList().Where(c => c.Id == id).First();

            if(user == null)
                return new OperationDetails(false,"هذا المستخدم غير موجود","");

            user.LockoutEnabled = lockIt;
            db.SaveChanges();

            return new OperationDetails(true, "تم قفل الحساب بشكل صحيح", "");
            
           
        }
    }
}
