using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.BLL.DTO
{
    public class CategoryPathDTO
    {
        public List<Tuple<long,Tuple<string,string> > > Path { get; set; }
        public string PathStr { get; set; }
        public long CategoryId { get; set; }
    }
}
