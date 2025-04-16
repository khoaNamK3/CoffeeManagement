using AutoMapper;
using CoffeeManagement.RequestModel.Shift;
using CoffeeManagement.ResponseModel.Shift;

namespace CoffeeManagement.Model.Mapper
{
    public class ShiftMapper :Profile
    {
        public  ShiftMapper() {

            CreateMap<ShiftRequest, Shift>()
                .ForMember(dest => dest.WorkingDate, opt => opt.MapFrom(src => src.WorkingDate))
                .ForMember(dest => dest.Salary, opt => opt.MapFrom(src => src.Salary))
                .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => src.StartTime))
                .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => src.EndTime));

            CreateMap<Shift, ShiftResponse>()
                .ForMember(dest => dest.WorkingDate, opt => opt.MapFrom(src => src.WorkingDate))
                .ForMember(dest => dest.Salary, opt => opt.MapFrom(src => src.Salary))
                .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => src.StartTime))
                .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => src.EndTime));
        }
    }
}
