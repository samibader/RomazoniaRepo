﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.BLL.DTO
{
    public class OptionValueManagerDTO
    {
        public long OptionId { get; set; }
        public long Id { get; set; }
        public string  ArabicName { get; set; }
        public string ImageURL { get; set; }

        public string EnglishName { get; set; }
        public string Value { get; set; }

    }
}
