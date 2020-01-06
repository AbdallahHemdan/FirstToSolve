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
        public ActionResult Index(string UserName = "", int id = 0)
        {
            ViewBag.ID = id;
            ViewBag.UserName = UserName;
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