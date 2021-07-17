using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;

using Fincon_Assessment.Common;

using Newtonsoft.Json;

namespace Fincon_Assessment.Models
{
    public class DashBoard : IDashBoard
    {
        public string UserName { get; set; }
        public string message { get; set; }
        public List<Quotation> lstData { get; set; }
        public List<Quotation> GetQuotation()
        {
            try
            {
                string URL = string.Empty;
                string Basicauth = System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(ConfigurationManager.AppSettings["ApiUserName"].ToString() + ":" + ConfigurationManager.AppSettings["ApiPassword"].ToString()));
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("Authorization", "Basic " + Basicauth);
                    URL = ConfigurationManager.AppSettings["BaseAddress"] + string.Format("Quotation/{0}", MySession.Current.UserId);
                    using (HttpResponseMessage response = client.GetAsync(URL).Result)
                    {
                        if (response.StatusCode != HttpStatusCode.Unauthorized)
                        {
                            ReturnAPI objdata = JsonConvert.DeserializeObject<ReturnAPI>(response.Content.ReadAsStringAsync().Result);
                            if (objdata.status == Status.OK)
                            {

                                List<Quotation> lstQuotation = JsonConvert.DeserializeObject<List<Quotation>>(objdata.data.ToString());
                                return lstQuotation;
                            }
                        }
                        else
                        {
                            return new List<Quotation>();
                        }

                    }
                }
                return new List<Quotation>();
            }
            catch (Exception Ex)
            {
                //Error Log
                throw Ex;
            }
        }
        public ReturnAPI DeleteQuotation(int Id)
        {
            try
            {
                string URL = string.Empty;
                string Basicauth = System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(ConfigurationManager.AppSettings["ApiUserName"].ToString() + ":" + ConfigurationManager.AppSettings["ApiPassword"].ToString()));
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("Authorization", "Basic " + Basicauth);
                    URL = ConfigurationManager.AppSettings["BaseAddress"] + string.Format("DeleteQuotation/{0}/{1}", Id, MySession.Current.UserId.ToString());
                    using (HttpResponseMessage response = client.GetAsync(URL).Result)
                    {
                        if (response.StatusCode != HttpStatusCode.Unauthorized)
                        {
                            return JsonConvert.DeserializeObject<ReturnAPI>(response.Content.ReadAsStringAsync().Result);
                        }
                        else
                        {
                            ReturnAPI returnAPI = new ReturnAPI();
                            returnAPI.data = null;
                            returnAPI.status = Status.unauthorised;
                            returnAPI.message = "Request Unauthorised";
                            return returnAPI;
                            //return JsonConvert.DeserializeObject<ReturnAPI>(response.Content.ReadAsStringAsync().Result);
                        }
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