using EShop.BLL.DTO;
using EShop.BLL.Infrastructure;
using EShop.BLL.Interfaces;
using EShop.DAL.Entities;
using EShop.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.BLL.Services
{
    public class AddressService:IAddressService
    {
        IUnitOfWork unitOfWork { get; set; }
    
        public AddressService(IUnitOfWork uow)
        {
            unitOfWork = uow;
            
        }
        public AddressDTO GetDefaultUserAddress(string userId)
        {
            return GetUserAddresses(userId).Where(c => c.IsDefault == true).FirstOrDefault() ;
        }

        public List<AddressDTO> GetUserAddresses(string userId)
        {
            List<long> addressIds = unitOfWork.AddressRepository.Get(c => c.UserId == userId).Select(c=>c.Id).ToList();
            List<AddressDTO> addressesDTO = new List<AddressDTO>();
            foreach (var addressId in addressIds)
            {
                addressesDTO.Add(GetAddressById(addressId));
            }
            return addressesDTO;
        }
        public AddressDTO GetAddressById(long addressId)
        {
            AddressDTO addressDTO = new AddressDTO();
            Address address = unitOfWork.AddressRepository.GetByID(addressId);
            addressDTO.Id = address.Id;
            addressDTO.Address1 = address.Address1;
            addressDTO.City = address.City;
            addressDTO.Country = address.Country;
            addressDTO.FirstName = address.FirstName;
            addressDTO.LastName = address.LastName;
            addressDTO.Phone = address.Phone;
            addressDTO.PostCode = address.PostCode;
            addressDTO.IsDefault = address.IsDefault;

            return addressDTO;

        }
        public OperationDetails AddAddress(String userId, AddressDTO addressDTO)
        {
            Address address = new Address();
            address.Address1 = addressDTO.Address1;
            address.City = addressDTO.City;
            address.Country = addressDTO.Country;
            address.FirstName = addressDTO.FirstName;
            address.LastName = addressDTO.LastName;
            address.Phone = addressDTO.Phone;
            address.PostCode = addressDTO.PostCode;
            address.UserId = userId;
            address.IsDefault = !unitOfWork.AddressRepository.Get(ad => ad.UserId==userId).Any()?true:addressDTO.IsDefault;
            address = unitOfWork.AddressRepository.Insert(address);
            unitOfWork.Save();
            if (addressDTO.IsDefault == true || unitOfWork.AddressRepository.Get(c => c.Id == c.Id).ToList().Count == 1)
            {
                SetDefaultAddress(address.Id);
            }
            return new OperationDetails(true, address.Id.ToString(), "Id");
        }
        public bool SetDefaultAddress(long addressId)
        {
            string userId = unitOfWork.AddressRepository.GetByID(addressId).UserId;
            List<Address> addresses = unitOfWork.AddressRepository.Get(c => c.UserId == userId).ToList();
            foreach (var address in addresses)
            {
                if (address.Id == addressId)
                    address.IsDefault = true;
                else
                    address.IsDefault = false;
            }
            unitOfWork.Save();
            return true;
        }
        public OperationDetails EditAddress(long addressId, AddressDTO addressDTO)
        {
            Address address = unitOfWork.AddressRepository.GetByID(addressId);
            if(address==null)
                return new OperationDetails(false, "لا يوجد هكذا عنوان", "");

            address.Address1 = addressDTO.Address1;
            address.City = addressDTO.City;
            address.Country = addressDTO.Country;
            address.FirstName = addressDTO.FirstName;
            address.LastName = addressDTO.LastName;
            address.Phone = addressDTO.Phone;
            address.PostCode = addressDTO.PostCode;
            address.IsDefault = address.IsDefault?address.IsDefault : addressDTO.IsDefault;
            unitOfWork.AddressRepository.Update(address);
            unitOfWork.Save();
            if (addressDTO.IsDefault == true || unitOfWork.AddressRepository.Get(c => c.Id == c.Id).ToList().Count == 1)
                SetDefaultAddress(addressDTO.Id);
            return new OperationDetails(true, "تم اضافة عنوان للمستخدم", "");
        }
        public OperationDetails RemoveAddress(long addressId)
        {
            Address address = unitOfWork.AddressRepository.GetByID(addressId);
            if (address == null)
            {
                return new OperationDetails(false, "حدث خطأ أثناء الحذف", "");
            }
            
            unitOfWork.AddressRepository.Delete(address);
            unitOfWork.Save();
            return new OperationDetails(true, "تم الحذف بنجاح", "");
        }

    }
}