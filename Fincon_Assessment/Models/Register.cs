using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
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
                string Basicauth = System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(ConfigurationManager.AppSettings["ApiUserName"].ToString() + ":" + ConfigurationManager.AppSettings["ApiPassword"].ToString()));
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("Authorization", "Basic " + Basicauth);
                    URL = ConfigurationManager.AppSettings["BaseAddress"] + "Register";
                    var stringContent = new StringContent(JsonConvert.SerializeObject(_register), Encoding.UTF8, "application/json");
                    using (HttpResponseMessage response = client.PostAsync(URL, stringContent).Result)
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