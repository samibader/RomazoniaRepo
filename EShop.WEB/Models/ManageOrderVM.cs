using EShop.Common;
using PagedList;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EShop.WEB.Models
{
    public class ManageOrderVM
    {
        public IPagedList<OrderVM> Orders { get; set; }
        public OrderFiltersVM filters { get; set; }

    }

    public class OrderItemVM
    {
        public long Id { get; set; }
        public long OrderId { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public int Quantity { get; set; }
        public double UnitPrice { get; set; }
        public double TotalPrice { get; set; }

    }

    public class OrderFiltersVM
    {
        [Display(Name = "معرف الطلب")]
        public long Id { get; set; }
        [Display(Name = "رقم الفاتورة")]
        public string Invoice { get; set; }

        [Display(Name = "اسم الزبون")]
        public string UserId { get; set; }

        [Display(Name = "حالة الطلب")]
        public OrderStates? Status { get; set; }

        [Display(Name = "المجموع النهائي")]
        public double? Total { get; set; }

        [Display(Name = "تاريخ الاضافة")]
        public DateTime? CreationDate { get; set; }

        [Display(Name = "تاريخ التعديل")]
        public DateTime? DateModified { get; set; }

        [Display(Name = "فرز حسب")]
        public Sorts SortBy { get; set; }

        [Display(Name = "حجم الصفحة")]
        public int PageSize { get; set; }

        public int PageNum { get; set; }

    }
    
}