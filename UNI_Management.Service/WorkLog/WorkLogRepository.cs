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
                string messege;
                var worklog = new WorkLogDTO();
                if (model.workLog.WorkLogId == null || model.workLog.WorkLogId == 0)
                {
                    var data = new WorkLog
                    {
                        Subject = model.workLog.Subject,
                        Message = model.workLog.Message,
                        SignOutTime = DateTime.Now,
                        CreatedDate = DateTime.Now,
                        EmployeeId = 131 
                    };
                    _context.Add(data);
                    messege = "WorkLog Added.";
                    _context.SaveChanges();
                }
                else
                {
                    WorkLog? workLog = _context.WorkLogs.Where(a => a.WorkLogId == model.workLog.WorkLogId).FirstOrDefault();
                    if (workLog == null)
                    {
                        throw new Exception("Lead Not Found");
                    }
                    workLog.Subject = model.workLog.Subject;
                    workLog.Message = model.workLog.Message;
                    _context.WorkLogs.Update(workLog);
                    messege = "WorkLog Updated.";
                }

                await _context.SaveChangesAsync();
                return (true, messege);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw;
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
    }
}
