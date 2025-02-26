﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using UNI_Management.Helper.Mapper.Attandence;
using UNI_Management.Service;
using UNI_Management.ViewModel;

namespace UNI_Management.Controllers
{
    [AuthManager]
    public class AttandanceController : BaseController
    {
        #region Constructor
        private readonly IAttandanceRepository _attandanceRepository;
        private readonly ILogger<EmployeeController> _logger;
        private readonly IEmployeeRepository _employeeRepository;
        public AttandanceController(IAttandanceRepository attandanceRepository, ILogger<EmployeeController> logger, IEmployeeRepository employeeRepository)
        {
            _attandanceRepository = attandanceRepository;
            _logger = logger;
            _employeeRepository = employeeRepository;
        }
        #endregion

        #region GetAttandace
        public IActionResult GetAttandaceForMonth(int year, int month,int EmployeeId)
        {
            int UserId = HttpContext.Session.GetInt32("UserId") ?? -1;
            int selectedEmployeeId = EmployeeId > 0 ? EmployeeId : UserId;
            var v =  _attandanceRepository.GetAttandace((int)year, (int)month, selectedEmployeeId).ToModel();
            return Json(v);
        }
        #endregion

        #region Add Attendance
        public async Task<IActionResult> Index()
       {
            AttandanceViewModal attandanceViewModal = new AttandanceViewModal();
            ViewBag.EmployeeDropdown = await _employeeRepository.GetEmployeeList();
            return View(attandanceViewModal);
        }
        public IActionResult SaveAttendance(int day, int month, int year, short status)
        {
            int UserId = HttpContext.Session.GetInt32("UserId") ?? -1;
            _attandanceRepository.AddAttandance(day, month, year, status, UserId);
            return Ok(new { message = "Attendance saved successfully" });
        }
        #endregion
    }
}
