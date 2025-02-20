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

namespace UNI_Management.Service
{
    [TransientDependency(ServiceType = typeof(IWorkLogRepository))]
    public class WorkLogRepository : IWorkLogRepository
    {
        private readonly ApplicationDbContext _context;
        public WorkLogRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public List<WorkLogDTO> WorkLogList(int? EmployeeId)
        {
            var data = _context.WorkLogs
                               .Where(a => a.EmployeeId == EmployeeId)
                               .OrderByDescending(cont => cont.SignOutTime)
                               .Select(cont => new WorkLogDTO()
            {
                workLog = cont
            }).ToList();

            return data;
        }

        public async Task<(bool IsSuccess, string Message)> WorkLogAdd(WorkLogDTO model)
        {
            try
            {
                string message;
                var currentDate = DateTime.Now.Date; // Get today's date without time part

                // Check if the user has already added a work log for today
                var existingWorkLog = _context.WorkLogs
                    .FirstOrDefault(wl => wl.EmployeeId == model.workLog.EmployeeId && wl.CreatedDate.Value.Date == currentDate);

                if (model.workLog.WorkLogId == null || model.workLog.WorkLogId == 0) // Adding new work log
                {
                    if (existingWorkLog != null) // Check if there's already a work log for today
                    {
                        message = "You can only add one work log per day. Please edit the existing work log.";
                        return (false, message); // Return a message indicating that the user cannot add a new work log
                    }

                    // No existing work log for today, so we can add a new one
                    var data = new WorkLog
                    {
                        Subject = model.workLog.Subject,
                        Message = model.workLog.Message,
                        SignOutTime = DateTime.Now,
                        CreatedDate = DateTime.Now,
                        EmployeeId = model.workLog.EmployeeId
                    };

                    _context.Add(data);
                    message = "WorkLog Added.";
                }
                else // Editing existing work log
                {
                    WorkLog? workLog = _context.WorkLogs.Where(a => a.WorkLogId == model.workLog.WorkLogId).FirstOrDefault();
                    if (workLog == null)
                    {
                        throw new Exception("WorkLog Not Found");
                    }

                    // If there's an existing work log for today, update it
                    workLog.Subject = model.workLog.Subject;
                    workLog.Message = model.workLog.Message;
                    _context.WorkLogs.Update(workLog);
                    message = "WorkLog Updated.";
                }

                await _context.SaveChangesAsync();
                return (true, message);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return (false, "An error occurred while processing the request.");
            }
        }


        public WorkLogDTO GetWorkLogDetails(int? WorkLogId)
        {
            var data = _context.WorkLogs.FirstOrDefault(a => a.WorkLogId == WorkLogId);
            return new WorkLogDTO
            {
                workLog = data
            };
        }

        //public async Task<List<WorkLog>> GetWorkLogForUser(int userId)
        //{
        //    return await _context.WorkLogs
        //                         .Where(w => w.EmployeeId == userId)
        //                         .ToListAsync();
        //}

    }
}
