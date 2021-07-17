using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fincon_Assessment.Models
{
    public class Quotation
    {
        public Quotation()
        {
            this.lstQuotationDetails = new List<QuotationDetails>();
        }
        public long Id { get; set; }
        public long UserID { get; set; }
        public string date { get; set; }
        public Guid QuotationNumber { get; set; }
        public string CustomerNamer { get; set; }
        public string CustomerAddress { get; set; }
        public QuotationStatus Status { get; set; }
        public int Statusid { get; set; }
        public List<SelectListItem> lstStatus { get; set; }
        public List<QuotationDetails> lstQuotationDetails { get; set; }
        public decimal TotalExcludingVat { get; set; }
        public decimal TotalIncludingVat { get; set; }
        public string ColourIndex { get; set; }
    }
}