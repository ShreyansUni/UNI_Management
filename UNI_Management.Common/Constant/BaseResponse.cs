using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace UNI_Management.Common.Constant
{
    public class BaseResponse
    {
        // request is successfull or not
        public bool IsSuccessfull { get; set; } = true;

        // request api status code
        public Enums.StatusCode StatusCode { get; set; }

        // request api status code as string
        public string StatusMessage { get; set; } = string.Empty;

        // add any message that regarding request api
        public string Message { get; set; } = string.Empty;

        // data that need to provide on responce of request api
        public object? Data { get; set; } = null;

        // for caching data on front-end side or not
        public object? TotalCount {  get; set; } = null;
    }
    public class UserBaseResponse
    {
        // request is successfull or not
        public bool IsSuccessfull { get; set; } = true;

        // request api status code
        public Enums.StatusCode StatusCode { get; set; }

        // request api status code as string
        public string StatusMessage { get; set; } = string.Empty;

        // add any message that regarding request api
        public string Message { get; set; } = string.Empty;

        // data that need to provide on responce of request api
        public object? Data { get; set; } = null;

        // for caching data on front-end side or not
        public bool? IsUserRegistered{ get; set; } = false;
    }
}
