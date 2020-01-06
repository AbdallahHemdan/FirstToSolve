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
    public class TeamController : Controller
    {
        public ActionResult Index()
        {
            return RedirectToAction("ViewAllTeams");
        }
        public ActionResult CreateTeam(int id = 0, string UserName = "")
        {
            ViewBag.id = id;
            ViewBag.UserName = UserName;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateTeam(TeamModel TM, int LeaderId = 0, string UserName = "")
        {
            if (LeaderId == null || LeaderId == 0)
            {
                return RedirectToAction("MustSignIn");
            }

            DBController db = new DBController();

            TM.TeamID = db.Count_Teams() + 2;
            TM.LeaderID = LeaderId;

            db.InsertTeam(TM);
            db.InsertTeamMembers(TM.TeamID, TM.LeaderID);
            db.InsertTeamMembers(TM.TeamID, db.Select_Id_by_Email(TM.Member2));
            db.InsertTeamMembers(TM.TeamID, db.Select_Id_by_Email(TM.Member3));

            ViewBag.id = LeaderId;
            ViewBag.UserName = UserName;
            return RedirectToAction("ViewMyTeams", "Team", new {id = LeaderId, UserName = UserName});
        }

        public ActionResult ViewAllTeams(int id = 0, string UserName = "")
        {
            DBController dbController = new DBController();
            DataTable dt = dbController.SelectTeams();

            List<TeamModel> Teams = new List<TeamModel>();
            for (int i = 0; i < dt.Rows.Count; ++i)
            {
                TeamModel Team = new TeamModel();
                Team.TeamName = Convert.ToString(dt.Rows[i]["TeamName"]);
                Team.TeamID = int.Parse(Convert.ToString(dt.Rows[i]["TeamID"]));
                Team.LeaderID = Convert.ToInt32(dt.Rows[i]["LeaderID"]);

                Teams.Add(Team);
            }

            ViewBag.id = id;
            ViewBag.UserName = UserName;

            return View(Teams);
        }
        public ActionResult ViewMyTeams(int id = 0, string UserName = "")
        {
            if (id == null || id == 0)
            {
                return RedirectToAction("MustSignIn");
            }

            DBController dbController = new DBController();
            DataTable dt = dbController.Select_Teams_of_Member(id);
            
            if (dt == null)
            {
                return RedirectToAction("EmptyTeams");
            }
            List<TeamModel> Teams = new List<TeamModel>();
            for (int i = 0; i < dt.Rows.Count; ++i)
            {
                TeamModel Team = new TeamModel();
                Team.TeamName = Convert.ToString(dt.Rows[i]["TeamName"]);
                Team.TeamID = int.Parse(Convert.ToString(dt.Rows[i]["TeamID"]));
                Team.Rating = Convert.ToInt32(dt.Rows[i]["Rating"]);
                Team.LeaderID = Convert.ToInt32(dt.Rows[i]["LeaderID"]);

                Teams.Add(Team);
            }

            ViewBag.id = id;
            ViewBag.UserName = UserName;
            return View(Teams);
        }

        public ActionResult ViewTeam(int TeamID, int id = 0, string UserName = "")
        {
            DBController db = new DBController();
            DataTable TeamRow = db.Select_Team_By_ID(TeamID);
            DataTable TeamMembers = db.Select_Team_members_Names(TeamID);

            TeamModel TM = new TeamModel
            {
                TeamName = Convert.ToString(TeamRow.Rows[0]["TeamName"]),
                Rating = Convert.ToInt32(TeamRow.Rows[0]["Rating"]),
                TeamID = TeamID,
                LeaderID = Convert.ToInt32(TeamRow.Rows[0]["LeaderID"])
            };

            List<int> MembersIDS = new List<int>();
            MembersIDS.Add(Convert.ToInt32(TeamMembers.Rows[0]["MemberID"]));
            MembersIDS.Add(Convert.ToInt32(TeamMembers.Rows[1]["MemberID"]));
            MembersIDS.Add(Convert.ToInt32(TeamMembers.Rows[2]["MemberID"]));

            if (MembersIDS[1] == TM.LeaderID)
            {
                MembersIDS[1] = MembersIDS[0];
            }
            else if (MembersIDS[2] == TM.LeaderID)
            {
                MembersIDS[2] = MembersIDS[0];
            }
            MembersIDS[0] = TM.LeaderID;

            ViewBag.Leader  = Convert.ToString(db.SelectUserNameByID(MembersIDS[0]).Rows[0]["Fname"]) +
                              Convert.ToString(db.SelectUserNameByID(MembersIDS[0]).Rows[0]["Lname"]);
            ViewBag.Member2 = Convert.ToString(db.SelectUserNameByID(MembersIDS[1]).Rows[0]["Fname"]) +
                              Convert.ToString(db.SelectUserNameByID(MembersIDS[1]).Rows[0]["Lname"]);
            ViewBag.Member3 = Convert.ToString(db.SelectUserNameByID(MembersIDS[2]).Rows[0]["Fname"]) +
                              Convert.ToString(db.SelectUserNameByID(MembersIDS[2]).Rows[0]["Lname"]);

            ViewBag.id = id;
            ViewBag.UserName = UserName;
            return View(TM);
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
        public ActionResult TeamOptions()
        {
            return View();
        }
        public ActionResult SuccessfulCreationOfTeam()
        {
            return View();
        }
        public ActionResult EmptyTeams()
        {
            return View();
        }
        public ActionResult MustSignIn()
        {
            return View();
        }

        public ActionResult DeleteTeam(int TeamID, int id = 0, string UserName = "")
        {
            DBController db = new DBController();
            db.DeleteTeam(TeamID);

            ViewBag.Id = id;
            ViewBag.UserName = UserName;
            return RedirectToAction("ViewAllTeams");
        }
    }
}
