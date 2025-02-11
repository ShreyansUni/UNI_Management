using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UNI_Management.Domain;

namespace UNI_Management.Service
{
    public interface IWorkLogRepository
    {
        List<WorkLogDTO> WorkLogList(int? EmployeeId);

        Task<(bool IsSuccess, string Message)> WorkLogAdd(WorkLogDTO model);

        WorkLogDTO GetWorkLogDetails(int? WorkLogId);
    }
}
