using AutoMapper;
using static UNI_Management.ViewModel.AttandanceViewModal;
using UNI_Management.Domain;
using UNI_Management.ViewModel;

namespace UNI_Management.Helper.Mapper.LeaveRequest
{
    public static class LeaveRequestMapper
    {
        public static List<LeaveRequestDetails> ToModel(this List<LeaveRequestDTO> entity)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<LeaveRequestDTO, LeaveRequestDetails>()
                .ForMember(dest => dest.LeaveID, mo => mo.MapFrom(src => src.leaveRequest.LeaveRequestId))
                .ForMember(dest => dest.LeaveRequestorID, mo => mo.MapFrom(src => src.leaveRequest.EmployeeId))
                .ForMember(dest => dest.LeaveResponsorID, mo => mo.MapFrom(src => src.leaveRequest.ReportingEmployeeId))
                .ForMember(dest => dest.LeaveRequestorName, mo => mo.MapFrom(src => src.leaveRequest.LeaveRequestId))
                .ForMember(dest => dest.LeaveResponsorName, mo => mo.MapFrom(src => src.leaveRequest.ReportingEmployeeId))
                .ForMember(dest => dest.ReasonForLeave, mo => mo.MapFrom(src => src.leaveRequest.ReasonForLeave))
                .ForMember(dest => dest.LeaveStartDate, mo => mo.MapFrom(src => src.leaveRequest.LeaveStartDate))
                .ForMember(dest => dest.LeaveStartType, mo => mo.MapFrom(src => src.leaveRequest.LeaveStartDateDuration))
                .ForMember(dest => dest.LeaveEndDate, mo => mo.MapFrom(src => src.leaveRequest.LeaveEndDate))
                .ForMember(dest => dest.LeaveEndType, mo => mo.MapFrom(src => src.leaveRequest.LeaveEndDuration))
                .ForMember(dest => dest.TotalLeaveDuration, mo => mo.MapFrom(src => src.leaveRequest.TotalLeaveDuration))
                .ForMember(dest => dest.ActualLeaveDuration, mo => mo.MapFrom(src => src.leaveRequest.ActualLeaveDuration))
                .ForMember(dest => dest.RequestedDate, mo => mo.MapFrom(src => src.leaveRequest.RequestedDate))
                .ForMember(dest => dest.ReturnDate, mo => mo.MapFrom(src => src.leaveRequest.ReturnDate))
                .ForMember(dest => dest.IsAdhocLeave, mo => mo.MapFrom(src => src.leaveRequest.IsAdhocLeave))
                .ForMember(dest => dest.IsAvailableOnPhone, mo => mo.MapFrom(src => src.leaveRequest.IsAvailableOnPhone))
                .ForMember(dest => dest.AlternatePhoneNumber, mo => mo.MapFrom(src => src.leaveRequest.AlternatePhoneNumber))
                .ForMember(dest => dest.PhoneNumber, mo => mo.MapFrom(src => src.leaveRequest.PhoneNumber));
            });
            IMapper mapper = config.CreateMapper();
            return mapper.Map<List<LeaveRequestDTO>, List<LeaveRequestDetails>>(entity);
        }

        public static LeaveRequestViewModel ToModel(this LeaveRequestDTO entity)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<LeaveRequestDTO, LeaveRequestViewModel>()
                .ForPath(dest => dest.leaveRequestDetails.LeaveID, mo => mo.MapFrom(src => src.leaveRequest.LeaveRequestId))
                .ForPath(dest => dest.leaveRequestDetails.LeaveRequestorID, mo => mo.MapFrom(src => src.leaveRequest.EmployeeId))
                .ForPath(dest => dest.leaveRequestDetails.LeaveResponsorID, mo => mo.MapFrom(src => src.leaveRequest.ReportingEmployeeId))
                .ForPath(dest => dest.leaveRequestDetails.ReasonForLeave, mo => mo.MapFrom(src => src.leaveRequest.ReasonForLeave))
                .ForPath(dest => dest.leaveRequestDetails.LeaveStartDate, mo => mo.MapFrom(src => src.leaveRequest.LeaveStartDate))
                .ForPath(dest => dest.leaveRequestDetails.LeaveStartType, mo => mo.MapFrom(src => src.leaveRequest.LeaveStartDateDuration))
                .ForPath(dest => dest.leaveRequestDetails.LeaveEndDate, mo => mo.MapFrom(src => src.leaveRequest.LeaveEndDate))
                .ForPath(dest => dest.leaveRequestDetails.LeaveEndType, mo => mo.MapFrom(src => src.leaveRequest.LeaveEndDuration))
                .ForPath(dest => dest.leaveRequestDetails.TotalLeaveDuration, mo => mo.MapFrom(src => src.leaveRequest.TotalLeaveDuration))
                .ForPath(dest => dest.leaveRequestDetails.ActualLeaveDuration, mo => mo.MapFrom(src => src.leaveRequest.ActualLeaveDuration))
                .ForPath(dest => dest.leaveRequestDetails.RequestedDate, mo => mo.MapFrom(src => src.leaveRequest.RequestedDate))
                .ForPath(dest => dest.leaveRequestDetails.ReturnDate, mo => mo.MapFrom(src => src.leaveRequest.ReturnDate))
                .ForPath(dest => dest.leaveRequestDetails.IsAdhocLeave, mo => mo.MapFrom(src => src.leaveRequest.IsAdhocLeave))
                .ForPath(dest => dest.leaveRequestDetails.IsAvailableOnPhone, mo => mo.MapFrom(src => src.leaveRequest.IsAvailableOnPhone))
                .ForPath(dest => dest.leaveRequestDetails.AlternatePhoneNumber, mo => mo.MapFrom(src => src.leaveRequest.AlternatePhoneNumber))
                .ForPath(dest => dest.leaveRequestDetails.PhoneNumber, mo => mo.MapFrom(src => src.leaveRequest.PhoneNumber));
            });
            IMapper mapper = config.CreateMapper();
            return mapper.Map<LeaveRequestDTO, LeaveRequestViewModel>(entity);
        }
        
        public static LeaveRequestDTO ToModel(this LeaveRequestViewModel entity)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<LeaveRequestViewModel, LeaveRequestDTO>()
                .ForPath(dest => dest.leaveRequest.LeaveRequestId, mo => mo.MapFrom(src => src.leaveRequestDetails.LeaveID))
                .ForPath(dest => dest.leaveRequest.EmployeeId, mo => mo.MapFrom(src => src.leaveRequestDetails.LeaveRequestorID))
                .ForPath(dest => dest.leaveRequest.ReportingEmployeeId, mo => mo.MapFrom(src => src.leaveRequestDetails.LeaveResponsorID))
                .ForPath(dest => dest.leaveRequest.ReasonForLeave, mo => mo.MapFrom(src => src.leaveRequestDetails.ReasonForLeave))
                .ForPath(dest => dest.leaveRequest.LeaveStartDate, mo => mo.MapFrom(src => src.leaveRequestDetails.LeaveStartDate))
                .ForPath(dest => dest.leaveRequest.LeaveStartDateDuration, mo => mo.MapFrom(src => src.leaveRequestDetails.LeaveStartType))
                .ForPath(dest => dest.leaveRequest.LeaveEndDate, mo => mo.MapFrom(src => src.leaveRequestDetails.LeaveEndDate))
                .ForPath(dest => dest.leaveRequest.LeaveEndDuration, mo => mo.MapFrom(src => src.leaveRequestDetails.LeaveEndType))
                .ForPath(dest => dest.leaveRequest.TotalLeaveDuration, mo => mo.MapFrom(src => src.leaveRequestDetails.TotalLeaveDuration))
                .ForPath(dest => dest.leaveRequest.ActualLeaveDuration, mo => mo.MapFrom(src => src.leaveRequestDetails.ActualLeaveDuration))
                .ForPath(dest => dest.leaveRequest.RequestedDate, mo => mo.MapFrom(src => src.leaveRequestDetails.RequestedDate))
                .ForPath(dest => dest.leaveRequest.ReturnDate, mo => mo.MapFrom(src => src.leaveRequestDetails.ReturnDate))
                .ForPath(dest => dest.leaveRequest.IsAdhocLeave, mo => mo.MapFrom(src => src.leaveRequestDetails.IsAdhocLeave))
                .ForPath(dest => dest.leaveRequest.IsAvailableOnPhone, mo => mo.MapFrom(src => src.leaveRequestDetails.IsAvailableOnPhone))
                .ForPath(dest => dest.leaveRequest.AlternatePhoneNumber, mo => mo.MapFrom(src => src.leaveRequestDetails.AlternatePhoneNumber))
                .ForPath(dest => dest.leaveRequest.PhoneNumber, mo => mo.MapFrom(src => src.leaveRequestDetails.PhoneNumber));
            });
            IMapper mapper = config.CreateMapper();
            return mapper.Map<LeaveRequestViewModel, LeaveRequestDTO>(entity);
        }
    }
}
