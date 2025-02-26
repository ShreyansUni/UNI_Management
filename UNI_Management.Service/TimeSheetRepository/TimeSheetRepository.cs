using Microsoft.EntityFrameworkCore;
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
        //private readonly AttandanceRepository _attandanceRepository;
        //private readonly WorkLogRepository _workLogRepository;
        public TimeSheetRepository(ApplicationDbContext context)
        {
            _context = context;
            //_attandanceRepository = attandanceRepository;
            //_workLogRepository = workLogRepository;
        }
        #endregion

        #region GetTimeSheetData
        //public List<TimeSheetDTO> GetTimeSheetData(int UserId)
        //{
        //    var workLogs = _context.WorkLogs.ToList();
        //    var attendances = _context.EmployeeAttendances.ToList();

        //    if (workLogs == null || attendances == null || UserId == -1)
        //    {
        //        return new List<TimeSheetDTO>();
        //    }

        //    var timeSheetDTOList = (from attendance in attendances
        //                            join workLog in workLogs
        //                            on attendance.EmployeeId equals workLog.EmployeeId into workLogGroup
        //                            from workLog in workLogGroup.DefaultIfEmpty()
        //                            where attendance.EmployeeId == UserId
        //                            select new TimeSheetDTO
        //                            {
        //                                workLog = workLog,
        //                                attandence = attendance
        //                            }).ToList();

        //    return timeSheetDTOList;
        //}
        public List<TimeSheetDTO> GetTimeSheetData(int UserId)
        {
            var workLogs = _context.WorkLogs
                .Where(w => w.EmployeeId == UserId) // Fetch only relevant work logs
                .ToList();

            var attendances = _context.EmployeeAttendances
                .Where(a => a.EmployeeId == UserId) // Fetch only relevant attendances
                .ToList();

            if (!workLogs.Any() && !attendances.Any())
            {
                return new List<TimeSheetDTO>();
            }

            var timeSheetDTOList = (from attendance in attendances
                                    join workLog in workLogs
                                    on new
                                    {
                                        EmployeeId = attendance.EmployeeId,
                                        AttendanceDate = attendance.Created?.Date ?? DateTime.MinValue.Date
                                    }
                                    equals new
                                    {
                                        EmployeeId = workLog.EmployeeId ?? 0,
                                        AttendanceDate = workLog.SignOutTime?.Date ?? DateTime.MinValue.Date
                                    }
                                    into workLogGroup
                                    from workLog in workLogGroup.DefaultIfEmpty()
                                    select new TimeSheetDTO
                                    {
                                        attandence = attendance,
                                        workLog = workLog?.SignOutTime != null ? workLog : null
                                    }).ToList();

            return timeSheetDTOList;
        }
        #endregion

        //#region UpdateTimesheetdata

        //public async Task UpdateTimeSheet(int UserId)
        //{
        //    var attadanceData = await _attandanceRepository.GetAttendanceForUser(UserId);
        //    var worklogData = await _workLogRepository.GetWorkLogForUser(UserId);

        //}
        //#endregion

        //public async Task UpdateTimeSheetData(int UserId)
        //{
        //    if (UserId == -1)
        //        return;

        //    var attendanceData = await _context.EmployeeAttendances
        //                                       .Where(a => a.EmployeeId == UserId)
        //                                       .ToListAsync();

        //    var worklogData = await _context.WorkLogs
        //                                    .Where(w => w.EmployeeId == UserId)
        //                                    .ToListAsync();

        //    var timeSheet = await _context
        //                                  .FirstOrDefaultAsync(t => t.EmployeeId == UserId);

        //    if (timeSheet == null)
        //    {
        //        timeSheet = new TimeSheet
        //        {
        //            EmployeeId = UserId,
        //            CreatedDate = DateTime.UtcNow
        //        };
        //        _context.TimeSheets.Add(timeSheet);
        //    }

        //    // Calculate total hours worked from work logs
        //    timeSheet.TotalHoursWorked = worklogData.Sum(w => w.Hours);

        //    // Count attendance days marked as '1' (Present)
        //    timeSheet.TotalDaysPresent = attendanceData.Count(a => a.Status == 1);

        //    // Update last modified date
        //    timeSheet.LastUpdatedDate = DateTime.UtcNow;

        //    await _context.SaveChangesAsync();
        //}

    }
}
