using AutoMapper;
using ValeShop.Models;
using ValeShop.ViewModels;

namespace ValeShop.Automapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserViewModel, User>().ReverseMap();
            CreateMap<CategoryViewModel, Category>().ReverseMap();
            CreateMap<ProductViewModel, Product>().ReverseMap();
        }
    }
}