using Microsoft.AspNetCore.Mvc;
using UNI_Management.Helper.Mapper.TimeSheet;
using UNI_Management.Service;
using UNI_Management.Service.TimeSheetRepository;
using UNI_Management.ViewModel;

namespace UNI_Management.Controllers
{
    public class TimeSheetController : BaseController
    {
        private readonly ITimeSheetRepository _timeSheetRepository;
        private readonly ILogger<TimeSheetController> _logger;
        public TimeSheetController(ITimeSheetRepository timeSheetRepository, ILogger<TimeSheetController> logger)
        {
            _timeSheetRepository = timeSheetRepository;
            _logger = logger;
        }
        #region Index
        public IActionResult Index()
        {
            try
            {
                int UserId = HttpContext.Session.GetInt32("UserId") ?? -1;
                TimeSheetViewModel timeSheetViewModel = new TimeSheetViewModel();
                timeSheetViewModel.timeSheetDetailsList = _timeSheetRepository.GetTimeSheetData(UserId).ToModel();

                return View("_TimeSheetList", timeSheetViewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception in InventoryOpenRequestPagination");
                return RedirectToRoute("Error_404");
            }
        }
        #endregion
    }
}
