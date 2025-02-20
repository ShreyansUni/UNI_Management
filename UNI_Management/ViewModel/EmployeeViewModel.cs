using AspNetCoreGeneratedDocument;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNI_Management.ViewModel
{
	public class EmployeeViewModel : BaseModelViewModel
	{
        public EmployeeViewModel()
        {
            employeeDetails = new EmployeeDetails();
            employeeList = new List<EmployeeDetails>();
        }

        public List<EmployeeDetails> employeeList { get; set; }

        public EmployeeDetails employeeDetails { get; set; }

        public class EmployeeDetails
        {
            public int EmployeeId { get; set; }

            [Required(ErrorMessage = "First Name is required.")]
            [StringLength(50, ErrorMessage = "First Name cannot exceed 50 characters.")]
            public string FirstName { get; set; } = null!;

            [Required(ErrorMessage = "Middle Name is required.")]
            [StringLength(50, ErrorMessage = "Middle Name cannot exceed 50 characters.")]
            public string MiddleName { get; set; } = null!;

            [Required(ErrorMessage = "Last Name is required.")]
            [StringLength(50, ErrorMessage = "Last Name cannot exceed 50 characters.")]
            public string LastName { get; set; } = null!;

            [Required(ErrorMessage = "Username is required.")]
            [StringLength(20, ErrorMessage = "Username cannot exceed 20 characters.")]
            public string UserName { get; set; } = null!;

            [Required(ErrorMessage = "Password is required.")]
            [StringLength(100, ErrorMessage = "Password cannot exceed 100 characters.")]
            public string Password { get; set; } = null!;

            [Required(ErrorMessage = "Contact Number is required.")]
            [RegularExpression("^\\d{10}$", ErrorMessage = "Contact Number must be a valid 10-digit number.")]
            public string? ContactNumber1 { get; set; }

            [RegularExpression("^\\d{10}$", ErrorMessage = "Contact Number must be a valid 10-digit number.")]
            public string? ContactNumber2 { get; set; }

            [Required(ErrorMessage = "Email is required.")]
            [EmailAddress(ErrorMessage = "Invalid email format.")]
            public string? Email { get; set; }

            [Required(ErrorMessage = "Address is required.")]
            [StringLength(200, ErrorMessage = "Address cannot exceed 200 characters.")]
            public string? Address { get; set; }

            //[StringLength(50, ErrorMessage = "Country name cannot exceed 50 characters.")]
            public string? Country { get; set; }

            //[StringLength(50, ErrorMessage = "State name cannot exceed 50 characters.")]
            public string? State { get; set; }

            [Required(ErrorMessage = "Birth Date is required.")]
            [DataType(DataType.Date, ErrorMessage = "Invalid date format.")]
            public DateTime? Birthdate { get; set; }

            [Required(ErrorMessage = "Education is required.")]
            [StringLength(100, ErrorMessage = "Education cannot exceed 100 characters.")]
            public string? Education { get; set; }

            public string? Photo { get; set; }

            public IFormFile? EmployeeImage { get; set; }

            [DataType(DataType.Date, ErrorMessage = "Invalid date format.")]
            public DateTime? Joinningdate { get; set; }

            public string? IsFresher { get; set; }

            public string? Resume { get; set; }

            [Required(ErrorMessage = "Bond information is required.")]
            [StringLength(50, ErrorMessage = "Bond information cannot exceed 50 characters.")]
            public string? Bond { get; set; }

            public bool? IsDeleted { get; set; }

            //[Required(ErrorMessage = "Active status is required.")]
            public string IsActive { get; set; } = null!;

            [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
            public string? Description { get; set; }

            //[Required(ErrorMessage = "Created date is required.")]
            public DateTime Created { get; set; }

            //[Required(ErrorMessage = "CreatedBy is required.")]
            public int CreatedBy { get; set; }

            public DateTime? Modified { get; set; }

            public int? ModifiedBy { get; set; }

            //[StringLength(50, ErrorMessage = "Employee type cannot exceed 50 characters.")]
            public string? EmployeeType { get; set; }

            [StringLength(10, ErrorMessage = "Gender cannot exceed 10 characters.")]
            public string? Gender { get; set; }

            public int EmployeeAttachmentId { get; set; }

            public bool? IsAdhar { get; set; }

            public IFormFile? AdharCard { get; set; }

            [RegularExpression("^\\d{12}$", ErrorMessage = "Adhar Number must be a valid 12-digit number.")]
            public string? AdharNo { get; set; }

            public bool? IsPassbook { get; set; }

            public IFormFile? PassBook { get; set; }

            [StringLength(20, ErrorMessage = "Account Number cannot exceed 20 characters.")]
            public string? AccountNumber { get; set; }

            [StringLength(50, ErrorMessage = "Bank Name cannot exceed 50 characters.")]
            public string? BankName { get; set; }

            [StringLength(11, ErrorMessage = "IFSC code cannot exceed 11 characters.")]
            public string? Ifsc { get; set; }

            [StringLength(50, ErrorMessage = "UPI ID cannot exceed 50 characters.")]
            public string? Upi { get; set; }

            public bool? IsDegree { get; set; }

            public IFormFile? Degree { get; set; }

            public bool? IsMarksheetUpload { get; set; }

            public IFormFile? Marksheet { get; set; }

            //[StringLength(200, ErrorMessage = "Other documents description cannot exceed 200 characters.")]
            public string? OtherDocuments { get; set; }

            public IFormFile? otherdocument { get; set; }

            public int TotalRecords { get; set; }

        }

    }
}
