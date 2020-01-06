using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using A4A.DataAccess;
using A4A.Models;

namespace A4A.Controllers
{
    public class ContestController : Controller
    {
        [Route("Contest/ViewContest")]
        public ActionResult ViewContests(int id = 0, string UserName = "")
        {
            DBController dbController = new DBController();
            DataTable dt = dbController.SelectContests();
            List<ContestModel> list = new List<ContestModel>();
            for (int i = 0; i < dt.Rows.Count; ++i)
            { 
                Models.ContestModel Contest = new ContestModel();
                Contest.ContestID = Convert.ToInt16(dt.Rows[i]["ContestID"]);
                Contest.ContestName = Convert.ToString(dt.Rows[i]["ContestName"]);
                Contest.ContestDate = Convert.ToDateTime(dt.Rows[i]["ContestDate"]);
                Contest.ContestLength = int.Parse(Convert.ToString(dt.Rows[i]["ContestLength"]));

                Contest.ContestWriterID = int.Parse(Convert.ToString(dt.Rows[i]["ContestWriter"]));
                DataTable WriterName = dbController.SelectUserNameByID(Contest.ContestWriterID);
                Contest.ContestWriterName = Convert.ToString(WriterName.Rows[0]["Fname"]) + " " +
                                            Convert.ToString(WriterName.Rows[0]["Lname"]);

                list.Add(Contest);
            }

            
            ViewBag.ID = id;
            ViewBag.UserName = UserName;
            return View(list);
        }

        public ActionResult ViewMyContests(int id = 0, string UserName = "") 
        {
            if (id == 0)
            {
                return RedirectToAction("MustSignIn");
            }

            DBController dbController = new DBController();
            DataTable dt = dbController.SelectMyContests(id);
            List<ContestModel> list = new List<ContestModel>();
            if (dt == null)
            {
                return RedirectToAction("EmptyContests");
            }
            for (int i = 0; i < dt.Rows.Count; ++i)
            {
                Models.ContestModel Contest = new ContestModel();
                Contest.ContestID = Convert.ToInt16(dt.Rows[i]["ContestID"]);
                Contest.ContestName = Convert.ToString(dt.Rows[i]["ContestName"]);
                Contest.ContestDate = Convert.ToDateTime(dt.Rows[i]["ContestDate"]);
                Contest.ContestLength = int.Parse(Convert.ToString(dt.Rows[i]["ContestLength"]));

                Contest.ContestWriterID = int.Parse(Convert.ToString(dt.Rows[i]["ContestWriter"]));
                DataTable WriterName = dbController.SelectUserNameByID(Contest.ContestWriterID);
                Contest.ContestWriterName = Convert.ToString(WriterName.Rows[0]["Fname"]) + " " +
                                            Convert.ToString(WriterName.Rows[0]["Lname"]);

                list.Add(Contest);
            }

            ViewBag.id = id;
            ViewBag.UserName = UserName;
            return View(list);
        }

        public ActionResult ContestProblems(int ContestID, int id = 0, string UserName = "")
        {
            DBController dbController = new DBController();
            DataTable dt = dbController.SelectContestProblems(ContestID);
            List<ProblemModel> list = new List<ProblemModel>();
            for (int i = 0; i < dt.Rows.Count; ++i)
            {
                ProblemModel problem = new ProblemModel();
                problem.ProblemID = Convert.ToString(dt.Rows[i]["ProblemId"]);
                problem.ProblemName = Convert.ToString(dt.Rows[i]["ProblemName"]);
                problem.ProblemTopic = Convert.ToString(dt.Rows[i]["ProblemTopic"]);
                problem.ProblemLink = Convert.ToString(dt.Rows[i]["ProblemLink"]);
                problem.ProblemDifficulty = int.Parse(Convert.ToString(dt.Rows[i]["ProblemDifficulty"]));
                list.Add(problem);
            }

            ViewBag.ID = id;
            ViewBag.UserName = UserName;
            return View(list);
        }

        [HttpPost]
        public ActionResult InsertContest(ContestModel CM, int id, string UserName)
        {
            CM.ContestWriterID = id;
            DBController dbController = new DBController();
            dbController.InsertContest(CM);
            return RedirectToAction("ViewContests", new { id, UserName });
        }

        public ActionResult InsertContest(int id, string UserName)
        {
            if (id == 0)
            {
                return RedirectToAction("MustSignIn");
            }

            ContestModel CM = new ContestModel();
            DBController dbController = new DBController();
            DataTable dt = dbController.GetAvailableProblems();
            string AvailableProblems = dt.Rows[1]["Names"].ToString();                      

            ViewBag.Problem1 = new SelectList(AvailableProblems.Split(',').ToList(), "Problem1");
            ViewBag.Problem2 = new SelectList(AvailableProblems.Split(',').ToList(), "Problem2");
            ViewBag.Problem3 = new SelectList(AvailableProblems.Split(',').ToList(), "Problem3");
            ViewBag.Problem4 = new SelectList(AvailableProblems.Split(',').ToList(), "Problem4");
            ViewBag.Problem5 = new SelectList(AvailableProblems.Split(',').ToList(), "Problem5");

            ViewBag.id = id;
            ViewBag.UserName = UserName;
            return View(CM);
        }

        public ActionResult EmptyContests()
        {
            return View();
        }

        public ActionResult MustSignIn()
        {
            return View();
        }
        public ActionResult DeleteContest(int ContestID, int id = 0, string UserName = "")
        {
            DBController db = new DBController();
            db.DeleteContest(ContestID);

            ViewBag.Id = id;
            ViewBag.UserName = UserName;
            return RedirectToAction("ViewContests");
        }
    }
}