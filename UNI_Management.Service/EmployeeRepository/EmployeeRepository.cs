using Microsoft.Bot.Builder.Adapters;
using Microsoft.Bot.Streaming.Payloads;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using UNI_Management.Common;
using UNI_Management.Common.DependencyInjection;
using UNI_Management.Common.Utility;
using UNI_Management.Domain;
using UNI_Management.Domain.DataContext;
using UNI_Management.Domain.DataModels;



namespace UNI_Management.Service
{
    [TransientDependency(ServiceType = typeof(IEmployeeRepository))]
    public class EmployeeRepository : IEmployeeRepository
    {
        #region Constructor
        private readonly ApplicationDbContext _context;
        public EmployeeRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        #endregion

        #region EmployeeAll
        /// <summary>
        /// Retrive Lists from Employee Table 
        /// </summary>
        /// <returns></returns>
        public List<EmployeeDTO> GetEmployeeList(string? filterFirstName, DateTime? filterJoiningDate, string? filterEmployeeType, int pageSize, int pageIndex, string columnName, string sortDirection)
        {
            var employeeList = _context.Employees
                               .Where(employee => employee.IsDeleted == false &&
                               (string.IsNullOrEmpty(filterFirstName) || employee.FirstName.Contains(filterFirstName)) &&
                               (!filterJoiningDate.HasValue ||
                               (employee.Joinningdate.HasValue &&
                               DateOnly.FromDateTime(employee.Joinningdate.Value) == DateOnly.FromDateTime(filterJoiningDate.Value))) &&
                               (string.IsNullOrEmpty(filterEmployeeType) || employee.EmployeeType == filterEmployeeType))
                               .ToList();

            switch (columnName)
            {
                case "FirstName":
                    employeeList = sortDirection == "asc"
                        ? employeeList.OrderBy(a => a.FirstName).ToList()
                        : employeeList.OrderByDescending(a => a.FirstName).ToList();
                    break;
                case "IpAddress":
                    employeeList = sortDirection == "asc"
                        ? employeeList.OrderBy(a => a.Joinningdate).ToList()
                        : employeeList.OrderByDescending(a => a.Joinningdate).ToList();
                    break;
                default:
                    employeeList = sortDirection == "asc"
                        ? employeeList.OrderBy(a => a.ModifiedBy).ToList()
                        : employeeList.OrderByDescending(a => a.ModifiedBy).ToList();
                    break;
            }

            int totalRecords = employeeList.Count();
            employeeList = employeeList.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

            var employeeDTOList = employeeList.Select(employee => new EmployeeDTO
            {
                Employee = employee, 
                TotalRecords = totalRecords  
            }).ToList();

            return employeeDTOList;
        }
        #endregion

        public List<EmployeeDTO> GetEmployeeDataWithFilter(string? filterEmployeeType, int? employeeId)
        {
            var employeeList = _context.Employees
                .Where(employee => employee.IsDeleted == false &&
                       (string.IsNullOrEmpty(filterEmployeeType) || employee.EmployeeType == filterEmployeeType) &&
                       (employee.EmployeeType == "BDE" || employee.EmployeeType == "Technical"))
                .Select(employee => new EmployeeDTO
                {
                    Employee = employee
                })
                .Distinct()
                .ToList();

            return employeeList;
        }


        public EmployeeDTO GetEmployeenData(int EmployeeId)
        {
            var employee = _context.Employees
                            .FirstOrDefault(emp => emp.EmployeeId == EmployeeId && emp.IsDeleted == false);
            var employeeAttachments = _context.EmployeeAttachments
                                        .Where(att => att.EmployeeId == EmployeeId)
                                        .ToList();

            return new EmployeeDTO
            {
                Employee = employee,
                employeeAttachment = employeeAttachments.FirstOrDefault()
            };
        }

        public bool EmployeeAddEdit(EmployeeDTO employee)
        {
            try
            {
                var checkEmployeeEmail = _context.Employees.Where(x => x.Email == employee.Employee.Email).FirstOrDefault();
                if (checkEmployeeEmail != null && checkEmployeeEmail.EmployeeId != employee.Employee.EmployeeId)
                {
                    return false;
                }
                if(employee.Employee.EmployeeId == 0)
                {
                    var newEmployee = new Employee
                    {
                        UserName = employee.Employee.FirstName,
                        Password = employee.Employee.MiddleName,
                        FirstName = employee.Employee.FirstName,
                        LastName = employee.Employee.LastName,
                        Email = employee.Employee.Email,
                        MiddleName = employee.Employee.MiddleName,
                        Address = employee.Employee.Address,
                        ContactNumber1 = employee.Employee.ContactNumber1,
                        ContactNumber2 = employee.Employee.ContactNumber2,
                        Country = employee.Employee.Country,
                        State = employee.Employee.State,
                        Birthdate = employee.Employee.Birthdate,
                        Education = employee.Employee.Education,
                        Gender = employee.Employee.Gender,
                        IsActive = employee.Employee.IsActive,
                        Joinningdate = employee.Employee.Joinningdate,
                        IsFresher = employee.Employee.IsFresher,
                        EmployeeType = employee.Employee.EmployeeType,
                        Bond = employee.Employee.Bond,
                        CreatedBy = employee.Employee.EmployeeId,
                        Created = DateTime.Now,
                        IsDeleted = false,
                    };
                    _context.Employees.Add(newEmployee);
                    _context.SaveChanges();

                    if (employee.employeeAttachment.EmployeeAttachmentId == 0)
                    {
                        var employeeAttachment = new EmployeeAttachment
                        {
                            EmployeeId = newEmployee.EmployeeId,
                            AdharNo = employee.employeeAttachment.AdharNo,
                            IsAdhar = employee.employeeAttachment.IsAdhar,
                            IsDegree = employee.employeeAttachment.IsDegree,
                            IsMarksheetUpload = employee.employeeAttachment.IsMarksheetUpload,
                            IsPassbook = employee.employeeAttachment.IsPassbook,
                            AccountNumber = employee.employeeAttachment.AccountNumber,
                            BankName = employee.employeeAttachment.BankName,
                            Ifsc = employee.employeeAttachment.Ifsc,
                            Upi = employee.employeeAttachment.Upi,
                            Created = DateTime.Now,
                            CreatedBy = employee.employeeAttachment.EmployeeAttachmentId,
                        };
                        _context.EmployeeAttachments.Add(employeeAttachment);
                        _context.SaveChanges();
                    }
                    return true;

                }
                else
                {
                    var existingEmployee = _context.Employees
                                                   .Include(x => x.EmployeeAttachments)
                                                   .FirstOrDefault(x => x.EmployeeId == employee.Employee.EmployeeId);
                    if (existingEmployee == null)
                    {
                        return false;
                    }

                    existingEmployee.FirstName = employee.Employee.FirstName;
                    existingEmployee.LastName = employee.Employee.LastName;
                    existingEmployee.MiddleName = employee.Employee.MiddleName;
                    existingEmployee.Email = employee.Employee.Email;
                    existingEmployee.Address = employee.Employee.Address;
                    existingEmployee.ContactNumber1 = employee.Employee.ContactNumber1;
                    existingEmployee.ContactNumber2 = employee.Employee.ContactNumber2;
                    existingEmployee.Country = employee.Employee.Country;
                    existingEmployee.State = employee.Employee.State;
                    existingEmployee.Birthdate = employee.Employee.Birthdate;
                    existingEmployee.Education = employee.Employee.Education;
                    existingEmployee.Gender = employee.Employee.Gender;
                    existingEmployee.IsActive = employee.Employee.IsActive;
                    existingEmployee.Joinningdate = employee.Employee.Joinningdate;
                    existingEmployee.IsFresher = employee.Employee.IsFresher;
                    existingEmployee.EmployeeType = employee.Employee.EmployeeType;
                    existingEmployee.Bond = employee.Employee.Bond;
                    existingEmployee.Modified = DateTime.Now;
                    existingEmployee.ModifiedBy = employee.Employee.EmployeeId;

                    _context.Employees.Update(existingEmployee);
                    _context.SaveChanges();

                    if(existingEmployee.EmployeeAttachments != null)
                    {
                        var existingAttachment = _context.EmployeeAttachments
                                                 .FirstOrDefault(x => x.EmployeeId == existingEmployee.EmployeeId);
                        if (existingAttachment == null)
                        {
                            return false;
                        }

                        existingAttachment.AdharNo = employee.employeeAttachment.AdharNo;
                        existingAttachment.IsAdhar = employee.employeeAttachment.IsAdhar;
                        existingAttachment.IsDegree = employee.employeeAttachment.IsDegree;
                        existingAttachment.IsMarksheetUpload = employee.employeeAttachment.IsMarksheetUpload;
                        existingAttachment.IsPassbook = employee.employeeAttachment.IsPassbook;
                        existingAttachment.AccountNumber = employee.employeeAttachment.AccountNumber;
                        existingAttachment.BankName = employee.employeeAttachment.BankName;
                        existingAttachment.Ifsc = employee.employeeAttachment.Ifsc;
                        existingAttachment.Upi = employee.employeeAttachment.Upi;
                        existingAttachment.Modified = DateTime.Now;
                        existingAttachment.ModifiedBy = existingEmployee.EmployeeId;

                        _context.EmployeeAttachments.Update(existingAttachment);
                        _context.SaveChanges();
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }

        public async Task<(bool isDeleteSuccess, string Message)> EmployeeDeleteAsync(int? employeeId)
        {
            try
            {
                string message;
                var existingEmployee = (from employee in _context.Employees
                                        join employeeattachment in _context.EmployeeAttachments
                                        on employee.EmployeeId equals employeeattachment.EmployeeId into attachments
                                        from employeeattachment in attachments.DefaultIfEmpty()
                                        where employee.EmployeeId == employeeId && employee.IsDeleted == false
                                        select new
                                        {
                                            employee,
                                            employeeattachment
                                        })
                                        .FirstOrDefault();

                if (existingEmployee != null)
                {
                    //if (existingagency.agency.Companylogo != null)
                    //{
                    //    string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", existingagency.agency.Companylogo);
                    //    if (File.Exists(imagePath))
                    //    {
                    //        File.Delete(imagePath); // Delete the image file from the server
                    //    }
                    //}
                    existingEmployee.employee.IsDeleted = true;

                    _context.Employees.Update(existingEmployee.employee);
                    _context.EmployeeAttachments.Update(existingEmployee.employeeattachment);
                    await _context.SaveChangesAsync();
                    message = ConstantMessage.EmployeeDeleteSuccess;
                    return (true, message);
                }

                message = ConstantMessage.EmployeeDeleteUnSuccess;
                return (false, message);
            }
            catch (Exception ex)
            {
                // Log the error (optional)
                Console.WriteLine($"An error occurred: {ex.Message}");
                return (false, ConstantMessage.EmployeeDeleteUnSuccess);
            }
        }

    }
}
