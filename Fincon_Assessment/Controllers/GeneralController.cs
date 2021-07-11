using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Fincon_Assessment.Common;
using Fincon_Assessment.Models;

using Newtonsoft.Json;

namespace Fincon_Assessment.Controllers
{
    public class GeneralController : Controller
    {
        private readonly IGeneral objIGeneral = null;
        public GeneralController(IGeneral objiGeneral)
        {
            this.objIGeneral = objiGeneral;
        }

        public ActionResult Index(string ID)
        {
            if (ID == null)
            {
                return this.RedirectToAction("Login", "Login");
            }
            if (Session["IsEdit"] != null && Session["IsEdit"].ToString() == "1")
            {
                Session["ActiveGrid"] = "General";
            }
            else
            {
                Session["ActiveGrid"] = "General (View)";
            }
            Functions.UpdateActiveID(ID);

            Quotation _Quotation = new Quotation();
            if (ID != "0")
            {
                _Quotation = GetQuotation(int.Parse(ID));
                QuotationDetails objblank = new QuotationDetails();
                objblank.isNew = true;
                _Quotation.lstQuotationDetails.Add(objblank);
                switch (_Quotation.Status)
                {
                    case QuotationStatus.Accepted:
                        _Quotation.Statusid = 1;
                        break;
                    case QuotationStatus.Declined:
                        _Quotation.Statusid = 2;
                        break;
                    case QuotationStatus.Pending:
                        _Quotation.Statusid = 0;
                        break;
                }
            }
            else
            {
                _Quotation.lstQuotationDetails = new List<QuotationDetails>();
                QuotationDetails objblank = new QuotationDetails();
                _Quotation.lstQuotationDetails.Add(objblank);
            }

            this.BindDropDownListForStatus(_Quotation);
            return View(_Quotation);
        }

        [HttpPost]
        public ActionResult Index(Quotation _Quotation)
        {

            return View(_Quotation);
        }
        public void BindDropDownListForStatus(Quotation objQuotation)
        {
            objQuotation.lstStatus = this.objIGeneral.GetStatusForDropDown().ToList();
        }
        public Quotation GetQuotation(int QuotationId)
        {
            ReturnAPI objdata = this.objIGeneral.GetGetQuotation(QuotationId);
            if (objdata.status == Status.OK)
            {
                return JsonConvert.DeserializeObject<Quotation>(objdata.data.ToString());
            }
            else
            {
                return new Quotation();
            }

        }
    }
}