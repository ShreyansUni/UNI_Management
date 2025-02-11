using AutoMapper;
using UNI_Management.Domain;
using UNI_Management.ViewModel;
using static UNI_Management.ViewModel.WorkLogViewModal;

namespace UNI_Management.Helper.Mapper.WorkLog
{
    public static class WorkLogMapper
    {
        public static List<WorkLogDetails> ToModel(this List<WorkLogDTO> entity)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<WorkLogDTO, WorkLogDetails>()
                .ForMember(dest => dest.WorkLogId, mo => mo.MapFrom(src => src.workLog.WorkLogId))
                .ForMember(dest => dest.EmployeeId, mo => mo.MapFrom(src => src.workLog.EmployeeId))
                .ForMember(dest => dest.Note, mo => mo.MapFrom(src => src.workLog.Message))
                .ForMember(dest => dest.Subject, mo => mo.MapFrom(src => src.workLog.Subject))
                .ForMember(dest => dest.SignOutTime, mo => mo.MapFrom(src => src.workLog.SignOutTime))
                .ForMember(dest => dest.CreatedDate, mo => mo.MapFrom(src => src.workLog.CreatedDate));
            });
            IMapper mapper = config.CreateMapper();
            return mapper.Map<List<WorkLogDTO>, List<WorkLogDetails>>(entity);
        }
        
        public static WorkLogDetails ToModel(this WorkLogDTO entity)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<WorkLogDTO, WorkLogDetails>()
                .ForMember(dest => dest.WorkLogId, mo => mo.MapFrom(src => src.workLog.WorkLogId))
                .ForMember(dest => dest.EmployeeId, mo => mo.MapFrom(src => src.workLog.EmployeeId))
                .ForMember(dest => dest.Note, mo => mo.MapFrom(src => src.workLog.Message))
                .ForMember(dest => dest.Subject, mo => mo.MapFrom(src => src.workLog.Subject))
                .ForMember(dest => dest.SignOutTime, mo => mo.MapFrom(src => src.workLog.SignOutTime))
                .ForMember(dest => dest.CreatedDate, mo => mo.MapFrom(src => src.workLog.CreatedDate));
            });
            IMapper mapper = config.CreateMapper();
            return mapper.Map<WorkLogDTO, WorkLogDetails>(entity);
        }
        
        public static WorkLogDTO ToModel(this WorkLogViewModal entity)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<WorkLogViewModal, WorkLogDTO>()
                .ForPath(dest => dest.workLog.WorkLogId, mo => mo.MapFrom(src => src.workLogDetails.WorkLogId))
                .ForPath(dest => dest.workLog.EmployeeId, mo => mo.MapFrom(src => src.workLogDetails.EmployeeId))
                .ForPath(dest => dest.workLog.Message, mo => mo.MapFrom(src => src.workLogDetails.Note))
                .ForPath(dest => dest.workLog.Subject, mo => mo.MapFrom(src => src.workLogDetails.Subject))
                .ForPath(dest => dest.workLog.SignOutTime, mo => mo.MapFrom(src => src.workLogDetails.SignOutTime))
                .ForPath(dest => dest.workLog.CreatedDate, mo => mo.MapFrom(src => src.workLogDetails.CreatedDate));
            });
            IMapper mapper = config.CreateMapper();
            return mapper.Map<WorkLogViewModal, WorkLogDTO>(entity);
        }
    }
}
