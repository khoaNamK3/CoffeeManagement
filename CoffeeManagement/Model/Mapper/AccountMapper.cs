using AutoMapper;
using CoffeeManagement.RequestModel.Account;
using CoffeeManagement.ResponseModel.Account;

namespace CoffeeManagement.Model.Mapper
{
    public class AccountMapper : Profile
    {
        public AccountMapper() {

            // Request -> Entity
            CreateMap<AccountRequest, Account>()
             .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
             .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
             .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password))
             .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone))
             .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
             .ForMember(dest => dest.IsActive, opt => opt.MapFrom(srt => srt.IsActive));
             
           

            // Entity -> Response
            CreateMap<Account, AccountResponse>()
             .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
             .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
             .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone))
             .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
             .ForMember(dest => dest.RoleId, opt => opt.MapFrom(src => src.Role.Name)) // This will map the Role name
             .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password))
             .ForMember(dest => dest.IsActive, opt => opt.MapFrom(srt => srt.IsActive));
        }
    }
}
