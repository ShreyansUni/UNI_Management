using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UNI_Management.Domain;
using UNI_Management.Domain.DataModels;

namespace UNI_Management.Service
{
    public interface IAttandanceRepository
    {
        public void AddAttandance(int day, int month, int year, short status,int UserId);
        List<AttandenceDTO> GetAttandace(int year, int month,int EmployeeId);
        //Task<List<EmployeeAttendance>> GetAttendanceForUser(int userId);
    }
}
