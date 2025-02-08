using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UNI_Management.Domain;
using UNI_Management.Domain.DataModels;

namespace UNIManagement.Repositories.Repository.InterFace
{
    public interface ILeaveRequestRepository
    {
        public void SubmitLeave(LeaveRequestDTO model);
        public List<Employee> GetEmployeeListForDropDown();
        public List<LeaveRequestDTO> GetLeaveRequestList(int userId);
        public void DeleteLeaveRecord(int leaveRequestId);
        public LeaveRequestDTO GetLeaveRecord(int leaveRequestId);
    }
}
