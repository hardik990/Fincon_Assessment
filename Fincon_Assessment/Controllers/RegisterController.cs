using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Fincon_Assessment.Models;

namespace Fincon_Assessment.Controllers
{
    public class RegisterController : Controller
    {
        private readonly IRegister objIRegister = null;
        public RegisterController(IRegister objiRegister)
        {
            this.objIRegister = objiRegister;
        }
        public ActionResult Register(bool? isvalidate)
        {
            Register _Register = new Register();
            if (isvalidate != null && isvalidate == true)
            {
                if (Session["ErrorMessage"] != null && Session["ErrorMessage"].ToString() != string.Empty)
                {
                    _Register.ErrorMessage = Session["ErrorMessage"].ToString();
                }
                else
                {
                    Session["ErrorMessage"] = string.Empty;
                    _Register.ErrorMessage = string.Empty;
                }
            }
            else
            {
                Session["ErrorMessage"] = string.Empty;
                _Register.ErrorMessage = string.Empty;
            }
            
            _Register.Name = string.Empty;
            _Register.Email = string.Empty;
            _Register.Password = string.Empty;
            return this.View(_Register);
        }
        [HttpPost]
        public ActionResult Register(Register objUser)
        {
            if ((!string.IsNullOrEmpty(objUser.Name) && !string.IsNullOrEmpty(objUser.Password) && !string.IsNullOrEmpty(objUser.Email)))
            {
                ReturnAPI objdata = this.objIRegister.ValidateRegister(objUser);
                if(objdata.status == Status.OK)
                {
                    Session["ErrorMessage"] = objdata.data.ToString();
                }
                else
                {
                    Session["ErrorMessage"] = objdata.message.ToString();
                }
                return this.RedirectToAction("Register", "Register",new { isvalidate =true});
            }
            else
            {
                Session["ErrorMessage"] = "Please Enter Name & Password orEmail.";
                return this.RedirectToAction("Register", "Register", new { isvalidate = true });
            }
        }
    }
}