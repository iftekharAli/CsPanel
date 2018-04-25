using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CsPanel.Models;

namespace CsPanel.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(Login lg)
        {
            if (lg.UserName.ToUpper() == "ADMIN" && lg.Password == "cspanel")
            {
                Session["Uid"] = lg.UserName;

                return RedirectToAction("Index", "Report");
            }
            else
            {
                ModelState.AddModelError("", @"Invalid user Name/Password");
                return View(lg);

            }


        }
        public ActionResult Logout()
        {
            Session["Uid"] = null;
            return RedirectToAction("Index", "Login");
        }


    }
}