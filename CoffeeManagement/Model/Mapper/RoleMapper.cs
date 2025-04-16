using AutoMapper;
using CoffeeManagement.ResponseModel.Role;

namespace CoffeeManagement.Model.Mapper
{
    public class RoleMapper : Profile
    {
        public RoleMapper() {
            CreateMap<Role, RoleResponse>()
              .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
              .ForMember(dest => dest.Permissions, opt => opt.MapFrom(src => src.Permissions));
        }
    }
}
