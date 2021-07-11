using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
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
                using (HttpClient client = new HttpClient())
                {
                    URL = ConfigurationManager.AppSettings["BaseAddress"] + string.Format("Login/{0}/{1}", login.UserName, login.Password);
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