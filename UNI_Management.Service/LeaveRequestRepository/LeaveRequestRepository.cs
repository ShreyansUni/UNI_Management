using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UNI_Management.Common.DependencyInjection;
using UNI_Management.DataModels;
using UNI_Management.Domain;
using UNI_Management.Domain.DataContext;
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
            if (model.leaveRequest.LeaveRequestId != default)
            {
                LeaveRequest leaveRequest = _context.LeaveRequests.FirstOrDefault(leaves => leaves.LeaveRequestId == model.leaveRequest.LeaveRequestId);
                leaveRequest.ReportingEmployeeId = 140;
                leaveRequest.ReasonForLeave = model.leaveRequest.ReasonForLeave;
                leaveRequest.LeaveStartDate = model.leaveRequest.LeaveStartDate;
                //leaveRequest.LeaveStartDateDuration = model.leaveRequest.LeaveStartType;
                leaveRequest.LeaveEndDate = model.leaveRequest.LeaveEndDate;
                //leaveRequest.LeaveEndDuration = model.LeaveEndType;
                leaveRequest.ActualLeaveDuration = model.leaveRequest.ActualLeaveDuration;
                leaveRequest.TotalLeaveDuration = model.leaveRequest.LeaveRequestId;
                leaveRequest.ReturnDate = model.leaveRequest.ReturnDate;
                leaveRequest.RequestedDate = model.leaveRequest.RequestedDate;
                leaveRequest.PhoneNumber = model.leaveRequest.PhoneNumber;
                leaveRequest.AlternatePhoneNumber = model.leaveRequest.AlternatePhoneNumber;
                leaveRequest.IsAvailableOnPhone = model.leaveRequest.IsAvailableOnPhone;
                leaveRequest.IsAdhocLeave = model.leaveRequest.IsAdhocLeave;
                leaveRequest.EmployeeId = model.leaveRequest.LeaveRequestId;
                leaveRequest.IsLeaveApproved = false;
                leaveRequest.CreatedAt = DateTime.Now;
                leaveRequest.CreatedBy = model.leaveRequest.LeaveRequestId;
                leaveRequest.ModifiedAt = DateTime.Now;
                leaveRequest.ModifiedBy = model.leaveRequest.LeaveRequestId;
                _context.LeaveRequests.Update(leaveRequest);
            }
            else
            {
                LeaveRequest leaveRequest = new();
                leaveRequest.ReportingEmployeeId = 140;
                leaveRequest.ReasonForLeave = model.leaveRequest.ReasonForLeave;
                leaveRequest.LeaveStartDate = model.leaveRequest.LeaveStartDate;
                leaveRequest.LeaveStartDateDuration = model.leaveRequest.LeaveRequestId;
                leaveRequest.LeaveEndDate = model.leaveRequest.LeaveEndDate;
                leaveRequest.LeaveEndDuration = model.leaveRequest.LeaveRequestId;
                leaveRequest.ActualLeaveDuration = model.leaveRequest.LeaveRequestId;
                leaveRequest.TotalLeaveDuration = model.leaveRequest.LeaveRequestId;
                leaveRequest.ReturnDate = model.leaveRequest.ReturnDate;
                leaveRequest.RequestedDate = model.leaveRequest.RequestedDate;
                leaveRequest.PhoneNumber = model.leaveRequest.PhoneNumber;
                leaveRequest.AlternatePhoneNumber = model.leaveRequest.AlternatePhoneNumber;
                leaveRequest.IsAvailableOnPhone = model.leaveRequest.IsAvailableOnPhone;
                leaveRequest.IsAdhocLeave = model.leaveRequest.IsAdhocLeave;
                leaveRequest.EmployeeId = model.leaveRequest.LeaveRequestId;
                leaveRequest.IsLeaveApproved = false;
                leaveRequest.CreatedAt = DateTime.Now;
                leaveRequest.CreatedBy = model.leaveRequest.LeaveRequestId;
                leaveRequest.ModifiedAt = DateTime.Now;
                leaveRequest.ModifiedBy = model.leaveRequest.LeaveRequestId;
                _context.LeaveRequests.Add(leaveRequest);
            }
            _context.SaveChanges();
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
                ReasonForLeave = leaveRecord.ReasonForLeave ?? "N/A",
                LeaveStartDate = leaveRecord.LeaveStartDate,
                LeaveEndDate = leaveRecord.LeaveEndDate,
                LeaveEndType = leaveRecord.LeaveEndDuration ?? default,
                LeaveStartType = leaveRecord.LeaveStartDateDuration ?? default,
                LeaveID = leaveRecord.LeaveRequestId,
                ActualLeaveDuration = leaveRecord.ActualLeaveDuration ?? default,
                TotalLeaveDuration = leaveRecord.TotalLeaveDuration ?? default,
                ReturnDate = leaveRecord.ReturnDate ?? default,
                RequestedDate = leaveRecord.RequestedDate ?? default,
                LeaveRequestorID = leaveRecord.EmployeeId,
                LeaveResponsorID = leaveRecord.ReportingEmployeeId,
                IsAdhocLeave = leaveRecord.IsAdhocLeave ?? default,
                IsAvailableOnPhone = leaveRecord.IsAvailableOnPhone ?? default,
                AlternatePhoneNumber = leaveRecord.AlternatePhoneNumber,
                PhoneNumber = leaveRecord.PhoneNumber,
            };
            Employee RequestingEmployeeName = _context.Employees.FirstOrDefault(emp => emp.EmployeeId == model.LeaveRequestorID);
            Employee ReportingEmployeeName = _context.Employees.FirstOrDefault(emp => emp.EmployeeId == model.LeaveResponsorID);
            model.LeaveRequestorName = RequestingEmployeeName.FirstName + " " + RequestingEmployeeName.LastName;
            model.LeaveRequestorName = ReportingEmployeeName.FirstName + " " + ReportingEmployeeName.LastName;
            model.EmployeeDropdownList = GetEmployeeListForDropDown();
            return model;
        }
    }
}
