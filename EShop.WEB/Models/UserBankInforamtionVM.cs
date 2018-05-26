using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EShop.WEB.Models
{
    public class UserBankInforamtionVM
    {
        [Required(ErrorMessage="مطلوب !")]
        public double Amount { get; set; }
        [Required(ErrorMessage = "مطلوب !")]
        public String AutherName { get; set; }
        [Required(ErrorMessage = "مطلوب !")]
        public String AccountNumber { get; set; }
        [Required(ErrorMessage = "مطلوب !")]
        public String BankName { get; set; }
        [Required(ErrorMessage = "مطلوب !")]
        public String TransferDate { get; set; }
    }
}