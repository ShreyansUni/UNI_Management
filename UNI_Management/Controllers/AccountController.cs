using Microsoft.AspNetCore.Mvc;
using UNI_Management.Controllers;

namespace TilesFinders.Controllers
{
    public class AccountController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
        
        public IActionResult ChangePassword()
        {
            return View();
        }
        
        public IActionResult ForgotPassword()
        {
            return View();
        }
        
        public IActionResult TwoFactorAuthentication()
        {
            return View();
        }
    }
}
