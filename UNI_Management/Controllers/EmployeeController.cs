using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UNI_Management.Controllers;
using UNI_Management.ViewModel;
using UNI_Management.Service;
using UNI_Management.Helper.Mapper;
using UNI_Management.Common;
using UNI_Management.Common.Utility;
using UNI_Management.Helper;
using static UNI_Management.ViewModel.EmployeeViewModel;
using UNI_Management.Domain;
using static QRCoder.PayloadGenerator;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace UNI_Management.Controllers
{
    [AuthManager]
    public class EmployeeController : BaseController
    {
        #region Constructor
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ILogger<EmployeeController> _logger;
        public EmployeeController(IEmployeeRepository employeeRepository, ILogger<EmployeeController> logger)
        {
            _employeeRepository = employeeRepository;
            _logger = logger;
        }
        #endregion

        #region List
        public IActionResult Index()
        {
            try
            {
                EmployeeViewModel model = new EmployeeViewModel();
                model.PageIndex = ConfigItems.DefaultPageNumber;
                model.PageSize = ConfigItems.DefaultPageSize;
                model.employeeList = _employeeRepository.GetEmployeeList(null, null, null, model.PageSize, model.PageIndex, string.Empty, string.Empty).ToModel();
                ViewBag.EmpTypeDropDown = new List<SelectListItem>
                                            {
                                                new SelectListItem { Value = "BDA", Text = "BDA" },
                                                new SelectListItem { Value = "Technical", Text = "Technical" }
                                            };
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception in InventoryOpenRequestPagination");
                return RedirectToRoute("Error_404");
            }
        }
        #endregion

        [HttpGet, Route("employee/search", Name = "EmployeeSerachFilter")]
        public IActionResult EmployeePagination(int pageIndex, int pageSize, string filterObj, string columnName, string sortDirection)
        {
            try
            {
                var model = new EmployeeViewModel();
                model.PageSize = pageSize;
                model.PageIndex = pageIndex;

                var filterFirstName = string.Empty;
                var filterJoiningDate = string.Empty;
                var filterEmployeeType = string.Empty;

                if (!string.IsNullOrEmpty(filterObj))
                {
                    filterFirstName = CommonHelper.GetFilterPropertyValue(filterObj, "txtFirstName").Trim();
                    filterEmployeeType = CommonHelper.GetFilterPropertyValue(filterObj, "EmployeeTypefilter").Trim();
                    filterJoiningDate = CommonHelper.GetFilterPropertyValue(filterObj, "txtJoiningDate").Trim();
                }
                DateTime? parsedDate = null;

                if (!string.IsNullOrEmpty(filterJoiningDate))
                {
                    if (DateTime.TryParse(filterJoiningDate, out DateTime tempDate))
                    {
                        parsedDate = tempDate;
                    }
                }

                //bool? isActive = null;
                //if (!string.IsNullOrEmpty(filterIsActive))
                //{
                //    if (bool.TryParse(filterIsActive, out bool result))
                //    {
                //        isActive = result;
                //    }
                //}

                model.employeeList = _employeeRepository.GetEmployeeList(filterFirstName, parsedDate, filterEmployeeType, pageSize, pageIndex, columnName, sortDirection).ToModel();
                return PartialView("_EmployeeGrid", model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception in DomainPagination");
                return RedirectToRoute("Error_404");
            }
        }

        [HttpGet, Route("employee/view", Name = "EmployeeViewModal")]
        public IActionResult Employeeviewmodal(string encodeEmployeeId)
        {
            try
            {
                EmployeeDetails employee = new EmployeeDetails();

                if (encodeEmployeeId != "0")
                {
                    int? ID = encodeEmployeeId.Decode();
                    employee = _employeeRepository.GetEmployeenData((int)ID).ToModel();
                }


                return PartialView("_EmployeeView", employee);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception in InventoryRequestOpenRequest");
                return Json(JsonResultData.SetJsonModel(Enums.StatusCode.BadRequest.GetHashCode(), "Something went wrong. Please try again!"));
            }
        }

        #region AddEditForm
        [HttpGet, Route("Employee/EmployeeEdit/{encodeEmployeeId}", Name = "EmployeeEdit")]
        [HttpGet, Route("/employee/employeeadd", Name = "EmployeeAdd")]
        public IActionResult EmployeeForm(string encodeEmployeeId)
        {
            try
            {
                EmployeeViewModel employee = new EmployeeViewModel();
                if (string.IsNullOrEmpty(encodeEmployeeId))
                {
                    employee.employeeDetails = new EmployeeViewModel.EmployeeDetails();
                }
                else
                {
                    int? Id = encodeEmployeeId.Decode();
                    employee.employeeDetails = _employeeRepository.GetEmployeenData((int)Id).ToModel();
                }
                return View("EmployeeForm", employee);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception in InventoryRequestOpenRequest");
                return Json(JsonResultData.SetJsonModel(Enums.StatusCode.BadRequest.GetHashCode(), "Something went wrong. Please try again!"));
            }
        }
        #endregion

        [HttpPost]
        public async Task<IActionResult> EmployeePost(EmployeeViewModel model)
        {
            try
            {
                if (model == null)
                {
                    return BadRequest("Invalid Employee Data");
                }
                bool result = _employeeRepository.EmployeeAddEdit(model.ToModel());
                if (!result)
                {
                    AddSweetAlertErrorPopup(ConstantMessage.EmployeeAddOrEditUnSuccess);
                    return View("EmployeeForm");
                }
                AddSweetAlertSuccessPopup(ConstantMessage.EmployeeAdded);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        #region Employee Delete
        [HttpPost, Route("employee/delete", Name = "EmployeeDelete")]
        public async Task<IActionResult> EmployeeDelete(string encodeEmployeeId)
        {
            try
            {
                if (!string.IsNullOrEmpty(encodeEmployeeId))
                {
                    int? EmployeeId = encodeEmployeeId.Decode();
                    var (isDeleteSuccess, message) = await _employeeRepository.EmployeeDeleteAsync(EmployeeId);
                    if (!isDeleteSuccess)
                    {
                        AddSweetAlertWarningPopup(message);
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        return Json(new { success = true });
                    }
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                AddSweetAlertWarningPopup("Failed to delete employee.");
                return RedirectToAction("Index");
            }
        }
        #endregion



    }
}
