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
        public ActionResult ViewContests()
        {
            DBController dbController = new DBController();
            DataTable dt = dbController.SelectContests();

            if (dt == null)
            {
                return RedirectToAction("EmptyContests");
            }

            List<ContestModel> Contests = new List<ContestModel>();
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

                Contests.Add(Contest);
            }

            return View(Contests);
        }

        public ActionResult ViewMyContests() 
        {
            int ID = Convert.ToInt16(Session["ID"]);
            if (ID == 0)
            {
                return RedirectToAction("MustSignIn");
            }

            DBController dbController = new DBController();
            DataTable dt = dbController.SelectMyContests(ID);

            if (dt == null)
            {
                return RedirectToAction("EmptyContests");
            }

            List<ContestModel> Contests= new List<ContestModel>();
            for (int i = 0; i < dt.Rows.Count; ++i)
            {
                Models.ContestModel Contest = new ContestModel();
                Contest.ContestID = Convert.ToInt16(dt.Rows[i]["ContestID"]);
                Contest.ContestName = Convert.ToString(dt.Rows[i]["ContestName"]);
                Contest.ContestDate = Convert.ToDateTime(dt.Rows[i]["ContestDate"]);
                Contest.ContestLength = int.Parse(Convert.ToString(dt.Rows[i]["ContestLength"]));
                Contest.ContestWriterID = int.Parse(Convert.ToString(dt.Rows[i]["ContestWriter"]));

                DataTable WriterName = dbController.SelectUserNameByID(Contest.ContestWriterID);
                Contest.ContestWriterName = Convert.ToString(WriterName.Rows[0]["Fname"]) 
                                          + " " 
                                          + Convert.ToString(WriterName.Rows[0]["Lname"]);

                Contests.Add(Contest);
            }

            return View(Contests);
        }

        public ActionResult ContestProblems(int ContestID)
        {
            DBController dbController = new DBController();
            DataTable dt = dbController.SelectContestProblems(ContestID);

            List<ProblemModel> Contests = new List<ProblemModel>();
            for (int i = 0; i < dt.Rows.Count; ++i)
            {
                ProblemModel problem = new ProblemModel();
                problem.ProblemID = Convert.ToString(dt.Rows[i]["ProblemId"]);
                problem.ProblemName = Convert.ToString(dt.Rows[i]["ProblemName"]);
                problem.ProblemTopic = Convert.ToString(dt.Rows[i]["ProblemTopic"]);
                problem.ProblemLink = Convert.ToString(dt.Rows[i]["ProblemLink"]);
                problem.ProblemDifficulty = int.Parse(Convert.ToString(dt.Rows[i]["ProblemDifficulty"]));
                Contests.Add(problem);
            }

            return View(Contests);
        }

        [HttpPost]
        public ActionResult InsertContest(ContestModel CM)
        {
            CM.ContestWriterID = Convert.ToInt16(Session["ID"]);
            DBController dbController = new DBController();
            dbController.InsertContest(CM);

            return RedirectToAction("ViewContests");
        }

        public ActionResult InsertContest()
        {
            int ID = Convert.ToInt16(Session["ID"]);

            if (ID == 0)
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

        public ActionResult DeleteContest(int ContestID)
        {
            DBController db = new DBController();
            db.DeleteContest(ContestID);

            return RedirectToAction("ViewContests");
        }
    }
}