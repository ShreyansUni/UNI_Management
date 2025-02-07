using AutoMapper;
using static UNI_Management.ViewModel.EmployeeViewModel;
using UNI_Management.Domain;
using UNI_Management.ViewModel;
using static UNI_Management.ViewModel.AttandanceViewModal;

namespace UNI_Management.Helper.Mapper.Attandence
{
    public static class AttandenceMapper
    {
        public static List<AttandenceDetails> ToModel(this List<AttandenceDTO> entity)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<AttandenceDTO, AttandenceDetails>()
                .ForMember(dest => dest.EmployeeId, mo => mo.MapFrom(src => src.employeeAttendance.EmployeeId))
                .ForMember(dest => dest.AttendanceId, mo => mo.MapFrom(src => src.employeeAttendance.AttendanceId))
                .ForMember(dest => dest.Status, mo => mo.MapFrom(src => src.employeeAttendance.Status))
                .ForMember(dest => dest.Day, mo => mo.MapFrom(src => src.employeeAttendance.Day))
                .ForMember(dest => dest.Month, mo => mo.MapFrom(src => src.employeeAttendance.Month))
                .ForMember(dest => dest.Year, mo => mo.MapFrom(src => src.employeeAttendance.Year));
            });
            IMapper mapper = config.CreateMapper();
            return mapper.Map<List<AttandenceDTO>, List<AttandenceDetails>>(entity);
        }
    }
}
