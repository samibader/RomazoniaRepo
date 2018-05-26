using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EShop.WEB.Simulation_Data
{
    public class ProductDTO
    {
        public long Id { get; set; }
        public long SkuId { get; set; }
        public string Name { get; set; }
        public double OriginalPrice { get; set; }
        public double DiscountPrice { get; set; }
        public double TotalPrice { get; set; }
        public bool HasDiscount { get; set; }
        public bool IsNew { get; set; }
        public bool IsAvailable { get; set; }
        public String MetaDiscription { get; set; }
        public String Text { get; set; }
        public byte Rate { get; set; }
        public int Quantity { get; set; }
        public List<String> Images { get; set; }
        public List<ColorValuesDTO> ProductColors;
        public List<SizeValueDTO> ProductSizes;
        public long SelectedColorValue { get; set; }
        public long SelectedSizeValue { get; set; }
        

        //Size And Color
        public ProductDTO(long id,String c,String s)
        {
            Id = id;
            SkuId = id*10;
            Name = "قميص رجالي شاروخان " + Id.ToString();
            OriginalPrice = 100;
            DiscountPrice = 50;
            HasDiscount = true;
            IsNew = true;
            IsAvailable = false;
            SelectedColorValue = 1;
            MetaDiscription = "أفضل الألبسة على الاطلاق";
            Text = "بنطال جنز شكسمينةشكمسينشمسنيتشسمينكتشكمينتكمشسنيت";
            Rate = 5;

            Images = new List<string>()
            {
                 "/Assets/images/products/img10.jpg","/Assets/images/products/img11.jpg","/Assets/images/products/img12.jpg"
            };
            ProductColors = new List<ColorValuesDTO>()
            {
                
                new ColorValuesDTO()
                {
                    OptionId = 1,
                    ValueId = 1,
                    ColorName = c,
                    Image = "#ff0000",
                    IsImages = false,
                    ProductImage = "/Assets/images/products/img10.jpg"

                },
                                new ColorValuesDTO()
                {
                    OptionId = 1,
                    ValueId = 2,
                    ColorName = "أصفر",
                    Image = "#0ff000",
                    IsImages = false,
                    ProductImage = "/Assets/images/products/img11.jpg"
                }
            };
            SelectedSizeValue = 2;
            ProductSizes = new List<SizeValueDTO>()
            {
                new SizeValueDTO()
                {
                    Available = false,
                    Name = s,
                    OptionId = 2,
                    OptionValueId = 1,
                   
                },
                new SizeValueDTO()
                {
                    Available = true,
                    Name = "كبير",
                    OptionId = 2,
                    OptionValueId = 2,
                   
                }
            };
        }
        //Color only 
        public ProductDTO(long id, String c)
        {
            Id = id;
            SkuId = id * 10;
            Name = "قميص رجالي شاروخان " + Id.ToString();
            OriginalPrice = 100;
            DiscountPrice = 50;
            HasDiscount = true;
            IsNew = true;
            IsAvailable = false;
            SelectedColorValue = 1;
            MetaDiscription = "أفضل الألبسة على الاطلاق";
            Text = "بنطال جنز شكسمينةشكمسينشمسنيتشسمينكتشكمينتكمشسنيت";
            Rate = 5;

            Images = new List<string>()
            {
                 "/Assets/images/products/img10.jpg","/Assets/images/products/img11.jpg","/Assets/images/products/img12.jpg"
            };
            ProductColors = new List<ColorValuesDTO>()
            {
                
                new ColorValuesDTO()
                {
                    OptionId = 1,
                    ValueId = 1,
                    ColorName = c,
                    Image = "#ff0000",
                    IsImages = false,
                    ProductImage = "/Assets/images/products/img10.jpg"

                },
                                new ColorValuesDTO()
                {
                    OptionId = 1,
                    ValueId = 2,
                    ColorName = "أصفر",
                    Image = "#0ff000",
                    IsImages = false,
                    ProductImage = "/Assets/images/products/img11.jpg"
                }
            };
            SelectedSizeValue = -1;
            ProductSizes =new List<SizeValueDTO>();
        }
        //SizeOnly
        public ProductDTO(long id, String s,long dummy)
        {
            Id = id;
            SkuId = id * 10;
            Name = "قميص رجالي شاروخان " + Id.ToString();
            OriginalPrice = 100;
            DiscountPrice = 50;
            HasDiscount = true;
            IsNew = true;
            IsAvailable = false;
           
            MetaDiscription = "أفضل الألبسة على الاطلاق";
            Text = "بنطال جنز شكسمينةشكمسينشمسنيتشسمينكتشكمينتكمشسنيت";
            Rate = 5;

            Images = new List<string>()
            {
                 "/Assets/images/products/img10.jpg","/Assets/images/products/img11.jpg","/Assets/images/products/img12.jpg"
            };
            SelectedColorValue = -1;
            ProductColors = new List<ColorValuesDTO>();
            SelectedSizeValue = 2;
            ProductSizes = new List<SizeValueDTO>()
            {
                new SizeValueDTO()
                {
                    Available = true,
                    Name = s,
                    OptionId = 2,
                    OptionValueId = 1,
                   
                },
                new SizeValueDTO()
                {
                    Available = false,
                    Name = "كبير",
                    OptionId = 2,
                    OptionValueId = 2,
                   
                }
            };
        }
       //Nothing
        public ProductDTO(long id)
        {
            Id = id;
            SkuId = id * 10;
            Name = "قميص رجالي شاروخان " + Id.ToString();
            OriginalPrice = 100;
            DiscountPrice = 50;
            HasDiscount = true;
            IsNew = true;
            IsAvailable = false;

            MetaDiscription = "أفضل الألبسة على الاطلاق";
            Text = "بنطال جنز شكسمينةشكمسينشمسنيتشسمينكتشكمينتكمشسنيت";
            Rate = 5;

            Images = new List<string>()
            {
                 "/Assets/images/products/img10.jpg","/Assets/images/products/img11.jpg","/Assets/images/products/img12.jpg"
            };
            SelectedColorValue = -1;
            ProductColors = new List<ColorValuesDTO>();
            SelectedSizeValue = -1;
            ProductSizes = new List<SizeValueDTO>();
        }
    }
}