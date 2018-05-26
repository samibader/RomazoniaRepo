using EShop.BLL.DTO;
using EShop.BLL.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.BLL.Interfaces
{
    public interface ISampleService
    {
        OperationDetails AddSample(SampleDTO sampleDto);
        IEnumerable<SampleDTO> GetAll();
        SampleDTO GetSample(long? id);
        void Dispose();
    }
}
