using AutoMapper;
using PensionerDetailModule.Models;
using PensionerDetailModule.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PensionerDetailModule.AutoMapperConfig
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<PensionerDetail, PensionerDetailDto>().ReverseMap();
            CreateMap<BankDetail, BankDetailDto>().ReverseMap();
        }
        

    }
}
