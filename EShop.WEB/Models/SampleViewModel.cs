using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EShop.WEB.Models
{
    public class SampleViewModel
    {
        [Display(Name="Sample ID")]
        public long Id { get; set; }

        [Display(Name="Sample Name")]
        [Required(ErrorMessage="{0} Required !")]
        public string Name { get; set; }

    }
}