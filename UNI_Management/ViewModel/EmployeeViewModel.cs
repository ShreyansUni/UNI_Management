﻿using AspNetCoreGeneratedDocument;
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

            [Required(ErrorMessage = "FirstName is Required")]
            public string FirstName { get; set; } = null!;

            [Required(ErrorMessage = "MiddleName is Required")]
            public string MiddleName { get; set; } = null!;

            [Required(ErrorMessage = "LastName is Required")]
            public string LastName { get; set; } = null!;

            public string UserName { get; set; } = null!;

            public string Password { get; set; } = null!;

            [Required(ErrorMessage = "ContactNumber is Required")]
            public string? ContactNumber1 { get; set; }

            public string? ContactNumber2 { get; set; }

            [Required(ErrorMessage = "Email is Required")]
            public string? Email { get; set; }

            [Required(ErrorMessage = "Address is Required")]
            public string? Address { get; set; }

            public string? Country { get; set; }

            public string? State { get; set; }

            [Required(ErrorMessage = "BirthDate is Required")]
            public DateTime? Birthdate { get; set; }

            [Required(ErrorMessage = "Education is Required")]
            public string? Education { get; set; }

            public string? Photo { get; set; }

            public IFormFile? EmployeeImage { get; set; }

            public DateTime? Joinningdate { get; set; }

            public string? IsFresher { get; set; }

            public string? Resume { get; set; }

            //public IFormFile? EmployeeResume { get; set; }
            [Required(ErrorMessage = "Bond is Required")]
            public string? Bond { get; set; }

            public bool? IsDeleted { get; set; }

            public string IsActive { get; set; } = null!;

            public string? Description { get; set; }

            public DateTime Created { get; set; }

            public int CreatedBy { get; set; }

            public DateTime? Modified { get; set; }

            public int? ModifiedBy { get; set; }

            public string? EmployeeType { get; set; }

            public string? Gender { get; set; }

            public int EmployeeAttachmentId { get; set; }

            public bool? IsAdhar { get; set; }

            public IFormFile? AdharCard { get; set; }

            public string? AdharNo { get; set; }

            public bool? IsPassbook { get; set; }

            public IFormFile? PassBook { get; set; }

            public string? AccountNumber { get; set; }

            public string? BankName { get; set; }

            public string? Ifsc { get; set; }

            public string? Upi { get; set; }

            public bool? IsDegree { get; set; }

            public IFormFile? Degree { get; set; }

            public bool? IsMarksheetUpload { get; set; }

            public IFormFile? Marksheet { get; set; }

            public string? OtherDocuments { get; set; }

            public IFormFile? otherdocument { get; set; }
            public int TotalRecords { get; set; }

        }

    }
}
