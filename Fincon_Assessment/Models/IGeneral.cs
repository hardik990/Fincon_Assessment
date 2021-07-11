using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Fincon_Assessment.Models
{
    public interface IGeneral
    {
        List<SelectListItem> GetStatusForDropDown();
        ReturnAPI GetGetQuotation(int QuotationId);
    }
}
