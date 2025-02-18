using Microsoft.AspNetCore.Mvc;
using UNI_Management.Helper.Mapper;
using UNI_Management.Helper.Mapper.WorkLog;
using UNI_Management.Service;
using UNI_Management.ViewModel;

namespace UNI_Management.Controllers
{
    public class WorkLogController : BaseController
    {
        #region Constructor
        private readonly IWorkLogRepository _worklogRepository;
        public WorkLogController(IWorkLogRepository worklogRepository)
        {
            _worklogRepository = worklogRepository;
        }
        #endregion


        public IActionResult Index()
        {
            int UserId = HttpContext.Session.GetInt32("UserId") ?? -1;
            string UserName = HttpContext.Session.GetString("Name");
            WorkLogViewModal wl = new WorkLogViewModal();
            if(UserId != null)
                wl.workLogList = _worklogRepository.WorkLogList(UserId).ToModel();
            return View(wl);
        }
        [HttpGet, Route("worklog/edit/{worklogid}", Name = "UserAddEditModal")]
        [HttpGet, Route("worklog/add/", Name = "worklogAdd")]
        public IActionResult AddWorkLog(int WorkLogId)
        {
            WorkLogViewModal workLogViewModal = new WorkLogViewModal();
            if (WorkLogId != null && WorkLogId != 0)
            {
                workLogViewModal.workLogDetails = _worklogRepository.GetWorkLogDetails(WorkLogId).ToModel();
            }
            return View(workLogViewModal);
        }
        public async Task<IActionResult> AddEdit(WorkLogViewModal model)
        {
            int UserId = HttpContext.Session.GetInt32("UserId") ?? -1;
            if (UserId != -1) { 
                model.workLogDetails.EmployeeId = UserId;
            }
            else{
                return RedirectToAction("Index");
            }
            var (isSuccess, Message) = await _worklogRepository.WorkLogAdd(model.ToModel());
            if (isSuccess) {
                AddSweetAlertSuccessPopup(Message);
            }
            else{
                AddSweetAlertErrorPopup(Message);
            }
            return RedirectToAction("Index");
        }
    }
}