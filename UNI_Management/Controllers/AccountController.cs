using Microsoft.AspNetCore.Mvc;

namespace TilesFinders.Controllers
{
    public class AccountController : Controller
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
