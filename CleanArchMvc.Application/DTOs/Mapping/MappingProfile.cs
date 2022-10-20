using AutoMapper;
using CleanArchMvc.Domain.Entities;

namespace CleanArchMvc.Application.DTOs.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<Product, ProductDTO>().ReverseMap();
        }
        
    }
}
