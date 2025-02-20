using AutoMapper;
using UNI_Management.Domain;
using UNI_Management.ViewModel;
using static UNI_Management.ViewModel.AttandanceViewModal;
using static UNI_Management.ViewModel.TimeSheetViewModel;
using static UNI_Management.ViewModel.WorkLogViewModal;

namespace UNI_Management.Helper.Mapper.TimeSheet
{
    public static class TimeSheetMapper
    {
        public static List<TimeSheetDetails> ToModel(this List<TimeSheetDTO> entity)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TimeSheetDTO, TimeSheetDetails>()
                 .ForMember(dest => dest.WorkLogDetails, mo => mo.MapFrom(src => new List<WorkLogDetails> 
                 {new WorkLogDetails
                        {
                            WorkLogId = src.workLog.WorkLogId,
                            EmployeeId = src.workLog.EmployeeId,
                            Note = src.workLog.Message,
                            SignOutTime = src.workLog.SignOutTime,
                            CreatedDate = src.workLog.CreatedDate
                        } }))
                .ForMember(dest => dest.AttandenceDetails, mo => mo.MapFrom(src => new List<AttandenceDetails>
                    {
                        new AttandenceDetails
                        {
                            CreatedDate = src.attandence.Created,
                            Status = src.attandence.Status
                        }
                    }));
            });
            IMapper mapper = config.CreateMapper();
            return mapper.Map<List<TimeSheetDTO>, List<TimeSheetDetails>>(entity);
        }
    }
}
