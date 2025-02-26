using static UNI_Management.ViewModel.AttandanceViewModal;
using static UNI_Management.ViewModel.WorkLogViewModal;

namespace UNI_Management.ViewModel
{
    public class TimeSheetViewModel : BaseModelViewModel
    {
        public TimeSheetViewModel() {
            timeSheetDetailsList = new List<TimeSheetDetails>();
        }
        public List<TimeSheetDetails> timeSheetDetailsList { get; set; }
        public TimeSheetDetails timeSheetDetails { get; set; }
        public int EmployeeId { get; set; }
        public class TimeSheetDetails
        {
            public List<WorkLogDetails> WorkLogDetails { get; set; }
            public List<AttandenceDetails> AttandenceDetails { get; set; }

            public TimeSpan? AvgSignInTime { get; set; } = TimeSpan.Zero;
            public TimeSpan? AvgSignOutTime { get; set; } = TimeSpan.Zero;
            public TimeSpan? AvgWorkingTime { get; set; } = TimeSpan.Zero;
            public int EmployeeId { get; set; }
            public int TotalRecords { get; set; }
        }
    }
}
