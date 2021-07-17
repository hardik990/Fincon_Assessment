using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;

using Newtonsoft.Json;

namespace Fincon_Assessment.Models
{
    public class Login : ILogin
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ErrorMessage { get; set; }

        public ReturnAPI ValidateLogin(Login login)
        {
            try
            {
                string URL = string.Empty;
                string Basicauth = System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(ConfigurationManager.AppSettings["ApiUserName"].ToString() + ":" + ConfigurationManager.AppSettings["ApiPassword"].ToString()));
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("Authorization", "Basic " + Basicauth);
                    URL = ConfigurationManager.AppSettings["BaseAddress"] + string.Format("Login/{0}/{1}", login.UserName, login.Password);
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