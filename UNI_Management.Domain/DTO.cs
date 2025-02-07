using UNI_Management.DataModels;

namespace UNI_Management.Domain
{
    public class EmployeeDTO : TotalRecord
    {
        public Employee Employee { get; set; }

        public EmployeeAttachment employeeAttachment { get; set; }
    }
    
    public class AttandenceDTO : TotalRecord
    {
        public EmployeeAttendance employeeAttendance { get; set; }
    }
    
    public class LeaveRequestDTO : TotalRecord
    {
        public LeaveRequest leaveRequest { get; set; }

        public int LeaveID { get; set; }
        public int LeaveRequestorID { get; set; }
        public int LeaveResponsorID { get; set; }
        public string LeaveRequestorName { get; set; }
        public string LeaveResponsorName { get; set; }
        public string ReasonForLeave { get; set; }
        public DateOnly? LeaveStartDate { get; set; }
        public int LeaveStartType { get; set; }
        public DateOnly? LeaveEndDate { get; set; }
        public int LeaveEndType { get; set; }
        public decimal ActualLeaveDuration { get; set; }
        public decimal TotalLeaveDuration { get; set; }
        public DateOnly? ReturnDate { get; set; }
        public DateOnly? RequestedDate { get; set; }
        public string PhoneNumber { get; set; }
        public string? AlternatePhoneNumber { get; set; }
        public bool IsAvailableOnPhone { get; set; }
        public bool IsAdhocLeave { get; set; }
        public List<Employee>? EmployeeDropdownList { get; set; }
    }

    public class TotalRecord
    {
        public int TotalRecords { get; set; }
    }
}