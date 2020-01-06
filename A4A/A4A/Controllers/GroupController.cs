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
    public class GroupController : Controller
    {
        public ActionResult Index()
        {
            return RedirectToAction("ViewAllGroups");
        }
        public ActionResult CreateGroup(int id = 0, string UserName = "")
        {
            if (id == 0)
            {
                return RedirectToAction("MustSignIn");
            }
            ViewBag.id = id;
            ViewBag.UserName = UserName;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateGroup(GroupModel GM, int AdminID, int ID = 0, string UserName = "")
        {
            DBController db = new DBController();

            GM.GroupID = db.CountGroups() + 1;
            GM.AdminID = AdminID;

            ViewBag.UserName = UserName;
            ViewBag.ID = ID;
            db.InsertGroup(GM);
            return View();
        }
        public ActionResult ViewAllGroups(int id = 0, string UserName="")
        {
            
            DBController dbController = new DBController();
            DataTable dt = dbController.SelectAllGroups();

            List<GroupModel> Groups = new List<GroupModel>();
            for (int i = 0; i < dt.Rows.Count; ++i)
            {
                GroupModel group = new GroupModel();
                group.GroupName = Convert.ToString(dt.Rows[i]["GroupName"]);
                group.GroupID = int.Parse(Convert.ToString(dt.Rows[i]["GroupId"]));
                group.AdminID = int.Parse(Convert.ToString(dt.Rows[i]["GroupAdmin"]));
                
                Groups.Add(group);
            }

            ViewBag.id = id;
            ViewBag.UserName = UserName;
            return View(Groups);
        }
        public ActionResult ViewMyGroups(int id = 0, string UserName = "")
        {
            if (id == 0)
            {
                return RedirectToAction("MustSignIn");
            }
            DBController dbController = new DBController();
            DataTable dt = dbController.SelectMyGroups(id);
            if (dt == null && id == 0)
            {
                return RedirectToAction("MustSignIn");
            }
            else if (dt == null)
            {
                return RedirectToAction("EmptyGroups");
            }
            List<GroupModel> MyGroups = new List<GroupModel>();
            for (int i = 0; i < dt.Rows.Count; ++i)
            {
                GroupModel group = new GroupModel();
                group.GroupName = Convert.ToString(dt.Rows[i]["GroupName"]);
                group.GroupID = int.Parse(Convert.ToString(dt.Rows[i]["GroupId"]));
                group.AdminID = int.Parse(Convert.ToString(dt.Rows[i]["GroupAdmin"]));
                MyGroups.Add(group);
            }

            ViewBag.id = id;
            ViewBag.UserName = UserName;
            return View(MyGroups);
        }

        public ActionResult ViewGroupContests(int GroupId, int id = 0, string UserName = "")
        {
            DBController db = new DBController();
            DataTable dt = db.SelectGroupContests(GroupId);

            List<ContestModel> Contests = new List<ContestModel>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ContestModel CM = new ContestModel
                {
                    ContestName = Convert.ToString(dt.Rows[i]["ContestName"]),
                    ContestLength = Convert.ToInt32(dt.Rows[i]["ContestLength"]),
                    ContestDate = Convert.ToDateTime(dt.Rows[i]["ContestDate"]),
                    ContestWriterID = Convert.ToInt32(dt.Rows[i]["ContestWriter"])
                };
                CM.ContestWriterName = Convert.ToString(db.SelectUserNameByID(CM.ContestWriterID).Rows[0]["Fname"]) +
                                       " " +
                                       Convert.ToString(db.SelectUserNameByID(CM.ContestWriterID).Rows[0]["Lname"]);
                Contests.Add(CM);
            }

            ViewBag.GroupId = GroupId;
            ViewBag.id = id;
            ViewBag.UserName = UserName;
            return View(Contests);
        }

        public ActionResult ViewGroupMembers(int GroupId, int id = 0, string UserName = "")
        {
            DBController db = new DBController();
            DataTable dt = db.SelectGroupMembers(GroupId);

            List<AccountModel> Members = new List<AccountModel>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                AccountModel AM = new AccountModel
                {
                    Fname = Convert.ToString(dt.Rows[i]["Fname"]),
                    Lname = Convert.ToString(dt.Rows[i]["Lname"]),
                    Email = Convert.ToString(dt.Rows[i]["Email"]),
                    Rating = int.Parse(Convert.ToString(dt.Rows[i]["Rating"])),
                    Type = Convert.ToString(dt.Rows[i]["Type"])
                };
                Members.Add(AM);
            }

            ViewBag.GroupId = GroupId;
            ViewBag.id = id;
            ViewBag.UserName = UserName;
            return View(Members);
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
        public ActionResult GroupOptions()
        {
            return View();
        }      
        public ActionResult SuccessfulCreationOfGroup()
        {
            return View();
        }

        public ActionResult EmptyGroups()
        {
            return View();
        }
        public ActionResult MustSignIn()
        {
            return View();
        }

        public ActionResult DeleteGroup(int GroupID, int id = 0, string UserName = "")
        {
            DBController db = new DBController();
            db.DeleteGroup(GroupID);

            ViewBag.Id = id;
            ViewBag.UserName = UserName;
            return RedirectToAction("ViewAllGroups");
        }
    }
}
