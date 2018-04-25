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
            if (lg.UserName.ToUpper() == "KAYMUN" && lg.Password == "kaymun@123")
            {
                Session["Uid"] = lg.UserName;

                return RedirectToAction("Index", "Report");
            }
            else if (lg.UserName.ToUpper() == "SANJUKTA" && lg.Password == "sanjukta123")
            {
                Session["Uid"] = lg.UserName;

                return RedirectToAction("Index", "Report");
            }
            else if (lg.UserName.ToUpper() == "SAEDUR" && lg.Password == "saedur123")
            {
                Session["Uid"] = lg.UserName;

                return RedirectToAction("Index", "Report");
            }
            else if (lg.UserName.ToUpper() == "TASNIM" && lg.Password == "tasnim123")
            {
                Session["Uid"] = lg.UserName;

                return RedirectToAction("Index", "Report");
            }
            else if (lg.UserName.ToUpper() == "SADIA" && lg.Password == "sadia@123")
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