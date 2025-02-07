using Microsoft.AspNetCore.Mvc;
using UNI_Management.Domain;
using UNI_Management.Helper.Mapper.LeaveRequest;
using UNI_Management.ViewModel;
using UNIManagement.Repositories.Repository.InterFace;
using static QRCoder.PayloadGenerator;

namespace UNI_Management.Controllers
{
    public class LeaveRequestController : Controller
    {
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        public LeaveRequestController(ILeaveRequestRepository leaveRequestRepository)
        {
            _leaveRequestRepository = leaveRequestRepository;
        }
        public IActionResult LeaveRequestFormPage()
        {
            LeaveRequestViewModel model = new();
            model.leaveRequestDetails.LeaveRequestorID = (int)HttpContext.Session.GetInt32("UserId");
            model.leaveRequestDetails.LeaveRequestorName = HttpContext.Session.GetString("Name");

            return View(model);
        }
        public IActionResult SubmitLeaveRequest(LeaveRequestDTO model)
        {
            try
            {
                _leaveRequestRepository.SubmitLeave(model);
                return RedirectToAction("LeaveRequestListing");
            }
            catch (Exception ex)
            {
                return RedirectToAction("LeaveRequestListing");
            }
        }
        public IActionResult LeaveRequestListing()
        {
            int UserId = HttpContext.Session.GetInt32("UserId") ?? -1;
            string UserName = HttpContext.Session.GetString("Name");
            List<LeaveRequestViewModel> model = new();
            if (UserId != null)
            {
                model = _leaveRequestRepository.GetLeaveRequestList(UserId).ToModel();
            }
            return View(model);
        }

        public IActionResult DeleteLeaveRequest(int leaveRecordID)
        {
            _leaveRequestRepository.DeleteLeaveRecord(leaveRecordID);
            int UserId = HttpContext.Session.GetInt32("UserId") ?? -1;
            List<LeaveRequestViewModel> model = _leaveRequestRepository.GetLeaveRequestList(UserId).ToModel();
            return RedirectToAction("LeaveRequestListing", model);
        }
        public IActionResult EditLeaveRequest(int leaveRequestId)
        {
            LeaveRequestDetails model = _leaveRequestRepository.GetLeaveRecord(leaveRequestId).ToModel();
            return View("LeaveRequestFormPage", model);
        }
    }
}
