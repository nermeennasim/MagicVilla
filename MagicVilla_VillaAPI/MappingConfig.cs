using AutoMapper;
using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Models.DTOs;

namespace MagicVilla_VillaAPI
{
    public class MappingConfig: Profile
    {
       public  MappingConfig()
        {
            CreateMap<Villa, VillaDTO>();
            //reverse mapping
            CreateMap<VillaDTO, Villa>();

            CreateMap<VillaDTO, Villa>().ReverseMap();
            //another custom mapping
            CreateMap<Villa, VillaCreateDTO>().ReverseMap();
            CreateMap<Villa, VillaCreateDTO>();
            CreateMap<Villa,VillaUpdateDTO>().ReverseMap();
            CreateMap<Villa, VillaUpdateDTO>();

        }
    }
}
