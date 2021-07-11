using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fincon_Assessment.Models
{
    public class QuotationDetails
    {
        public long Id { get; set; }
        public long QuotationId { get; set; }
        public string Description { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Vat { get; set; }
        public decimal TotalExcludingVat { get; set; }
        public decimal TotalIncludingVat { get; set; }
        public bool isNew { get; set; } = false;
    }
}