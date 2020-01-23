using A4A.DataAccess;
using A4A.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace A4A.Controllers
{
    public class OrgController : Controller
    {
        public ActionResult CreateOrg()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateOrg(OrgModel OM)
        {
            DBController db = new DBController();

            OM.OrgID = db.Count_Orgs() + 1;

            //TODO
            OM.AdminID = 2;

            db.InsertOrg(OM);
            return View();
        }
        public ActionResult ViewAllOrgs()
        {
            DBController dbController = new DBController();
            DataTable dt = dbController.SelectOrgs();

            List<OrgModel> Orgs = new List<OrgModel>();
            for (int i = 0; i < dt.Rows.Count; ++i)
            {
                OrgModel Org= new OrgModel();
                Org.OrgName = Convert.ToString(dt.Rows[i]["OrganizationName"]);
                Org.OrgID = int.Parse(Convert.ToString(dt.Rows[i]["OrganizationID"]));
                Org.AdminID = Convert.ToInt32(dt.Rows[i]["AdminID"]);

                Orgs.Add(Org);
            }
            return View(Orgs);
        }
        public ActionResult ViewGroupOrgs(int GroupID)
        {
            DBController dbController = new DBController();
            DataTable dt = dbController.SelectMyGroups(GroupID);
            List<OrgModel> Orgs = new List<OrgModel>();
            for (int i = 0; i < dt.Rows.Count; ++i)
            {
                OrgModel Org = new OrgModel();
                Org.OrgName = Convert.ToString(dt.Rows[i]["OrganizationName"]);
                Org.OrgID = int.Parse(Convert.ToString(dt.Rows[i]["OrganizationID"]));
                Org.AdminID = Convert.ToInt32(dt.Rows[i]["AdminID"]);

                Orgs.Add(Org);
            }
            return View(Orgs);
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult OrgOptions()
        {
            return View();
        }
        public ActionResult SuccessfulCreationOfOrg()
        {
            return View();
        }
        public ActionResult DeleteOrg(int OrgID, int id = 0, string UserName = "")
        {
            DBController db = new DBController();
            db.DeleteOrg(OrgID);

            ViewBag.Id = id;
            ViewBag.UserName = UserName;
            return RedirectToAction("ViewAllOrgs");
        }
    }
}
