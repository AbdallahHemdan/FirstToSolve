using System;
using System.Collections.Generic;
using A4A.DataAccess;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.Mvc;
using A4A.Models;
using System.IO;
using System.Data.SqlClient;

namespace A4A.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //initializing of ID in case of a guest not a user and to handle Errors
            if (Session["ID"] == null)
                Session["ID"] = 0;

            return View();
        }

        public ActionResult About(string UserName = "", int id = 0)
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact(string UserName = "", int id = 0)
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}