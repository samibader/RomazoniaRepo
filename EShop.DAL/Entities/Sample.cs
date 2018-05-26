using EShop.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.DAL.Entities
{
    public class Sample : IEntityBase
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }
}
