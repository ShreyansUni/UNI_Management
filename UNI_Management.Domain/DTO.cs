using UNI_Management.Domain.DataModels;

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
        public int LeaveStartType { get; set; }
        public int LeaveEndType { get; set; }
        public string LeaveRequestorName { get; set; }
        public string LeaveResponsorName { get; set; }

        public List<Employee>? EmployeeDropdownList { get; set; }
    }

    public class WorkLogDTO : TotalRecord
    {
        public WorkLog workLog { get; set; }
    }

    public class TotalRecord
    {
        public int TotalRecords { get; set; }
    }
}