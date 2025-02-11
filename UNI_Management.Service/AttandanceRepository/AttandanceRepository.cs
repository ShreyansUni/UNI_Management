using UNI_Management.Common.DependencyInjection;
using UNI_Management.Common.Utility;
using UNI_Management.Domain;
using UNI_Management.Domain.DataContext;
using UNI_Management.Domain.DataModels;
using UNI_Management.Service;

namespace UNI_Management.Service
{
    [TransientDependency(ServiceType = typeof(IAttandanceRepository))]
    public class AttandanceRepository : IAttandanceRepository
    {
        #region Constructor
        private readonly ApplicationDbContext _context;
        public AttandanceRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        #endregion

        #region Add
        public void AddAttandance(int day, int month, int year, short status)
        {
            try
            {
                var EmpAttandace = new EmployeeAttendance();
                EmpAttandace.EmployeeId = 131;
                EmpAttandace.Status = status;
                EmpAttandace.Day = day;
                EmpAttandace.Month = month;
                EmpAttandace.Year = year;
                EmpAttandace.Created = DateTime.Now;

                _context.EmployeeAttendances.Add(EmpAttandace);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                return;
            }
        }
        #endregion

        #region GetAttandace
        public List<AttandenceDTO> GetAttandace(int year, int month, int E)
        {
            var list = _context.EmployeeAttendances
                     .Where(e => e.Month == (month+1) && e.Year == year && e.EmployeeId == E)
                     .Select(cont => new AttandenceDTO()
                     {
                         employeeAttendance = cont
                     })
                     .ToList();

            return list;
        }
        #endregion
    }
}
