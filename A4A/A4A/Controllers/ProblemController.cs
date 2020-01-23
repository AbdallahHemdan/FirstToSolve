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
        public ActionResult ProblemSet()
        {
            DBController dbController = new DBController();
            DataTable dt = dbController.SelectProblems();

            List<ProblemModel> Problems = new List<ProblemModel>();
            for (int i = 0; i < dt.Rows.Count; ++i)
            {
                ProblemModel problem = new ProblemModel();
                problem.ProblemName = Convert.ToString(dt.Rows[i]["ProblemName"]);
                problem.ProblemTopic = Convert.ToString(dt.Rows[i]["ProblemTopic"]);
                problem.ProblemLink = Convert.ToString(dt.Rows[i]["ProblemLink"]);
                problem.ProblemID = Convert.ToString(dt.Rows[i]["ProblemID"]);
                problem.ProblemDifficulty = int.Parse(Convert.ToString(dt.Rows[i]["ProblemDifficulty"]));
                Problems.Add(problem);
            }

            return View(Problems);
        }

        // GET: Problem
        [Route("Problem/ViewProblem")]
        public ActionResult ViewProblem(string ProblemID, string ProblemLink)
        {
            ProblemModel Problem = new ProblemModel
            {
                ProblemLink = ProblemLink,
                ProblemID = ProblemID,
            };
          
            return View(Problem);
        }

        public ActionResult ViewSubmission(int SubmissionID)
        {
            DBController db = new DBController();
            DataTable dt = db.GetSubmissionByID(SubmissionID);

            string UserName = Session["UserName"].ToString();

            List<SubmissionModel> Submissions = new List<SubmissionModel>();
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

                Submissions.Add(Submission);
            }

            return View(Submissions);
        }

        [HttpPost]
        public ActionResult Submit(ProblemModel Problem, string ProblemID)
        {
            int pos, ContestID;
            bool isTwoChars = int.TryParse(ProblemID.Substring(ProblemID.Length - 1), out pos);

            int ID = Convert.ToInt16(Session["ID"]);

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
            int SubmissionID = helper.ParseSubmission(SubmissionJson, ID, Problem.ProblemContestID);

            return RedirectToAction("ViewSubmission", new { SubmissionID = SubmissionID});
        }

        public ActionResult DeleteProblem(string ProblemID)
        {
            DBController db = new DBController();
            db.DeleteProblem(ProblemID);

            return RedirectToAction("ProblemSet");
        }
    }
}