using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UNI_Management.Domain;

namespace UNI_Management.Service.TimeSheetRepository
{
    public interface ITimeSheetRepository
    {
        public List<TimeSheetDTO> GetTimeSheetData(int UserId);
    }
}
