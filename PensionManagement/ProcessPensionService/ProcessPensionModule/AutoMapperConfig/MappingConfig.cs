using AutoMapper;
using ProcessPensionModule.Models;
using ProcessPensionModule.Models.Dtos;

namespace ProcessPensionModule.AutoMapperConfig
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<PensionDetail, PensionDetailDto>().ReverseMap();
        }
        

    }
}
