using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNI_Management.ViewModel
{
    public class AttandanceViewModal : BaseModelViewModel
    {
        public AttandanceViewModal()
        {
            attadenceDetails = new AttandenceDetails();
            attadenceList = new List<AttandenceDetails>();
        }

        public List<AttandenceDetails> attadenceList { get; set; }

        public AttandenceDetails attadenceDetails { get; set; }

        public class AttandenceDetails
        {
            public int AttendanceId { get; set; }
            public int EmployeeId { get; set; }
            public short? Status { get; set; }
            public int? Day { get; set; }
            public int? Month { get; set; }
            public int? Year { get; set; }
            public DateTime CreatedDate { get; set; }
        }


    }
}
