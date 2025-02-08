using AutoMapper;
using UNI_Management.Domain;
using UNI_Management.ViewModel;
using static UNI_Management.ViewModel.EmployeeViewModel;

namespace UNI_Management.Helper.Mapper
{
    public static class EmployeeMapper
    {
        public static List<EmployeeDetails> ToModel(this List<EmployeeDTO> entity)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<EmployeeDTO, EmployeeDetails>()
                .ForMember(dest => dest.FirstName, mo => mo.MapFrom(src => src.Employee.FirstName))
                .ForMember(dest => dest.Joinningdate, mo => mo.MapFrom(src => src.Employee.Joinningdate))
                .ForMember(dest => dest.EmployeeType, mo => mo.MapFrom(src => src.Employee.EmployeeType))
                .ForMember(dest => dest.EmployeeId, mo => mo.MapFrom(src => src.Employee.EmployeeId))
                .ForMember(dest => dest.ContactNumber1, mo => mo.MapFrom(src => src.Employee.ContactNumber1));
            });
            IMapper mapper = config.CreateMapper();
            return mapper.Map<List<EmployeeDTO>, List<EmployeeDetails>>(entity);
        }

        public static EmployeeDetails ToModel(this EmployeeDTO entity)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<EmployeeDTO, EmployeeDetails>()
                .ForMember(x => x.FirstName, mo => mo.MapFrom(x => x.Employee.FirstName))
                .ForMember(x => x.LastName, mo => mo.MapFrom(x => x.Employee.LastName))
                .ForMember(x => x.EmployeeId, mo => mo.MapFrom(x => x.Employee.EmployeeId))
                .ForMember(x => x.MiddleName, mo => mo.MapFrom(x => x.Employee.MiddleName))
                .ForMember(x => x.UserName, mo => mo.MapFrom(x => x.Employee.UserName))
                .ForMember(x => x.Password, mo => mo.MapFrom(x => x.Employee.Password))
                .ForMember(x => x.ContactNumber1, mo => mo.MapFrom(x => x.Employee.ContactNumber1))
                .ForMember(x => x.ContactNumber2, mo => mo.MapFrom(x => x.Employee.ContactNumber2))
                .ForMember(x => x.Email, mo => mo.MapFrom(x => x.Employee.Email))
                .ForMember(x => x.Address, mo => mo.MapFrom(x => x.Employee.Address))
                .ForMember(x => x.Country, mo => mo.MapFrom(x => x.Employee.Country))
                .ForMember(x => x.State, mo => mo.MapFrom(x => x.Employee.State))
                .ForMember(x => x.Birthdate, mo => mo.MapFrom(x => x.Employee.Birthdate))
                .ForMember(x => x.Education, mo => mo.MapFrom(x => x.Employee.Education))
                //.ForMember(x => x.Photo, mo => mo.MapFrom(x => x.Employee.Photo))
                .ForMember(x => x.Gender, mo => mo.MapFrom(x => x.Employee.Gender))
                .ForMember(x => x.Joinningdate, mo => mo.MapFrom(x => x.Employee.Joinningdate))
                .ForMember(x => x.IsFresher, mo => mo.MapFrom(x => x.Employee.IsFresher))
                .ForMember(x => x.IsActive, mo => mo.MapFrom(x => x.Employee.IsActive))
                //.ForMember(x => x.Resume, mo => mo.MapFrom(x => x.Employee.Resume))
                .ForMember(x => x.Bond, mo => mo.MapFrom(x => x.Employee.Bond))
                .ForMember(x => x.EmployeeType, mo => mo.MapFrom(x => x.Employee.EmployeeType))
                .ForMember(x => x.EmployeeAttachmentId, mo => mo.MapFrom(x => x.employeeAttachment.EmployeeAttachmentId))
                .ForMember(x => x.EmployeeId, mo => mo.MapFrom(x => x.employeeAttachment.EmployeeId))
                .ForMember(x => x.IsAdhar, mo => mo.MapFrom(x => x.employeeAttachment.IsAdhar))
                .ForMember(x => x.AdharNo, mo => mo.MapFrom(x => x.employeeAttachment.AdharNo))
                .ForMember(x => x.IsPassbook, mo => mo.MapFrom(x => x.employeeAttachment.IsPassbook))
                .ForMember(x => x.AccountNumber, mo => mo.MapFrom(x => x.employeeAttachment.AccountNumber))
                .ForMember(x => x.Ifsc, mo => mo.MapFrom(x => x.employeeAttachment.Ifsc))
                .ForMember(x => x.Upi, mo => mo.MapFrom(x => x.employeeAttachment.Upi))
                .ForMember(x => x.IsDegree, mo => mo.MapFrom(x => x.employeeAttachment.IsDegree))
                .ForMember(x => x.IsMarksheetUpload, mo => mo.MapFrom(x => x.employeeAttachment.IsMarksheetUpload))
                .ForMember(x => x.BankName, mo => mo.MapFrom(x => x.employeeAttachment.BankName));
            });
            IMapper mapper = config.CreateMapper();
            return mapper.Map<EmployeeDTO, EmployeeDetails>(entity);
        }
        
        public static EmployeeDTO ToModel(this EmployeeViewModel entity)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<EmployeeViewModel, EmployeeDTO>()
                .ForPath(x => x.Employee.FirstName, mo => mo.MapFrom(x => x.employeeDetails.FirstName))
                .ForPath(x => x.Employee.LastName, mo => mo.MapFrom(x => x.employeeDetails.LastName))
                .ForPath(x => x.Employee.EmployeeId, mo => mo.MapFrom(x => x.employeeDetails.EmployeeId))
                .ForPath(x => x.Employee.MiddleName, mo => mo.MapFrom(x => x.employeeDetails.MiddleName))
                .ForPath(x => x.Employee.UserName, mo => mo.MapFrom(x => x.employeeDetails.UserName))
                .ForPath(x => x.Employee.Password, mo => mo.MapFrom(x => x.employeeDetails.Password))
                .ForPath(x => x.Employee.ContactNumber1, mo => mo.MapFrom(x => x.employeeDetails.ContactNumber1))
                .ForPath(x => x.Employee.ContactNumber2, mo => mo.MapFrom(x => x.employeeDetails.ContactNumber2))
                .ForPath(x => x.Employee.Email, mo => mo.MapFrom(x => x.employeeDetails.Email))
                .ForPath(x => x.Employee.IsActive, mo => mo.MapFrom(x => x.employeeDetails.IsActive))
                .ForPath(x => x.Employee.Address, mo => mo.MapFrom(x => x.employeeDetails.Address))
                .ForPath(x => x.Employee.Country, mo => mo.MapFrom(x => x.employeeDetails.Country))
                .ForPath(x => x.Employee.State, mo => mo.MapFrom(x => x.employeeDetails.State))
                .ForPath(x => x.Employee.Birthdate, mo => mo.MapFrom(x => x.employeeDetails.Birthdate))
                .ForPath(x => x.Employee.Education, mo => mo.MapFrom(x => x.employeeDetails.Education))
                //.ForPath(x => x.Employee.Photo, mo => mo.MapFrom(x => x.Photo))
                .ForPath(x => x.Employee.Gender, mo => mo.MapFrom(x => x.employeeDetails.Gender))
                .ForPath(x => x.Employee.Joinningdate, mo => mo.MapFrom(x => x.employeeDetails.Joinningdate))
                .ForPath(x => x.Employee.ContactNumber1, mo => mo.MapFrom(x => x.employeeDetails.ContactNumber1))
                .ForPath(x => x.Employee.IsFresher, mo => mo.MapFrom(x => x.employeeDetails.IsFresher))
                //.ForPath(x => x.Employee.Resume, mo => mo.MapFrom(x => x.Resume))
                .ForPath(x => x.Employee.Bond, mo => mo.MapFrom(x => x.employeeDetails.Bond))
                .ForPath(x => x.Employee.EmployeeType, mo => mo.MapFrom(x => x.employeeDetails.EmployeeType))
                .ForPath(x => x.employeeAttachment.EmployeeAttachmentId, mo => mo.MapFrom(x => x.employeeDetails.EmployeeAttachmentId))
                .ForPath(x => x.employeeAttachment.IsAdhar, mo => mo.MapFrom(x => x.employeeDetails.IsAdhar))
                .ForPath(x => x.employeeAttachment.AdharNo, mo => mo.MapFrom(x => x.employeeDetails.AdharNo))
                .ForPath(x => x.employeeAttachment.IsPassbook, mo => mo.MapFrom(x => x.employeeDetails.IsPassbook))
                .ForPath(x => x.employeeAttachment.AccountNumber, mo => mo.MapFrom(x => x.employeeDetails.AccountNumber))
                .ForPath(x => x.employeeAttachment.Ifsc, mo => mo.MapFrom(x => x.employeeDetails.Ifsc))
                .ForPath(x => x.employeeAttachment.Upi, mo => mo.MapFrom(x => x.employeeDetails.Upi))
                .ForPath(x => x.employeeAttachment.BankName, mo => mo.MapFrom(x => x.employeeDetails.BankName));
            });
            IMapper mapper = config.CreateMapper();
            return mapper.Map<EmployeeViewModel, EmployeeDTO>(entity);
        }
    }
}
