using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;

using Newtonsoft.Json;

namespace Fincon_Assessment.Models
{
    public class Register : IRegister
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ErrorMessage { get; set; }

        public ReturnAPI ValidateRegister(Register _register)
        {
            try
            {
                string URL = string.Empty;
                using (HttpClient client = new HttpClient())
                {
                    URL = ConfigurationManager.AppSettings["BaseAddress"] + "Register";
                    var stringContent = new StringContent(JsonConvert.SerializeObject(_register), Encoding.UTF8, "application/json");
                    using (HttpResponseMessage response = client.PostAsync(URL, stringContent).Result)
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