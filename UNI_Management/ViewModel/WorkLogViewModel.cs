using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNI_Management.ViewModel
{
    public class WorkLogViewModal : BaseModelViewModel
    {
        public WorkLogViewModal()
        {
            workLogDetails = new WorkLogDetails();
            workLogList = new List<WorkLogDetails>();
        }

        public List<WorkLogDetails> workLogList { get; set; }

        public WorkLogDetails workLogDetails { get; set; }

        public class WorkLogDetails
        {
            public int WorkLogId { get; set; }

            public int? EmployeeId { get; set; }

            public string? Message { get; set; }
            public string? Subject { get; set; }

            public DateTime? SignOutTime { get; set; }

            public DateTime? CreatedDate { get; set; }
        }
        
    }
}
