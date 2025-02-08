using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNI_Management.ViewModel
{
    public class LeaveRequestViewModel : BaseModelViewModel
    {
        public LeaveRequestViewModel()
        {
            leaveRequestDetails = new LeaveRequestDetails();
            leaveRequestList = new List<LeaveRequestDetails>();
        }

        public List<LeaveRequestDetails> leaveRequestList { get; set; }

        public LeaveRequestDetails leaveRequestDetails { get; set; }
    }
    public class LeaveRequestDetails
    {
        public int LeaveID {  get; set; }
        [Required]
        public int LeaveRequestorID { get; set; }
        [Required]
        public int LeaveResponsorID { get; set; }
        [Required]
        public string LeaveResponsorName { get; set; }
        [Required]
        public string LeaveRequestorName { get; set; }
        [Required]
        public string ReasonForLeave { get; set; }
        [Required]
        public DateOnly? LeaveStartDate { get; set; }
        [Required]
        public int LeaveStartType {  get; set; }
        [Required]
        public DateOnly? LeaveEndDate {  get; set; }
        [Required]
        public int LeaveEndType {  get; set; }
        [Required]
        public decimal ActualLeaveDuration {  get; set; }
        [Required]
        public decimal TotalLeaveDuration {  get; set; }
        [Required]
        public DateOnly ReturnDate {  get; set; }
        [Required]
        public DateOnly RequestedDate { get; set; }
        //public List<Employee>? EmployeeDropdownList{ get; set; }
        [Required]
        [RegularExpression(@"^(\+91[\-\s]?|91[\-\s]?|0)?[6-9]\d{9}$",ErrorMessage = "Invalid phone number.")]
        public string PhoneNumber { get; set; }
        [RegularExpression(@"^(\+91[\-\s]?|91[\-\s]?|0)?[6-9]\d{9}$",ErrorMessage = "Invalid phone number.")]
        public string? AlternatePhoneNumber { get; set; }
        public bool IsAvailableOnPhone { get; set; } = true;
        public bool IsAdhocLeave { get; set; } = false;
    }
}
