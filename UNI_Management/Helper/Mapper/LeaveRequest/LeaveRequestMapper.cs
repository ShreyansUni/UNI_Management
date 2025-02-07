using AutoMapper;
using static UNI_Management.ViewModel.AttandanceViewModal;
using UNI_Management.Domain;
using UNI_Management.ViewModel;

namespace UNI_Management.Helper.Mapper.LeaveRequest
{
    public static class LeaveRequestMapper
    {
        public static List<LeaveRequestViewModel> ToModel(this List<LeaveRequestDTO> entity)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<LeaveRequestDTO, LeaveRequestViewModel>()
                .ForPath(dest => dest.leaveRequestDetails.LeaveID, mo => mo.MapFrom(src => src.LeaveID))
                .ForPath(dest => dest.leaveRequestDetails.LeaveRequestorID, mo => mo.MapFrom(src => src.LeaveRequestorID))
                .ForPath(dest => dest.leaveRequestDetails.LeaveRequestorName, mo => mo.MapFrom(src => src.LeaveRequestorName))
                .ForPath(dest => dest.leaveRequestDetails.LeaveResponsorID, mo => mo.MapFrom(src => src.LeaveResponsorID))
                .ForPath(dest => dest.leaveRequestDetails.LeaveResponsorName, mo => mo.MapFrom(src => src.LeaveResponsorName))
                .ForPath(dest => dest.leaveRequestDetails.ReasonForLeave, mo => mo.MapFrom(src => src.ReasonForLeave))
                .ForPath(dest => dest.leaveRequestDetails.LeaveStartDate, mo => mo.MapFrom(src => src.LeaveStartDate))
                .ForPath(dest => dest.leaveRequestDetails.LeaveStartType, mo => mo.MapFrom(src => src.LeaveStartType))
                .ForPath(dest => dest.leaveRequestDetails.LeaveEndDate, mo => mo.MapFrom(src => src.LeaveEndDate))
                .ForPath(dest => dest.leaveRequestDetails.LeaveEndType, mo => mo.MapFrom(src => src.ReasonForLeave))
                .ForPath(dest => dest.leaveRequestDetails.TotalLeaveDuration, mo => mo.MapFrom(src => src.TotalLeaveDuration))
                .ForPath(dest => dest.leaveRequestDetails.ActualLeaveDuration, mo => mo.MapFrom(src => src.ActualLeaveDuration))
                .ForPath(dest => dest.leaveRequestDetails.RequestedDate, mo => mo.MapFrom(src => src.RequestedDate))
                .ForPath(dest => dest.leaveRequestDetails.ReturnDate, mo => mo.MapFrom(src => src.ReturnDate))
                .ForPath(dest => dest.leaveRequestDetails.IsAdhocLeave, mo => mo.MapFrom(src => src.IsAdhocLeave))
                .ForPath(dest => dest.leaveRequestDetails.IsAvailableOnPhone, mo => mo.MapFrom(src => src.IsAvailableOnPhone))
                .ForPath(dest => dest.leaveRequestDetails.AlternatePhoneNumber, mo => mo.MapFrom(src => src.AlternatePhoneNumber))
                .ForPath(dest => dest.leaveRequestDetails.PhoneNumber, mo => mo.MapFrom(src => src.PhoneNumber));
            });
            IMapper mapper = config.CreateMapper();
            return mapper.Map<List<LeaveRequestDTO>, List<LeaveRequestViewModel>>(entity);
        }
        
        public static LeaveRequestDetails ToModel(this LeaveRequestDTO entity)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<LeaveRequestDTO, LeaveRequestDetails>()
                .ForMember(dest => dest.LeaveID, mo => mo.MapFrom(src => src.LeaveID))
                .ForMember(dest => dest.LeaveRequestorID, mo => mo.MapFrom(src => src.LeaveRequestorID))
                .ForMember(dest => dest.LeaveRequestorName, mo => mo.MapFrom(src => src.LeaveRequestorName))
                .ForMember(dest => dest.LeaveResponsorID, mo => mo.MapFrom(src => src.LeaveResponsorID))
                .ForMember(dest => dest.LeaveResponsorName, mo => mo.MapFrom(src => src.LeaveResponsorName))
                .ForMember(dest => dest.ReasonForLeave, mo => mo.MapFrom(src => src.ReasonForLeave))
                .ForMember(dest => dest.LeaveStartDate, mo => mo.MapFrom(src => src.LeaveStartDate))
                .ForMember(dest => dest.LeaveStartType, mo => mo.MapFrom(src => src.LeaveStartType))
                .ForMember(dest => dest.LeaveEndDate, mo => mo.MapFrom(src => src.LeaveEndDate))
                .ForMember(dest => dest.LeaveEndType, mo => mo.MapFrom(src => src.ReasonForLeave))
                .ForMember(dest => dest.TotalLeaveDuration, mo => mo.MapFrom(src => src.TotalLeaveDuration))
                .ForMember(dest => dest.ActualLeaveDuration, mo => mo.MapFrom(src => src.ActualLeaveDuration))
                .ForMember(dest => dest.RequestedDate, mo => mo.MapFrom(src => src.RequestedDate))
                .ForMember(dest => dest.ReturnDate, mo => mo.MapFrom(src => src.ReturnDate))
                .ForMember(dest => dest.IsAdhocLeave, mo => mo.MapFrom(src => src.IsAdhocLeave))
                .ForMember(dest => dest.IsAvailableOnPhone, mo => mo.MapFrom(src => src.IsAvailableOnPhone))
                .ForMember(dest => dest.AlternatePhoneNumber, mo => mo.MapFrom(src => src.AlternatePhoneNumber))
                .ForMember(dest => dest.PhoneNumber, mo => mo.MapFrom(src => src.PhoneNumber));
            });
            IMapper mapper = config.CreateMapper();
            return mapper.Map<LeaveRequestDTO, LeaveRequestDetails>(entity);
        }
    }
}
