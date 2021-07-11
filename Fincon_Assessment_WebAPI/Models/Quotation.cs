using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fincon_Assessment_WebAPI.Models
{
    public class Quotation
    {
        public long Id { get; set; }
        public long UserID { get; set; }
        public DateTime date { get; set; }
        public Guid QuotationNumber { get; set; }
        public string CustomerNamer { get; set; }
        public string CustomerAddress { get; set; }
        public long Status { get; set; }
        public string ColourIndex { get; set; }
        public List<QuotationDetails> lstQuotationDetails { get; set; }
        public decimal TotalExcludingVat()
        {
            return lstQuotationDetails.Sum(x => x.TotalExcludingVat());
        }
        public decimal TotalIncludingVat()
        {
            return lstQuotationDetails.Sum(x => x.TotalIncludingVat());
        }
    }

    public class QuotationDetails
    {
        public long Id { get; set; }
        public long QuotationId { get; set; }
        public string Description { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Vat { get; set; }
        public decimal TotalExcludingVat()
        {
            return Quantity * Price;
        }
        public decimal TotalIncludingVat()
        {
            return (TotalExcludingVat() * 100) / Vat;
        }
    }

   
}