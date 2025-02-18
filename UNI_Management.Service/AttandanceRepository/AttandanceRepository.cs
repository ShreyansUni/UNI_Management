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
        public void AddAttandance(int day, int month, int year, short status, int UserId)
        {
            try
            {
                if(DateTime.Now.Day == day)
                {
                    var existingAttendance = _context.EmployeeAttendances
                                            .Where(e => e.Day == day && e.EmployeeId == UserId).FirstOrDefault();
                    if (existingAttendance == null) {
                        var EmpAttandace = new EmployeeAttendance();
                        EmpAttandace.EmployeeId = UserId;
                        EmpAttandace.Status = status;
                        EmpAttandace.Day = day;
                        EmpAttandace.Month = month;
                        EmpAttandace.Year = year;
                        EmpAttandace.Created = DateTime.Now;

                        _context.EmployeeAttendances.Add(EmpAttandace);
                    }
                    else
                    {
                        existingAttendance.Status = status;
                        existingAttendance.Day = day;
                        existingAttendance.Month = month;
                        existingAttendance.Year = year;
                        existingAttendance.Modified = DateTime.Now;

                        _context.EmployeeAttendances.Update(existingAttendance);
                    }
                }
                
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
                     .Where(e => e.Month == (month) && e.Year == year && e.EmployeeId == E)
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
