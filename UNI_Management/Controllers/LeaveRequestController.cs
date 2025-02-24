using Microsoft.AspNetCore.Mvc;
using UNI_Management.Domain;
using UNI_Management.Helper.Mapper;
using UNI_Management.Helper.Mapper.LeaveRequest;
using UNI_Management.Service;
using UNI_Management.ViewModel;
using UNIManagement.Repositories.Repository.InterFace;
using static QRCoder.PayloadGenerator;

namespace UNI_Management.Controllers
{
    public class LeaveRequestController : Controller
    {
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly IEmployeeRepository _employeeRepository;
        public LeaveRequestController(ILeaveRequestRepository leaveRequestRepository, IEmployeeRepository employeeRepository)
        {
            _leaveRequestRepository = leaveRequestRepository;
            _employeeRepository = employeeRepository;
        }
        public IActionResult LeaveRequestFormPage()
        {
            LeaveRequestViewModel model = new();
            model.leaveRequestDetails.LeaveRequestorID = (int)HttpContext.Session.GetInt32("UserId");
            model.leaveRequestDetails.LeaveRequestorName = HttpContext.Session.GetString("Name");

            return View(model);
        }
        public IActionResult SubmitLeaveRequest(LeaveRequestViewModel model)
        {
            try
            {
                _leaveRequestRepository.SubmitLeave(model.ToModel());
                return RedirectToAction("LeaveRequestListing");
            }
            catch (Exception ex)
            {
                return RedirectToAction("LeaveRequestListing");
            }
        }
        public async Task<IActionResult> LeaveRequestListing()
        {
            int UserId = HttpContext.Session.GetInt32("UserId") ?? -1;
            string UserName = HttpContext.Session.GetString("Name");    
            LeaveRequestViewModel model = new LeaveRequestViewModel();
            if (UserId != null)
            {
                model.leaveRequestList = _leaveRequestRepository.GetLeaveRequestList(UserId).ToModel();
            }
            ViewBag.EmployeeDropdown = await _employeeRepository.GetEmployeeList();
            return View(model);
        }

        public IActionResult DeleteLeaveRequest(int leaveRecordID)
        {
            _leaveRequestRepository.DeleteLeaveRecord(leaveRecordID);
            int UserId = HttpContext.Session.GetInt32("UserId") ?? -1;
            List<LeaveRequestDetails> model = _leaveRequestRepository.GetLeaveRequestList(UserId).ToModel();
            return RedirectToAction("LeaveRequestListing", model);
        }
        public IActionResult EditLeaveRequest(int leaveRequestId)
        {
            LeaveRequestViewModel model = _leaveRequestRepository.GetLeaveRecord(leaveRequestId).ToModel();
            return View("LeaveRequestFormPage", model);
        }

        [HttpPost]
        public IActionResult UpdateLeaveStatus([FromBody] LeaveStatusUpdateModel model)
        {
            bool result = _leaveRequestRepository.UpdateLeaveRequestStatus(model.LeaveID, model.LeaveRequestId, model.leaveStatus);
            if (result)
                return Json(new { success = result });
            return View();
        }

    }
}
