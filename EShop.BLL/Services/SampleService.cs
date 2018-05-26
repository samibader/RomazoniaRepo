using AutoMapper;
using EShop.BLL.DTO;
using EShop.BLL.Infrastructure;
using EShop.BLL.Interfaces;
using EShop.DAL.Entities;
using EShop.DAL.Interfaces;
using EShop.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.BLL.Services
{
    public class SampleService : ISampleService
    {
        IUnitOfWork unitOfWork { get; set; }

        public SampleService(IUnitOfWork uow)
        {
            unitOfWork = uow;
        }
        public OperationDetails AddSample(SampleDTO sampleDto)
        {
            var matched = unitOfWork.SampleRepository.Get(c => c.Name.ToLower() == sampleDto.Name.ToLower());
            if(matched.Count()>0)
            {
                throw new ValidationException("Name already exists !!!", "Name");
            }

            Sample sample = new Sample()
            {
                Name = sampleDto.Name
            };
            unitOfWork.SampleRepository.Insert(sample);
            unitOfWork.Save();

            return new OperationDetails(true, "Sample inserted Successfully", "");
        }


        public IEnumerable<SampleDTO> GetAll()
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Sample, SampleDTO>());
            return Mapper.Map<IEnumerable<Sample>, List<SampleDTO>>(unitOfWork.SampleRepository.Get());
        }

        public SampleDTO GetSample(long? id)
        {
            if (id == null)
                throw new ValidationException("No ID Specified", "");

            var sample = unitOfWork.SampleRepository.GetByID(id.Value);
            if (sample == null)
                throw new ValidationException("No such sample found", "");

            Mapper.Initialize(cfg => cfg.CreateMap<Sample, SampleDTO>());
            return Mapper.Map<Sample, SampleDTO>(sample);
        }

        public void Dispose()
        {
            unitOfWork.Dispose();
        }
    }
}
