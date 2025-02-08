using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UNI_Management.Domain;
using UNI_Management.Service;
namespace UNI_Management.Service
{
    public interface IEmployeeRepository
    {

        List<EmployeeDTO> GetEmployeeList(string? filterFirstName, DateTime? filterJoiningDate, int pageSize, int pageIndex, string columnName, string sortDirection);

        EmployeeDTO GetEmployeenData(int EmployeeId);
        //Task DeleteEmployeeAsync(int EmployeeId);
        //public void AddEmployee(Employee model);
        //public void UpdateEmployee(Employee model);
        //public Employee GetEmployeeDetails(int EmployeeId);
        //List<Employee> GetEmployeeListfilter(string filterName, string filterType, DateTime? filterJoinningDate, string filterIsActive);
        //List<Employee> isEmailExist(string Email);

        bool EmployeeAddEdit(EmployeeDTO employee);

        Task<(bool isDeleteSuccess, string Message)> EmployeeDeleteAsync(int? employeeId);

    }
}
