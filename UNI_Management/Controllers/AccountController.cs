using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UNI_Management.Common.Email;
using UNI_Management.Controllers;
using UNI_Management.Domain.DataModels;
using UNI_Management.Service;
using UNI_Management.Service.Authentication;
using UNI_Management.ViewModel;
using helper = UNI_Management.Common.Utility.CommonHelper;

namespace UNI_Management.Controllers
{
    public class AccountController : BaseController
    {
        #region Constructor
        private readonly ILoginRepository _loginRepository;
        //private readonly IEmployeeRepository _employeeRepository;
        private readonly IJwtTokenRepository _jwtTokenRepository;
        //private readonly INotyfService _notyf;
        private static readonly PasswordHasher<string> hasher = new PasswordHasher<string>();
        public AccountController(ILoginRepository loginRepository, IJwtTokenRepository jwtTokenRepository)
        {
            _loginRepository = loginRepository;
            _jwtTokenRepository = jwtTokenRepository;
            //_notyf = notyfService;
        }
        #endregion

        #region Login Page View
        public IActionResult Index()
        {
            return View();
        }
        #endregion

        [HttpPost]
        public IActionResult UserLogin(User userModel)
        {
            if (ModelState.IsValid)
            {
                Employee? employee = _loginRepository.GetUser(userModel.Email, userModel.Password);
                if (employee != null)
                {
                    var JWTToken = _jwtTokenRepository.GenerateJwtToken(employee.Email, employee.EmployeeId);
                    HttpContext.Session.SetString("JwtToken", JWTToken);
                    HttpContext.Session.SetInt32("UserId", employee.EmployeeId);
                    HttpContext.Session.SetString("Email", employee.Email);
                    HttpContext.Session.SetString("Name", employee.FirstName + " " + employee.LastName);
                    //_notyf.Success("Welcome to Unique IT Solution Employee Portal!");
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["ErrorMessage"] = "Invalid email or password. Please try again.";
                    return RedirectToAction("Index", userModel);
                }
            }
            else
            {
                TempData["ErrorMessage"] = "Please fill in all required fields.";
                return View("Index", userModel);
            }
        }

        #region ChangePassword
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ChangePassword(ChangePasswordViewModel changePasswordViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //var newPasswordHash = hasher.HashPassword(null, changePasswordViewModel.OldPassword);
                    bool checkdetails = _loginRepository.ChangeUserPassword(changePasswordViewModel.Email, changePasswordViewModel.OldPassword, changePasswordViewModel.NewPassword, changePasswordViewModel.ConfirmPassword);
                    if (checkdetails)
                    {
                        var employee = _loginRepository.GetUserByEmail(changePasswordViewModel.Email);
                        var JWTToken = _jwtTokenRepository.GenerateJwtToken(employee.Email, employee.EmployeeId);
                        HttpContext.Session.SetString("JwtToken", JWTToken);
                        HttpContext.Session.SetInt32("UserId", employee.EmployeeId);
                        HttpContext.Session.SetString("Email", employee.Email);
                        HttpContext.Session.SetString("Name", employee.FirstName + " " + employee.LastName);
                        //_notyf.Success("Welcome to Unique IT Solution Employee Portal!");
                        return RedirectToAction("Index", "Home");
                    }
                }
                return View("ChangePassword");
            }
            catch (Exception ex)
            {
                return View("ChangePassword");
            }
        }
        #endregion

        #region ForgotPassword
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ForgetPassword(string email)
        {
            if (email != null)
            {
                var (randomPassword, passwordHash) = helper.GetPassword(8);
                bool isValid = _loginRepository.checkEmail(email, randomPassword, passwordHash);

                if (isValid)
                {
                    string strMailTemplet = GetMailTemplate("login", "forgetpassword.html");
                    strMailTemplet = strMailTemplet.Replace("@username@", email);
                    strMailTemplet = strMailTemplet.Replace("@email@", email);
                    strMailTemplet = strMailTemplet.Replace("@password@", randomPassword);
                    EmailHelper.SendMail(email, "Forget Password", strMailTemplet);
                    HttpContext.Session.SetString("Email", email);
                    return RedirectToAction("Index");
                }
            }
            return View("ForgetPassword");
        }
        #endregion

        public IActionResult TwoFactorAuthentication()
        {
            return View();
        }

        #region Logout
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("JwtToken");
            HttpContext.Session.Remove("UserId");
            HttpContext.Session.Remove("Email");
            return RedirectToAction("Index");
        }
        #endregion
    }
}
