using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Fincon_Assessment.Common;
using Fincon_Assessment.Models;

using Newtonsoft.Json;

namespace Fincon_Assessment.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILogin objILogin = null;
        public LoginController(ILogin objiLogin)
        {
            this.objILogin = objiLogin;
        }
        public ActionResult Login(bool? isvalidate)
        {
            Login _Login = new Login();
            if (isvalidate != null && isvalidate == true)
            {
                if (Session["ErrorMessage"] != null && Session["ErrorMessage"].ToString() != string.Empty)
                {
                    _Login.ErrorMessage = Session["ErrorMessage"].ToString();
                }
                else
                {
                    Session["ErrorMessage"] = string.Empty;
                    _Login.ErrorMessage = string.Empty;
                }
            }
            else
            {
                Session["ErrorMessage"] = string.Empty;
                _Login.ErrorMessage = string.Empty;
            }

            _Login.UserName = string.Empty;
            _Login.Password = string.Empty;
            return this.View(_Login);
        }
        [HttpPost]
        public ActionResult Login(Login objUser)
        {
            if ((!string.IsNullOrEmpty(objUser.UserName) && !string.IsNullOrEmpty(objUser.Password)))
            {
                ReturnAPI objdata = this.objILogin.ValidateLogin(objUser);
                if (objdata.message == string.Empty)
                {
                    Register objClsUser = JsonConvert.DeserializeObject<Register>(objdata.data.ToString());
                    Functions.UpdateCookies(objClsUser.Name, objClsUser.Id.ToString());
                    return this.RedirectToAction("Index", "DashBoard");
                }
                else
                {
                    Session["ErrorMessage"] = objdata.message.ToString();
                    return this.RedirectToAction("Login", "Login", new { isvalidate = true });
                }
            }
            else
            {
                Session["ErrorMessage"] = "Please Enter UserName & Password.";
                return this.RedirectToAction("Login", "Login", new { isvalidate = true });
            }
        }
    }
}