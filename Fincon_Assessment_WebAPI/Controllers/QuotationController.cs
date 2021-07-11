using Fincon_Assessment_WebAPI.DBContext;
using Fincon_Assessment_WebAPI.Models;

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace Fincon_Assessment_WebAPI.Controllers
{
    public class QuotationController : ApiController
    {
        DBcontext _DBcontext { get; set; }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("addQuotation")]
        public HttpResponseMessage addQuotation([FromBody] Quotation Quotation)
        {
            var result = new Response();
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

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("Quotation/{userid}")]
        public HttpResponseMessage viewAllQuotation(string userid)
        {
            var result = new Response();
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

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("viewQuotation/{Id}")]
        public HttpResponseMessage viewQuotation(string ID)
        {
            var result = new Response();
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

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("DeleteQuotation/{Id}/{userid}")]
        public HttpResponseMessage DeleteQuotation(string ID,string userid)
        {
            var result = new Response();
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