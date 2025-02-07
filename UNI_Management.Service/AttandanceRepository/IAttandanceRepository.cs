using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UNI_Management.DataModels;
using UNI_Management.Domain;

namespace UNI_Management.Service
{
    public interface IAttandanceRepository
    {
        public void AddAttandance(int day, int month, int year, short status);
        List<AttandenceDTO> GetAttandace(int year, int month,int EmployeeId);
    }
}
