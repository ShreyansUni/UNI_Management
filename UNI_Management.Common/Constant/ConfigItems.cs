using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using UNI_Management.Common.Email;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNI_Management.Common
{
    public class ConfigItems
    {
        public static string Domain;
        private static IHttpContextAccessor _httpContextAccessor;
        private static IConfiguration Configuration;

        public static void Initialize(IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {

            _httpContextAccessor = httpContextAccessor;
            Domain = _httpContextAccessor.HttpContext?.Request.Host.ToUriComponent(); // Correct assignment
            Configuration = configuration;
        }

        public static String DBConnectionString()
        {
            return ConnectionString;
        }
        #region Default Connection

        public static string ConnectionString => Configuration["Data:DefaultConnection:ConnectionString"];
        public static string StageConnectionString => Configuration["Data:DefaultConnection:Stage_ConnectionString"];
        public static string ProductionConnectionString => Configuration["Data:DefaultConnection:Production_ConnectionString"];

        public static string IpAddress => _httpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString();
        public static string Domainmain => _httpContextAccessor.HttpContext?.Request.Host.ToUriComponent();

        #endregion


        public static string CryptographyKey => Configuration["Data:CryptographyKey"];

        public static string JwtKey => Configuration["Jwt:Key"];
        public static string JwtIssuer => Configuration["Jwt:Issuer"];
        public static string JwtAudience => Configuration["Jwt:Audience"];
        public static int SessionIdleTimeoutInDay => Convert.ToInt32(Configuration["Data:SessionIdleTimeoutInDay"]);


        #region Email Configuration details

        public static bool IsSendEmail => Convert.ToBoolean(Configuration["Data:Email:IsSendEmail"]);
        public static string SMTPHost => Convert.ToString(Configuration["Data:Email:SMTPHost"]);
        public static int SMTPPort => Convert.ToInt32(Configuration["Data:Email:SMTPPort"]);
        public static bool IsSMTPEnableSsl => Convert.ToBoolean(Configuration["Data:Email:IsSMTPEnableSsl"]);
        public static string DisplayEmailSender => Convert.ToString(Configuration["Data:Email:DisplayEmailSender"]);
        public static string EmailSender => Convert.ToString(Configuration["Data:Email:Sender"]);
        public static string EmailPassword => Convert.ToString(Configuration["Data:Email:Password"]);

        #endregion

        #region
        public static bool CacheWithRemoveOtherSite => Convert.ToBoolean(Configuration["Data:CacheWithRemoveOtherSite"]);
        public static bool IsLockCustomCacheMethods => Convert.ToBoolean(Configuration["Data:IsLockCustomCacheMethods"]);
        public static bool IsCacheActive => Convert.ToBoolean(Configuration["Data:IsCacheActive"]);
        public static bool CacheInSleepWhileRequestIsInProgress => Convert.ToBoolean(Configuration["Data:CacheInSleepWhileRequestIsInProgress"]);
        #endregion


        public static string RegisterOTPTemplate => Convert.ToString(Configuration["Data:SmsMessages:RegisterOTPTemplate"]);


        #region Default Variables
        public static int DefaultPageNumber => Convert.ToInt32(Configuration["Data:DefaultPageNumber"]);
        public static int DefaultPageSize => Convert.ToInt32(Configuration["Data:DefaultPageSize"]);
        public static int DefaultMenuId => Convert.ToInt32(Configuration["Data:DefaultMenuId"]);
        public static int DefaultInCorrectPasswordAttempt => Convert.ToInt32(Configuration["Data:DefaultInCorrectPasswordAttempt"]);
        public static int AfterHoursToUnBlock => Convert.ToInt32(Configuration["Data:AfterHoursToUnBlock"]);
        #endregion

        #region JWT

        public static string JwtSubject => Convert.ToString(Configuration["Data:Jwt:Subject"]);
        public static string CRM_LeadmanagerSecretAnonymousKey => Convert.ToString(Configuration["Data:CRM_LeadmanagerSecretAnonymousKey"]);
        public static string CRM_LeadmanagerSecretKey => Convert.ToString(Configuration["Data:CRM_LeadmanagerSecretKey"]);
        #endregion

        #region ApiCalling
        public static string Oauth2TokenUrl => Configuration["Data:ApiUrl:Oauth2TokenUrl"];
        public static string BaseUrlForDomain => Configuration["Data:ApiUrl:BaseUrlForDomain"];
        public static string ClientId => Configuration["Data:ApiUrl:ClientId"];
        public static string ClientSecret => Configuration["Data:ApiUrl:ClientSecret"];
        public static string UserName => Configuration["Data:ApiUrl:UserName"];
        public static string Password => Configuration["Data:ApiUrl:Password"];
        public static string GrantType => Configuration["Data:ApiUrl:GrantType"];

        #endregion

        public static int UserAlreadyExists => -1;
        public static int UserCityId => 1;
        public static int UserStateId => 1;
    }
}
