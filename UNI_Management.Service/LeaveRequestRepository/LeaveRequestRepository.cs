using Microsoft.Bot.Streaming.Payloads;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UNI_Management.Common;
using UNI_Management.Common.DependencyInjection;
using UNI_Management.Common.Email;
using UNI_Management.Common.Utility;
using UNI_Management.Domain;
using UNI_Management.Domain.DataContext;
using UNI_Management.Domain.DataModels;
using UNI_Management.Service;
using UNIManagement.Repositories.Repository.InterFace;

namespace UNIManagement.Repositories.Repository
{
    [TransientDependency(ServiceType = typeof(ILeaveRequestRepository))]
    public class LeaveRequestRepository : ILeaveRequestRepository
    {
        private readonly ApplicationDbContext _context;
        public LeaveRequestRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public void SubmitLeave(LeaveRequestDTO model)
        {
            Employee employee = _context.Employees.FirstOrDefault(emp => emp.EmployeeId == model.leaveRequest.EmployeeId);
            if (employee != null)
            {
                if (model.leaveRequest.LeaveRequestId != default)
                {
                    LeaveRequest leaveRequest = _context.LeaveRequests.FirstOrDefault(leaves => leaves.LeaveRequestId == model.leaveRequest.LeaveRequestId);
                    leaveRequest.ReportingEmployeeId = model.leaveRequest.EmployeeId;
                    leaveRequest.ReasonForLeave = model.leaveRequest.ReasonForLeave;
                    leaveRequest.LeaveStartDate = model.leaveRequest.LeaveStartDate;
                    //leaveRequest.LeaveStartDateDuration = model.leaveRequest.LeaveStartType;
                    leaveRequest.LeaveEndDate = model.leaveRequest.LeaveEndDate;
                    //leaveRequest.LeaveEndDuration = model.LeaveEndType;
                    leaveRequest.ActualLeaveDuration = model.leaveRequest.ActualLeaveDuration;
                    leaveRequest.TotalLeaveDuration = model.leaveRequest.TotalLeaveDuration;
                    leaveRequest.ReturnDate = model.leaveRequest.ReturnDate;
                    leaveRequest.RequestedDate = model.leaveRequest.RequestedDate;
                    leaveRequest.PhoneNumber = model.leaveRequest.PhoneNumber;
                    leaveRequest.AlternatePhoneNumber = model.leaveRequest.AlternatePhoneNumber;
                    leaveRequest.IsAvailableOnPhone = model.leaveRequest.IsAvailableOnPhone;
                    leaveRequest.IsAdhocLeave = model.leaveRequest.IsAdhocLeave;
                    leaveRequest.EmployeeId = model.leaveRequest.EmployeeId;
                    leaveRequest.IsLeaveApproved = false;
                    leaveRequest.CreatedAt = DateTime.Now;
                    leaveRequest.CreatedBy = model.leaveRequest.EmployeeId;
                    leaveRequest.ModifiedAt = DateTime.Now;
                    leaveRequest.ModifiedBy = model.leaveRequest.EmployeeId;
                    _context.LeaveRequests.Update(leaveRequest);
                    _context.SaveChanges();

                    string EmailBody = "";
                    string EmailSubject = employee.FirstName + " " + employee.LastName + " - Leave Request (EDITED)";
                    if (model.leaveRequest.LeaveStartDate != model.leaveRequest.LeaveEndDate)
                    {
                        EmailBody = "EDITED: " + employee.FirstName + " " + employee.LastName + " is requesting " + model.leaveRequest.ActualLeaveDuration + " days leave from " + model.leaveRequest.LeaveStartDate + " (" + ((Enums.LeaveType)model.LeaveStartType).ToString() + ") to " + model.leaveRequest.LeaveEndDate + " (" + ((Enums.LeaveType)model.LeaveEndType).ToString() + ").<br>" + "Reason of Leave: <br>" + model.leaveRequest.ReasonForLeave + "<br>";
                    }
                    else
                    {
                        EmailBody = "EDITED: " + employee.FirstName + " " + employee.LastName + " is requesting " + model.leaveRequest.ActualLeaveDuration + " day leave on " + model.leaveRequest.LeaveStartDate + " (" + ((Enums.LeaveType)model.LeaveStartType).ToString() + "). <br> Reason Of Leave: <br>" + model.leaveRequest.ReasonForLeave + "<br>";
                    }
                    EmailHelper.SendMail(employee.Email, EmailSubject, EmailBody);
                }


                else
                {
                    LeaveRequest leaveRequest = new();
                    leaveRequest.ReportingEmployeeId = model.leaveRequest.EmployeeId;
                    leaveRequest.ReasonForLeave = model.leaveRequest.ReasonForLeave;
                    leaveRequest.LeaveStartDate = model.leaveRequest.LeaveStartDate;
                    leaveRequest.LeaveStartDateDuration = model.leaveRequest.LeaveStartDateDuration;
                    leaveRequest.LeaveEndDate = model.leaveRequest.LeaveEndDate;
                    leaveRequest.LeaveEndDuration = model.leaveRequest.LeaveEndDuration;
                    leaveRequest.ActualLeaveDuration = model.leaveRequest.ActualLeaveDuration;
                    leaveRequest.TotalLeaveDuration = model.leaveRequest.TotalLeaveDuration;
                    leaveRequest.ReturnDate = model.leaveRequest.ReturnDate;
                    leaveRequest.RequestedDate = model.leaveRequest.RequestedDate;
                    leaveRequest.PhoneNumber = model.leaveRequest.PhoneNumber;
                    leaveRequest.AlternatePhoneNumber = model.leaveRequest.AlternatePhoneNumber;
                    leaveRequest.IsAvailableOnPhone = model.leaveRequest.IsAvailableOnPhone;
                    leaveRequest.IsAdhocLeave = model.leaveRequest.IsAdhocLeave;
                    leaveRequest.EmployeeId = model.leaveRequest.EmployeeId;
                    leaveRequest.IsLeaveApproved = false;
                    leaveRequest.CreatedAt = DateTime.Now;
                    leaveRequest.CreatedBy = model.leaveRequest.EmployeeId;
                    leaveRequest.ModifiedAt = DateTime.Now;
                    leaveRequest.ModifiedBy = model.leaveRequest.EmployeeId;
                    _context.LeaveRequests.Add(leaveRequest);
                    _context.SaveChanges();

                    string EmailBody = "";
                    string EmailSubject = employee.FirstName + " " + employee.LastName + " - Leave Request";
                    if (model.leaveRequest.LeaveRequestId != model.leaveRequest.LeaveRequestId)
                    {
                        EmailBody = employee.FirstName + " " + employee.LastName + " is requesting " + model.leaveRequest.ActualLeaveDuration + " days leave from " + model.leaveRequest.LeaveStartDate + " (" + ((Enums.LeaveType)model.LeaveStartType).ToString() + ") to " + model.leaveRequest.LeaveEndDate + " (" + ((Enums.LeaveType)model.LeaveEndType).ToString() + ").<br>" + "Reason of Leave:<br>" + model.leaveRequest.ReasonForLeave + "<br>";

                    }
                    else
                    {
                        EmailBody = employee.FirstName + " " + employee.LastName + " is requesting " + model.leaveRequest.ActualLeaveDuration + " day leave on " + model.leaveRequest.LeaveStartDate + " (" + ((Enums.LeaveType)model.LeaveStartType).ToString() + "). <br> Reason Of Leave: <br>" + model.leaveRequest.ReasonForLeave + "<br>";
                    }
                    EmailHelper.SendMail(employee.Email, EmailSubject, EmailBody);
                }
            }
            
        }
        public List<Employee> GetEmployeeListForDropDown()
        {
            List<Employee> EmployeeList = _context.Employees.Where(emp => emp.IsDeleted == false).ToList();
            return EmployeeList;
        }
        public List<LeaveRequestDTO> GetLeaveRequestList(int userId)
        {
            List<LeaveRequestDTO> model = new();
            if (userId == -1)
            {
                return model;
            }
            model = (from leaveRequests in _context.LeaveRequests
                     where leaveRequests.EmployeeId == userId && leaveRequests.DeletedAt == null
                     orderby leaveRequests.LeaveStartDate descending
                     select new LeaveRequestDTO
                     {
                         leaveRequest = leaveRequests,
                     }).ToList();
            return model;
        }
        public void DeleteLeaveRecord(int leaveRequestId)
        {
            LeaveRequest request = _context.LeaveRequests.FirstOrDefault(leave => leave.LeaveRequestId == leaveRequestId);
            if (request != null)
            {
                request.DeletedAt = DateTime.Now;
                _context.LeaveRequests.Update(request);
            }
            _context.SaveChanges();
        }
        public LeaveRequestDTO GetLeaveRecord(int leaveRequestId)
        {
            LeaveRequest leaveRecord = _context.LeaveRequests.FirstOrDefault(record => record.LeaveRequestId == leaveRequestId);
            LeaveRequestDTO model = new()
            {
                leaveRequest = leaveRecord
            };
            Employee RequestingEmployeeName = _context.Employees.FirstOrDefault(emp => emp.EmployeeId == model.leaveRequest.EmployeeId);
            Employee ReportingEmployeeName = _context.Employees.FirstOrDefault(emp => emp.EmployeeId == model.leaveRequest.ReportingEmployeeId);
            model.LeaveRequestorName = RequestingEmployeeName.FirstName + " " + RequestingEmployeeName.LastName;
            model.LeaveRequestorName = ReportingEmployeeName.FirstName + " " + ReportingEmployeeName.LastName;
            model.EmployeeDropdownList = GetEmployeeListForDropDown();
            return model;
        }

        List<Employee> ILeaveRequestRepository.GetEmployeeListForDropDown()
        {
            throw new NotImplementedException();
        }
    }
}
