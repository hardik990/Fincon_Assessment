using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fincon_Assessment.Models
{
    public class ReturnAPI
    {
        public Status status;
        public string message { get; set; }
        public dynamic data { get; set; }
    }
    public enum Status
    {

        OK = 200,
        InternalServerError = 500,
        InvalidParameter = 503,
        BadRequest = 400,
        unauthorised = 401
    }
}