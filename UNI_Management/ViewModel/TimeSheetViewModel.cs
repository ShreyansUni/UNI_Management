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
        public class TimeSheetDetails
        {
            public List<WorkLogDetails> WorkLogDetails { get; set; }
            public List<AttandenceDetails> AttandenceDetails { get; set; }
        }
    }
}
