using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using UNI_Management.Domain.DataContext;
using UNI_Management.Service.Authentication;

namespace UNI_Management.Controllers
{
    public class AuthManager : Attribute, IAuthorizationFilter
    {
        private readonly string _role;
        private readonly ApplicationDbContext _context;

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var jwtService = context.HttpContext.RequestServices.GetService<IJwtTokenRepository>();
            //fetch details from session----------------------------------------------------------
            var UserToken = context.HttpContext.Session.GetString("JwtToken");
            var UserId = context.HttpContext.Session.GetInt32("UserId");
            var UserEmail = context.HttpContext.Session.GetString("Email");
            //------------------------------------------------------------------------------------

            if (jwtService == null)
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary(new { Controller = "Account", Action = "Index" }));
                return;
            }
            // Redirect to login if not logged in 
            if (UserToken == null || !jwtService.ValidateToken(UserToken, out JwtSecurityToken jwtToken))
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary(new { Controller = "Account", Action = "Index" }));
                return;
            }


            //var roleClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "Role");
            // Access Denied if Role Not matched
            //if (roleClaim == null)
            //{
            //    context.Result = new RedirectToRouteResult(new RouteValueDictionary(new { Controller = "Guest", Action = "submit_request_page" }));
            //    return;
            //}

            //if (string.IsNullOrWhiteSpace(_role) || roleClaim.Value != _role)
            //{
            //    context.Result = new RedirectToRouteResult(new RouteValueDictionary(new { Controller = "Guest", Action = "AccessDenied" }));
            //}
        }
    }
}
