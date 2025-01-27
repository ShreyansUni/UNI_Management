namespace UNI_Management.Controllers
{
    //[AttributeUsage(AttributeTargets.All)]
    //public class AuthManager : Attribute, IAuthorizationFilter  
    //{
    //public async void OnAuthorization(AuthorizationFilterContext context)
    //{
    //    var jwtService = context.HttpContext.RequestServices.GetService<IJwtTokenRepository>();
    //    //var loginService = context.HttpContext.RequestServices.GetService<IAccountRepository>();

    //    if (jwtService == null)
    //    {
    //        context.Result = new RedirectToRouteResult(new RouteValueDictionary(new { Controller = "Account", action = "Index" }));
    //        return;
    //    }

    //    var request = context.HttpContext.Request;
    //    var token = request.Cookies["AuthValidator"];

    //    if (token == null || !jwtService.ValidateToken(token, out JwtSecurityToken jwtToken))
    //    {
    //        context.Result = new RedirectToRouteResult(new RouteValueDictionary(new { Controller = "Account", action = "Index" }));
    //        return;
    //    }

    //    var roleIdClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "RoleId");
    //    if (roleIdClaim == null)
    //    {
    //        context.Result = new RedirectToRouteResult(new RouteValueDictionary(new { Controller = "Account", action = "Index" }));
    //        return;
    //    }

    //    var roleId = Convert.ToInt32(roleIdClaim.Value);
    //    var path = context.HttpContext.Request.Path;
    //    var menuItems = loginService.GetTeamMemberMenus(roleId);
    //    var Path = context.HttpContext.Request.Path;
    //    var fullPath = context.HttpContext.Request.Path; // This is "/Dashboard/GetLocations"
    //    if (!string.IsNullOrEmpty(fullPath))
    //    {
    //        var parts = fullPath.Value.Split('/');
    //        if (parts.Length > 1)
    //        {
    //            Path = "/" + parts[1]; // Combine the '/' with the first part after splitting
    //                                   // desiredPath will now be "/Dashboard"
    //        }
    //    }
    //    List<MstMenu> staticmenu = loginService.GetTeamMemberMenus((int)Convert.ToInt32(CV.RoleId()));


    //    bool ispathavailable = staticmenu != null && staticmenu.Any(item =>
    //        item.Url != null && item.Url.Equals(Path, StringComparison.OrdinalIgnoreCase));
    //    if ((staticmenu == null || !ispathavailable))
    //    {
    //        context.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "error404", action = "error_404" }));

    //    }
    //}

    //public async void OnAuthorization(AuthorizationFilterContext context)
    //{
    //    var jwtService = context.HttpContext.RequestServices.GetService<IJwtTokenRepository>();
    //    var loginService = context.HttpContext.RequestServices.GetService<IAccountRepository>();
    //    var userService = context.HttpContext.RequestServices.GetService<IUserRepository>();
    //
    //    if (jwtService == null)
    //    {
    //        context.Result = new RedirectToRouteResult(new RouteValueDictionary(new { Controller = "Account", action = "Index" }));
    //        return;
    //    }
    //
    //    var request = context.HttpContext.Request;
    //    var token = request.Cookies["AuthValidator"];
    //
    //    if (token == null || !jwtService.ValidateToken(token, out JwtSecurityToken jwtToken))
    //    {
    //        context.Result = new RedirectToRouteResult(new RouteValueDictionary(new { Controller = "Account", action = "Index" }));
    //        return;
    //    }
    //
    //    var userIdClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "UserId");
    //    if (userIdClaim == null)
    //    {
    //        context.Result = new RedirectToRouteResult(new RouteValueDictionary(new { Controller = "Account", action = "Index" }));
    //        return;
    //    }
    //
    //    var Path = context.HttpContext.Request.Path;
    //    var fullPath = context.HttpContext.Request.Path; // This is "/Dashboard/GetLocations"
    //    if (!string.IsNullOrEmpty(fullPath))
    //    {
    //        var parts = fullPath.Value.Split('/');
    //        if (parts.Length > 1)
    //        {
    //            Path = "/" + parts[1]; // Combine the '/' with the first part after splitting
    //                                   // desiredPath will now be "/Dashboard"
    //        }
    //    }
    //    List<MenuItem> staticmenu = loginService.SetMenuForUser((int)Convert.ToInt32(CV.UserID()));
    //
    //
    //    bool ispathavailable = staticmenu != null && staticmenu.Any(item =>
    //        item.Url != null && item.Url.Equals(Path, StringComparison.OrdinalIgnoreCase));
    //    if ((staticmenu == null || !ispathavailable))
    //    {
    //        context.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "error404", action = "error_404" }));
    //
    //    }
    //
    //    bool flage = false;
    //
    //}
}
