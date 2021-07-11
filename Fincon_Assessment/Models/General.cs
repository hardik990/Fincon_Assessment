using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

using Newtonsoft.Json;

namespace Fincon_Assessment.Models
{
    public class General : IGeneral
    {
        public List<SelectListItem> GetStatusForDropDown()
        {
            List<SelectListItem> lstClient = new List<SelectListItem>();
            lstClient.Add(new SelectListItem { Text = "Pending", Value = "0" });
            lstClient.Add(new SelectListItem { Text = "Accepted", Value = "1" });
            lstClient.Add(new SelectListItem { Text = "Declined", Value = "2" });
            return lstClient;
        }
        public ReturnAPI GetGetQuotation(int QuotationId)
        {
            try
            {
                string URL = string.Empty;
                using (HttpClient client = new HttpClient())
                {
                    URL = ConfigurationManager.AppSettings["BaseAddress"] + string.Format("viewQuotation/{0}", QuotationId);
                    using (HttpResponseMessage response = client.GetAsync(URL).Result)
                    {
                        return JsonConvert.DeserializeObject<ReturnAPI>(response.Content.ReadAsStringAsync().Result);
                    }
                }
            }
            catch (Exception Ex)
            {
                //Error Log
                throw Ex;
            }
        }
    }
}