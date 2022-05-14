using AutoMapper;
using PensionerDetailModule.Models;
using PensionerDetailModule.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PensionerDetailModule.Utility.AutoMapperConfig
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Pensioner, PensionerDto>().ReverseMap();
            CreateMap<Bank, BankDto>().ReverseMap();
            CreateMap<Pensioner, PensionerDetailDto>().ReverseMap();
        }
        

    }
}
