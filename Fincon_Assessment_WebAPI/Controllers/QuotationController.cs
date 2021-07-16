using Fincon_Assessment_WebAPI.DBContext;
using Fincon_Assessment_WebAPI.MessageHandlers;
using Fincon_Assessment_WebAPI.Models;

using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace Fincon_Assessment_WebAPI.Controllers
{
    //[Authorize]
    [RoutePrefix("api")]
    public class QuotationController : ApiController
    {
        DBcontext _DBcontext { get; set; }

        [BasicAuthentication]
        [HttpPost]
        [Route("addQuotation")]
        public HttpResponseMessage addQuotation([FromBody] Quotation Quotation)
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
                KeyValuePair<bool, string> isValid = _DBcontext.AddQuotation(Quotation);
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

        [BasicAuthentication]
        [HttpGet]
        [Route("Quotation/{userid}")]
        public HttpResponseMessage viewAllQuotation(string userid)
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
                KeyValuePair<bool, dynamic> isValid = _DBcontext.GetQuotation(userid);
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

        [BasicAuthentication]
        [HttpGet]
        [Route("viewQuotation/{Id}")]
        public HttpResponseMessage viewQuotation(string ID)
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
                KeyValuePair<bool, dynamic> isValid = _DBcontext.GetQuotationId(ID);
                if (isValid.Key)
                {
                    result.data = JsonConvert.SerializeObject(isValid.Value);
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

        [BasicAuthentication]
        [HttpGet]
        [Route("DeleteQuotation/{Id}/{userid}")]
        public HttpResponseMessage DeleteQuotation(string ID, string userid)
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
                KeyValuePair<bool, dynamic> isValid = _DBcontext.DeleteQuotation(ID, userid);
                if (isValid.Key)
                {
                    result.data = JsonConvert.SerializeObject(isValid.Value);
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