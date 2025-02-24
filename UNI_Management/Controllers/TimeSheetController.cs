using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using UNI_Management.Domain;
using UNI_Management.Helper.Mapper.TimeSheet;
using UNI_Management.Service;
using UNI_Management.Service.TimeSheetRepository;
using UNI_Management.ViewModel;
using static UNI_Management.ViewModel.AttandanceViewModal;
using static UNI_Management.ViewModel.TimeSheetViewModel;
using static UNI_Management.ViewModel.WorkLogViewModal;

namespace UNI_Management.Controllers
{
    public class TimeSheetController : BaseController
    {
        private readonly ITimeSheetRepository _timeSheetRepository;
        private readonly ILogger<TimeSheetController> _logger;
        private readonly IEmployeeRepository _employeeRepository;
        public TimeSheetController(ITimeSheetRepository timeSheetRepository, ILogger<TimeSheetController> logger, IEmployeeRepository employeeRepository)
        {
            _timeSheetRepository = timeSheetRepository;
            _logger = logger;
            _employeeRepository = employeeRepository;
        }

        #region Index
        public async Task<IActionResult> Index()
        {
            try
            {
                int UserId = HttpContext.Session.GetInt32("UserId") ?? -1;
                TimeSheetViewModel timeSheetViewModel = new TimeSheetViewModel
                {
                    timeSheetDetails = new TimeSheetDetails() // Prevent null reference issues
                };

                ViewBag.EmployeeDropdown = await _employeeRepository.GetEmployeeList();
                var timeSheetData = _timeSheetRepository.GetTimeSheetData(UserId);
                timeSheetViewModel.timeSheetDetailsList = timeSheetData.ToModel();

                if (timeSheetViewModel.timeSheetDetailsList.Any())
                {
                    var signInTimes = timeSheetViewModel.timeSheetDetailsList
                        .SelectMany(t => t.AttandenceDetails ?? new List<AttandenceDetails>())
                        .Where(a => a.CreatedDate.HasValue)
                        .Select(a => a.CreatedDate.Value.TimeOfDay)
                        .ToList();

                    var signOutTimes = timeSheetViewModel.timeSheetDetailsList
                        .SelectMany(t => t.WorkLogDetails ?? new List<WorkLogDetails>())
                        .Where(w => w.SignOutTime.HasValue)
                        .Select(w => w.SignOutTime.Value.TimeOfDay)
                        .ToList();

                    var workingDurations = timeSheetViewModel.timeSheetDetailsList
                        .Where(t => (t.AttandenceDetails?.Any(a => a.CreatedDate.HasValue) ?? false) &&
                                    (t.WorkLogDetails?.Any(w => w.SignOutTime.HasValue) ?? false))
                        .Select(t =>
                        {
                            var firstSignIn = t.AttandenceDetails.FirstOrDefault()?.CreatedDate;
                            var firstSignOut = t.WorkLogDetails.FirstOrDefault()?.SignOutTime;

                            return (firstSignOut.HasValue && firstSignIn.HasValue)
                                ? firstSignOut.Value - firstSignIn.Value
                                : (TimeSpan?)null;
                        })
                        .Where(d => d.HasValue)
                        .Select(d => d.Value)
                        .ToList();

                    // Calculate Averages
                    timeSheetViewModel.timeSheetDetails.AvgSignInTime = signInTimes.Any()
                        ? TimeSpan.FromTicks((long)signInTimes.Average(t => t.Ticks))
                        : (TimeSpan?)null;

                    timeSheetViewModel.timeSheetDetails.AvgSignOutTime = signOutTimes.Any()
                        ? TimeSpan.FromTicks((long)signOutTimes.Average(t => t.Ticks))
                        : (TimeSpan?)null;

                    timeSheetViewModel.timeSheetDetails.AvgWorkingTime = workingDurations.Any()
                        ? TimeSpan.FromTicks((long)workingDurations.Average(d => d.Ticks))
                        : (TimeSpan?)null;
                }

                return View("_TimeSheetList", timeSheetViewModel);
            }
            catch (Exception ex)
            {
                // Log error
                _logger.LogError(ex, "Error in fetching time sheet data");
                return StatusCode(500, "Internal server error");
            }
        }
        #endregion


        #region
        //public IActionResult DownloadTimesheet()
        //{
        //    var timesheetData = GetTimesheetData(); // Fetch your data from DB or service

        //    using (var workbook = new XLWorkbook())
        //    {
        //        var worksheet = workbook.Worksheets.Add("Timesheet");

        //        // Define headers
        //        worksheet.Cell(1, 1).Value = "Date";
        //        worksheet.Cell(1, 2).Value = "P/A";
        //        worksheet.Cell(1, 3).Value = "SignInTime";
        //        worksheet.Cell(1, 4).Value = "SignOutTime";
        //        worksheet.Cell(1, 5).Value = "WorkingHours";
        //        worksheet.Cell(1, 6).Value = "WorklogNotes";

        //        int row = 2;
        //        foreach (var entry in timesheetData)
        //        {
        //            worksheet.Cell(row, 1).Value = entry.Date.ToString("yyyy-MM-dd");
        //            worksheet.Cell(row, 2).Value = entry.Present ? "Present" : "Absent";
        //            worksheet.Cell(row, 3).Value = entry.SignInTime.ToString("HH:mm");
        //            worksheet.Cell(row, 4).Value = entry.SignOutTime.ToString("HH:mm");
        //            worksheet.Cell(row, 5).Value = entry.WorkingHours;
        //            worksheet.Cell(row, 6).Value = entry.WorklogNotes;
        //            row++;
        //        }

        //        using (var stream = new MemoryStream())
        //        {
        //            workbook.SaveAs(stream);
        //            var content = stream.ToArray();
        //            return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Timesheet.xlsx");
        //        }
        //    }
        //}
        #endregion
    }
}
