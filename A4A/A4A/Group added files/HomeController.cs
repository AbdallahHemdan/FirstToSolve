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
            return View();
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

        public ActionResult ProblemSet()
        {
            DBController dbController = new DBController();
            DataTable dt = dbController.SelectProblems();
            List<ProblemSetModel> list = new List<ProblemSetModel>();
            for (int i = 0; i < dt.Rows.Count; ++i)
            {
                ProblemSetModel problem = new ProblemSetModel();
                problem.ProblemName = Convert.ToString(dt.Rows[i]["ProblemName"]);
                problem.ProblemTopic = Convert.ToString(dt.Rows[i]["ProblemTopic"]);
                problem.ProblemLink = Convert.ToString(dt.Rows[i]["ProblemLink"]);
                problem.ProblemDifficulty = int.Parse(Convert.ToString(dt.Rows[i]["ProblemDifficulty"]));
                list.Add(problem);
            }
            return View(list);
        }

        public ActionResult GroupOptions()
        {
            return View();
        }
        public ActionResult CreateGroup()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateGroup(GroupModel model)
        {
            if (ModelState.IsValid)
            {

                return RedirectToAction("SuccessfulCreationOfGroup");
            }
            return View();
        }
        public ActionResult ViewMyGroups(/*int UserID*/)
        {
            //DBController dbController = new DBController();
            //DataTable dt = dbController.SelectGroupsOfUser(UserID);
            //List<GroupModel> list = new List<GroupModel>();
            //for (int i = 0; i < dt.Rows.Count; ++i)
            //{
            //    GroupModel group = new GroupModel();
            //    group.GroupName = Convert.ToString(dt.Rows[i]["GroupName"]);
            //    group.GroupId = int.Parse(Convert.ToString(dt.Rows[i]["GroupId"]));
            //    group.GroupAdmin = int.Parse(Convert.ToString(dt.Rows[i]["GroupAdmin"]));
            //    list.Add(group);
            //}
            //return View(list);
            return View();
        }
        public ActionResult ExploreGroups()
        {
            DBController dbController = new DBController();
            DataTable dt = dbController.SelectAllGroups();
            List<GroupModel> list = new List<GroupModel>();
            for (int i = 0; i < dt.Rows.Count; ++i)
            {
                GroupModel group = new GroupModel();
                group.GroupName = Convert.ToString(dt.Rows[i]["GroupName"]);
                //group.GroupId = int.Parse(Convert.ToString(dt.Rows[i]["GroupId"]));
                group.GroupAdmin = int.Parse(Convert.ToString(dt.Rows[i]["GroupAdmin"]));
                list.Add(group);
            }
            return View(list);
            //return View();
        }
        public ActionResult SuccessfulCreationOfGroup()
        {
            return View();
        }
    }
}