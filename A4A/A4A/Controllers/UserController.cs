using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.HtmlControls;
using A4A.DataAccess;
using A4A.Models;
using Microsoft.Ajax.Utilities;
using OpenQA.Selenium.Internal;

namespace A4A.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult ViewAllUsers(int id = 0, string UserName = "")
        {
            DBController dbController = new DBController();
            DataTable dt = dbController.SelectUsers();

            List<UsersModel> list = new List<UsersModel>();
            for (int i = 0; i < dt.Rows.Count; ++i)
            {
                UsersModel User = new UsersModel();

                User.Name = Convert.ToString(dt.Rows[i]["Fname"]) + " " + Convert.ToString(dt.Rows[i]["Lname"]);

                User.ID = int.Parse(Convert.ToString(dt.Rows[i]["UserID"]));
                User.Rating = int.Parse(Convert.ToString(dt.Rows[i]["Rating"]));
                list.Add(User);
            }

            ViewBag.id = id;
            ViewBag.UserName = UserName;
            return View(list);
        }
        public ActionResult ViewUser(int ID, int IV = 0, string UserName = "",bool ShowFriends = false)
        {
            DBController dbController = new DBController();
            DataTable dt = dbController.SelectUser(ID);
            AccountModel User = new AccountModel();

            User.Fname = Convert.ToString(dt.Rows[0]["Fname"]);
            User.Lname = Convert.ToString(dt.Rows[0]["Lname"]);
            User.Email = Convert.ToString(dt.Rows[0]["Email"]);
            User.Rating = int.Parse(Convert.ToString(dt.Rows[0]["Rating"]));
            User.Binding = dbController.Binding(ID);
            User.Solved = dbController.Solved(ID);

            ViewBag.ID = IV;
            ViewBag.UserName = UserName;
            ViewBag.ShowFriends = ShowFriends;

            return View(User);
        }

        public ActionResult CreateUser()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateUser(AccountModel accountModel)
        {
            DBController db = new DBController();

            accountModel.ID = db.Count_Users() + 1;
            accountModel.Rating = 0;
            accountModel.Solved = 0;
            accountModel.Binding = 0;
            accountModel.Type = "User";
            db.InsertUser(accountModel);

            return RedirectToAction("ViewAllUsers");
        }


        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string Email, string Password)
        {
            DBController db = new DBController();
            int id = db.Select_User_ID(Email, Password);

            if (id == 0) // not found
            {
                //TODO uncorrect email or Password
                return View();
            }
            else
            {
                //view of user (home page)
                DataRow dr = db.SelectUserNameByID(id).Rows[0];
                string UserName = Convert.ToString(dr["Fname"]) + Convert.ToString(dr["Lname"]);
                Session["ID"] = id;
                Session["UserName"] = UserName;
                return RedirectToAction("Index", "Home", new {UserName = UserName, id = id});
            }
        }
        public ActionResult Logout()
        {
            ViewBag.id = 0;
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Friends(int ID = 0, string UserName = "")
        {
            ViewBag.id = ID;
            ViewBag.UserName = UserName;

            DBController dbController = new DBController();
            DataTable dt = dbController.SelectFriends(ID);
            if (dt == null && ID == 0)
            {
                return RedirectToAction("MustSignIn");
            }
            else if (dt == null)
            {
                return RedirectToAction("NoFriends", "User", new { id = ID, UserName = UserName });
            }

            List<UsersModel> list = new List<UsersModel>();
            for (int i = 0; i < dt.Rows.Count; ++i)
            {
                UsersModel Friend = new UsersModel();

                Friend.Name = Convert.ToString(dt.Rows[i]["Fname"]) + " " + Convert.ToString(dt.Rows[i]["Lname"]);

                Friend.ID = int.Parse(Convert.ToString(dt.Rows[i]["UserID"]));
                Friend.Rating = int.Parse(Convert.ToString(dt.Rows[i]["Rating"]));
                list.Add(Friend);
            }


            return View(list);
        }
        public ActionResult NoFriends(int ID = 0, string UserName = "")
        {
            ViewBag.ID = ID;
            ViewBag.UserName = UserName;
            return View();
        }

        public ActionResult AddFriend(int ID = 0,string UserName="")
        {
            ViewBag.ID = ID;
            ViewBag.UserName = UserName;

            if (ID == null || ID == 0)
            {
                return RedirectToAction("MustSignIn");
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddFriend(int UserID = 0, string UserName = "", string Email = "")
        {
            DBController db = new DBController();
            int FriendID = db.Select_UserID_By_Email(Email);
            if (UserID == FriendID)
            {
                return RedirectToAction("CanNot", new {id = UserID, UserName = UserName});
            }
            
            ViewBag.ID = UserID;
            ViewBag.UserName = UserName;

            if (FriendID == 0 ) // not found
            {
                //TODO uncorrect email or Password
                return View();
            }
            else
            {
                db.InsertFriend(UserID, FriendID);
                return RedirectToAction("ViewUser" ,"User" ,new {ID =FriendID ,IV =UserID , UserName = UserName});
            }
        }

        public string GetType(int id)
        {
            DBController db = new DBController();
            return Convert.ToString(db.SelectTypeById(id));
        }

        public ActionResult CanNot(int id = 0, string UserName = "")
        {
            ViewBag.ID = id;
            ViewBag.UserName = UserName;
            return View();
        }
        public ActionResult MustSignIn()
        {
            return View();
        }

    }
}