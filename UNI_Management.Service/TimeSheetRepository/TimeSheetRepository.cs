using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UNI_Management.Common.DependencyInjection;
using UNI_Management.Domain;
using UNI_Management.Domain.DataContext;
using UNI_Management.Domain.DataModels;

namespace UNI_Management.Service.TimeSheetRepository
{
    [TransientDependency(ServiceType = typeof(ITimeSheetRepository))]
    public class TimeSheetRepository:ITimeSheetRepository
    {
        #region Constructor
        private readonly ApplicationDbContext _context;
        public TimeSheetRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        #endregion

        #region GetTimeSheetData
        public List<TimeSheetDTO> GetTimeSheetData(int UserId)
        {
            var workLogs = _context.WorkLogs.ToList();
            var attendances = _context.EmployeeAttendances.ToList();

            if (workLogs == null || attendances == null || UserId == -1)
            {
                return new List<TimeSheetDTO>();
            }

            var timeSheetDTOList = (from attendance in attendances
                                    join workLog in workLogs
                                    on attendance.EmployeeId equals workLog.EmployeeId into workLogGroup
                                    from workLog in workLogGroup.DefaultIfEmpty()  
                                    where attendance.EmployeeId == UserId
                                    select new TimeSheetDTO
                                    {
                                        workLog = workLog,  
                                        attandence = attendance
                                    }).ToList();




            return timeSheetDTOList;
        }   
        #endregion
    }
}
