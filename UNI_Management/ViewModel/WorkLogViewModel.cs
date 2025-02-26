using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        public int SelectedEmployeeId { get; set; }

        public class WorkLogDetails
        {
            public int WorkLogId { get; set; }

            [Required(ErrorMessage = "Employee ID is required")]
            public int? EmployeeId { get; set; }

            public string EmployeeName { get; set; }

            [Required(ErrorMessage = "Subject is required")]
            [StringLength(100, ErrorMessage = "Subject cannot exceed 100 characters")]
            [RegularExpression(@"^(?!\s*$).+", ErrorMessage = "Subject cannot be only whitespace.")]
            public string Subject { get; set; }

            [Required(ErrorMessage = "Note is required")]
            [StringLength(500, ErrorMessage = "Note cannot exceed 500 characters")]
            [RegularExpression(@"^(?!\s*$).+", ErrorMessage = "Note cannot be only whitespace.")]
            public string Note { get; set; }

            [Required(ErrorMessage = "Work date is required")]
            public DateTime? WorkDate { get; set; }

            [Required(ErrorMessage = "Hours spent is required")]
            public TimeOnly? BreakHours { get; set; }

            [Required(ErrorMessage = "Project selection is required")]
            public int? ProjectId { get; set; }

            public string ProjectName { get; set; }

            [Required(ErrorMessage = "Category is required")]
            public string Category { get; set; }

            public DateTime? SignOutTime { get; set; }

            public DateTime? CreatedDate { get; set; }
        }
        
    }
}
