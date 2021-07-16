using Fincon_Assessment_WebAPI.DBContext;
using Fincon_Assessment_WebAPI.MessageHandlers;
using Fincon_Assessment_WebAPI.Models;

using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Fincon_Assessment_WebAPI.Controllers
{
    [RoutePrefix("api")]
    public class LoginController : ApiController
    {
        DBcontext _DBcontext { get; set; }
        // GET: Login
        [BasicAuthentication]
        [HttpGet]
        [Route("Login/{email}/{paswword}")]
        public HttpResponseMessage Login(string email, string paswword)
        {
            var result = new Response();
            if (!ModelState.IsValid)
            {
                result.status = Status.BadRequest;
                result.message = "ModelState is Not valid.";
                return Request.CreateResponse(HttpStatusCode.BadRequest, result);
            }

            try
            {
                if (_DBcontext == null)
                    _DBcontext = new DBcontext();
                KeyValuePair<bool, dynamic> isValid = _DBcontext.Login(email, paswword);
                if (isValid.Key)
                {
                    result.data = isValid.Value;
                    result.status = Status.OK;
                    result.message = string.Empty;
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                else
                {
                    result.status = Status.InternalServerError;
                    result.message = isValid.Value.ToString();
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, result);
                }
            }
            catch (Exception Ex)
            {
                result.status = Status.BadRequest;
                result.message = Ex.ToString();
                return Request.CreateResponse(HttpStatusCode.BadRequest, result);
            }

        }
    }
}