using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Fincon_Assessment.Common;
using Fincon_Assessment.Models;

namespace Fincon_Assessment.Controllers
{
    public class DashBoardController : Controller
    {
        private readonly IDashBoard objIDashBoard = null;
        public DashBoardController(IDashBoard objiDashBoard)
        {
            this.objIDashBoard = objiDashBoard;
        }
        public ActionResult Index(string message)
        {
            if (MySession.Current.UserId == 0)
            {
                return this.RedirectToAction("Login", "Login");
            }
            DashBoard _dashBoard = new DashBoard();
            _dashBoard.UserName = MySession.Current.UserName;
            _dashBoard.lstData = this.objIDashBoard.GetQuotation().ToList();
            if (!string.IsNullOrEmpty(message))
            {
                _dashBoard.message = message;
            }
            else
            {
                _dashBoard.message = string.Empty;
            }
            return View(_dashBoard);
        }
        public ActionResult ViewQuotation(string Id)
        {
            Session["IsEdit"] = "0";
            return this.RedirectToAction("Index", "General", new { Id = Id });
        }
        public ActionResult EditQuotation(string Id)
        {
            Session["IsEdit"] = "1";
            return this.RedirectToAction("Index", "General", new { Id = Id });
        }
        public ActionResult DeleteQuotation(string Id)
        {
            ReturnAPI objdata = this.objIDashBoard.DeleteQuotation(int.Parse(Id));
            return this.RedirectToAction("Index", "DashBoard", new { message = string.Empty });

        }
        public ActionResult NewQuotation()
        {
            Session["IsEdit"] = "1";
            return this.RedirectToAction("Index", "General", new { Id = 0 });
        }
    }
}