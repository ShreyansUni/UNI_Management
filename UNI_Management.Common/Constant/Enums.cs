using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace UNI_Management.Common
{
    public class MenuDetails
    {
        public string Url { get; set; }
        public string DetailUrl { get; set; }
        public string BreadcrumbParent { get; set; }
        public string BreadcrumbTitle { get; set; }
        public string BreadcrumbChild { get; set; }
        public bool IsClient { get; set; }
        public bool IsReject { get; set; }
    }

    public static class EnumExtensions
    {
        public static string GetEnumDescription(this Enum enumValue)
        {
            FieldInfo? fi = enumValue.GetType().GetField(enumValue.ToString());

            if (fi == null)
                return enumValue.ToString();

            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(
                typeof(DescriptionAttribute),
                false);

            if (attributes != null &&
                attributes.Length > 0)
                return attributes[0].Description;
            else
                return enumValue.ToString();
        }
    }

    public class Enums
    {
        public static string GetEnumDescription(Enum value)
        {
            try
            {
                if (value.GetHashCode() == 0)
                    return "";

                FieldInfo fi = value.GetType().GetField(value.ToString());

                DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (attributes != null && attributes.Length > 0)
                    return attributes[0].Description;
                else
                    return value.ToString();
            }
            catch (Exception)
            {
                return "";
            }
        }

        public static string GetEnumDescription<TEnum>(int value)
        {
            return GetEnumDescription((Enum)(object)((TEnum)(object)value));
        }

        public enum Gender
        {
            Female,
            Male
        }
        public enum UserType
        {
            Admin = 1,
            Employee = 2,
        }
        public enum LeaveType
        {
            FullLeave = 1,
            FirstHalf = 2,
            SecondHalf = 3
        }

        public enum CheckMail
        {
            [Description("User not registered kindly sign-up.")]
            NotRegistered,
            [Description("User already exists kindly sign-in directly.")]
            AlreadyRegistered
        }
        public enum EmailSmsTemplate
        {
            Login = 1,
            Registartion = 2,
            ForgotPassword = 3,
            RedeemAmountOtp = 4,
            AdminCreateNewShopKeeper = 5,
            PurchasedCRM_LeadmanagerReceiver = 6,
            PurchasedCRM_LeadmanagerSender = 7,
            SupportPhysicalCardPurchase = 8,
            SupportCRM_LeadmanagerCheckBalance = 9,
            SupportPhysicalCardLessInventory = 10,
            PartnerCreateVirtualCard = 11,
            VirtualCreditCardRequest = 12,
            SupportCRM_LeadmanagerPurchase = 13,
            SupportActivateCRM_Leadmanager = 14,
            ApiRegistration = 15,
            ApiTokenChange = 16
        }

        public enum DetailsType
        {
            News=1,
            Property=2,
            Horoscope=3
        }

        public enum StatusCode
        {
            Ok = 200,
            BadRequest = 400,
            NotFound = 404, // also use for data not found
            ServerError = 500,
            AccessDenied = 403,
            NotAllowed = 405,
            Conflict = 409,
            Unauthorized = 401
        }

        public static string GetStatusCodeString(Enums.StatusCode code)
        {
            if (code == Enums.StatusCode.Ok)
                return "Ok";
            else if (code == Enums.StatusCode.BadRequest)
                return "Bad Request";
            else if (code == Enums.StatusCode.NotFound)
                return "Not Found";
            else if (code == Enums.StatusCode.ServerError)
                return "Server Error";
            else if (code == Enums.StatusCode.AccessDenied)
                return "Access Denied";
            else if (code == Enums.StatusCode.NotAllowed)
                return "Not Allowed";
            else if (code == Enums.StatusCode.Conflict)
                return "Conflict";
            else if (code == Enums.StatusCode.Unauthorized)
                return "Token Expired";

            return "";
        }
        public enum PasswordChanges
        {
            forgotpasswordafterupdate = 1,
            changepasswordafterupdate = 2
        }
        public enum GridPageSize
        {
            [Description("10")]
            Ten = 10,
            [Description("20")]
            Twenty = 20,
            [Description("30")]
            Thirty = 30,
            [Description("50")]
            Fifty = 50,
            [Description("100")]
            Hundred = 100
        }

        public enum MediaType
        {
            Image = 1,
            Video = 2
        }
    }
}
