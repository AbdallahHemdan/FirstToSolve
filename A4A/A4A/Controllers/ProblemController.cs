using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;
using A4A.DataAccess;
using A4A.Models;
using AutoCodeforces;
using Newtonsoft.Json;
using Helpers;

namespace A4A.Controllers
{
    public class ProblemController : Controller
    {
        public ActionResult ProblemSet(int id = 0, string UserName = "Login")
        {
            DBController dbController = new DBController();
            DataTable dt = dbController.SelectProblems();
            List<ProblemModel> list = new List<ProblemModel>();
            for (int i = 0; i < dt.Rows.Count; ++i)
            {
                ProblemModel problem = new ProblemModel();
                problem.ProblemName = Convert.ToString(dt.Rows[i]["ProblemName"]);
                problem.ProblemTopic = Convert.ToString(dt.Rows[i]["ProblemTopic"]);
                problem.ProblemLink = Convert.ToString(dt.Rows[i]["ProblemLink"]);
                problem.ProblemID = Convert.ToString(dt.Rows[i]["ProblemID"]);
                problem.ProblemDifficulty = int.Parse(Convert.ToString(dt.Rows[i]["ProblemDifficulty"]));
                list.Add(problem);
            }

            ViewBag.UserName = UserName;
            ViewBag.ID = id;
            return View(list);
        }

        // GET: Problem
        [Route("Problem/ViewProblem")]
        public ActionResult ViewProblem(string ProblemID, string ProblemLink, string UserName = "Login", int id = 0)
        {
            ProblemModel Problem = new ProblemModel
            {
                ProblemLink = ProblemLink,
                ProblemID = ProblemID,
            };

            ViewBag.ID = id;
            ViewBag.UserName = UserName;
            return View(Problem);
        }

        public ActionResult ViewSubmission(int SubmissionID, int id, string UserName)
        {
            DBController db = new DBController();
            DataTable dt = db.GetSubmissionByID(SubmissionID);

            List<SubmissionModel> list = new List<SubmissionModel>();
            for (int i = 0; i < dt.Rows.Count; ++i)
            {
                SubmissionModel Submission = new SubmissionModel();
                Submission.SubmissionID = Convert.ToInt32(dt.Rows[i]["SubmissionID"]);

                DataTable ContestantName = db.SelectUserNameByID(Convert.ToInt32(dt.Rows[i]["ContestantID"]));
                ViewBag.ContestantName = UserName;


                Submission.SubmissionVerdict = Convert.ToString(dt.Rows[i]["SubmissionVerdict"]);
                Submission.SubmissionMemory = Convert.ToInt32(dt.Rows[i]["SubmissionMemory"]);
                Submission.SubmissionDate = Convert.ToDateTime(dt.Rows[i]["SubmissionDate"]);
                Submission.SubmissionTime = Convert.ToInt32(dt.Rows[i]["SubmissionTime"]);
                Submission.SubmissionLang = Convert.ToString(dt.Rows[i]["SubmissionLang"]);

                Submission.ProblemID = Convert.ToString(dt.Rows[i]["ProblemID"]);
                ViewBag.ProblemName = db.GetProblemNameByID(Submission.ProblemID);

                list.Add(Submission);
            }

            ViewBag.ID = id;
            ViewBag.UserName = UserName;
            return View(list);
        }

        [HttpPost]
        public ActionResult Submit(ProblemModel Problem, string ProblemID, int id, string UserName)
        {
            int pos, ContestID;
            bool isTwoChars = int.TryParse(ProblemID.Substring(ProblemID.Length - 1), out pos);
            if (isTwoChars)
            {
                ContestID = int.Parse(ProblemID.Substring(0, ProblemID.Length - 2));
            }
            else
            {
                ContestID = int.Parse(ProblemID.Substring(0, ProblemID.Length - 1)); 
            }
            Automate Judge = new Automate();
            Judge.OpenCodeforces(Problem.ProblemCode, ProblemID);
            string SubmissionJson = Judge.SubmissionJsonFile("A4A_A4A", ContestID);
            Helpers.SubmissionHelper helper = new Helpers.SubmissionHelper();
            int SubmissionID = helper.ParseSubmission(SubmissionJson, id, Problem.ProblemContestID);
            return RedirectToAction("ViewSubmission", new { SubmissionID = SubmissionID, id = id, UserName = UserName });
        }
        public ActionResult DeleteProblem(string ProblemID, int id = 0, string UserName = "")
        {
            DBController db = new DBController();
            db.DeleteProblem(ProblemID);

            ViewBag.Id = id;
            ViewBag.UserName = UserName;
            return RedirectToAction("ProblemSet");
        }
    }
}