using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fincon_Assessment.Models
{
    public interface IDashBoard
    {
        List<Quotation> GetQuotation();
        ReturnAPI DeleteQuotation(int Id);
    }
}
