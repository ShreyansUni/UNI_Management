namespace UNI_Management.ViewModel
{
    public class CV
    {
        private static IHttpContextAccessor _contextAccessor;

        static CV()
        {
            _contextAccessor = new HttpContextAccessor();
        }

        public static string? TeamMemberId()
        {
            string cookieValue;
            string TeamMemberId = null;

            if (_contextAccessor.HttpContext.Request.Cookies["AuthValidator"] != null)
            {
                cookieValue = _contextAccessor.HttpContext.Request.Cookies["AuthValidator"].ToString();

                TeamMemberId = DecodeToken.DecodeJwt(DecodeToken.ConvertJwtStringToJwtSecurityToken(cookieValue)).claims.FirstOrDefault(t => t.Key == "TeamMemberId").Value;
            }

            return TeamMemberId;
        }

        public static string? RoleId()
        {
            string cookieValue;
            string RoleId = null;

            if (_contextAccessor.HttpContext.Request.Cookies["AuthValidator"] != null)
            {
                cookieValue = _contextAccessor.HttpContext.Request.Cookies["AuthValidator"].ToString();

                RoleId = DecodeToken.DecodeJwt(DecodeToken.ConvertJwtStringToJwtSecurityToken(cookieValue)).claims.FirstOrDefault(t => t.Key == "RoleId").Value;
            }

            return RoleId;
        }
        public static string? RoleName()
        {
            string cookieValue;
            string RoleName = null;

            if (_contextAccessor.HttpContext.Request.Cookies["AuthValidator"] != null)
            {
                cookieValue = _contextAccessor.HttpContext.Request.Cookies["AuthValidator"].ToString();

                RoleName = DecodeToken.DecodeJwt(DecodeToken.ConvertJwtStringToJwtSecurityToken(cookieValue)).claims.FirstOrDefault(t => t.Key == "RoleName").Value;
            }

            return RoleName;
        }
        public static string? Email()
        {
            string cookieValue;
            string UserID = null;

            if (_contextAccessor.HttpContext.Request.Cookies["AuthValidator"] != null)
            {
                cookieValue = _contextAccessor.HttpContext.Request.Cookies["AuthValidator"].ToString();

                UserID = DecodeToken.DecodeJwt(DecodeToken.ConvertJwtStringToJwtSecurityToken(cookieValue)).claims.FirstOrDefault(t => t.Key == "Email").Value;
            }

            return UserID;
        }

        public static string? EmailBySession()
        {
            string? userEmail = _contextAccessor.HttpContext.Session.GetString("Email");
            return userEmail;
        }

        public static string? Username()
        {
            string cookieValue;
            string UserID = null;

            if (_contextAccessor.HttpContext.Request.Cookies["AuthValidator"] != null)
            {
                cookieValue = _contextAccessor.HttpContext.Request.Cookies["AuthValidator"].ToString();

                UserID = DecodeToken.DecodeJwt(DecodeToken.ConvertJwtStringToJwtSecurityToken(cookieValue)).claims.FirstOrDefault(t => t.Key == "Username").Value;
            }

            return UserID;
        }

        public static string? AspNetUserId()
        {
            string cookieValue;
            string UserID = null;

            if (_contextAccessor.HttpContext.Request.Cookies["AuthValidator"] != null)
            {
                cookieValue = _contextAccessor.HttpContext.Request.Cookies["AuthValidator"].ToString();

                UserID = DecodeToken.DecodeJwt(DecodeToken.ConvertJwtStringToJwtSecurityToken(cookieValue)).claims.FirstOrDefault(t => t.Key == "AspNetUserId").Value;
            }

            return UserID;
        }

        public static string? AgencyId()
        {
            string cookieValue;
            string UserID = null;

            if (_contextAccessor.HttpContext.Request.Cookies["AuthValidator"] != null)
            {
                cookieValue = _contextAccessor.HttpContext.Request.Cookies["AuthValidator"].ToString();

                UserID = DecodeToken.DecodeJwt(DecodeToken.ConvertJwtStringToJwtSecurityToken(cookieValue)).claims.FirstOrDefault(t => t.Key == "AgencyId").Value;
            }

            return UserID;
        }
        public static string? UserType()
        {
            string cookieValue;
            string UserID = null;

            if (_contextAccessor.HttpContext.Request.Cookies["AuthValidator"] != null)
            {
                cookieValue = _contextAccessor.HttpContext.Request.Cookies["AuthValidator"].ToString();

                UserID = DecodeToken.DecodeJwt(DecodeToken.ConvertJwtStringToJwtSecurityToken(cookieValue)).claims.FirstOrDefault(t => t.Key == "UserType").Value;
            }

            return UserID;
        }
    }
}
