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
  
        public ActionResult ViewAllUsers()
        {
            DBController dbController = new DBController();
            DataTable dt = dbController.SelectUsers();

            if (dt == null || dt.Rows.Count == 0)
            {
                return RedirectToAction("EmptyUsers");
            }

            List<UsersModel> Users = new List<UsersModel>();
            for (int i = 0; i < dt.Rows.Count; ++i)
            {
                UsersModel User = new UsersModel();

                User.Name = Convert.ToString(dt.Rows[i]["Fname"]) 
                          + " " 
                          + Convert.ToString(dt.Rows[i]["Lname"]);

                User.ID = int.Parse(Convert.ToString(dt.Rows[i]["UserID"]));
                User.Rating = int.Parse(Convert.ToString(dt.Rows[i]["Rating"]));

                Users.Add(User);
            }

            return View(Users);
        }

        public ActionResult ViewUser(int ID)
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
                string UserName = Convert.ToString(dr["Fname"])
                                + " "
                                + Convert.ToString(dr["Lname"]);

                Session["ID"] = id;
                Session["UserName"] = UserName;

                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult Logout()
        {
            Session["ID"] = 0;

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Friends()
        {
            int ID = Convert.ToInt16(Session["ID"]);

            DBController dbController = new DBController();
            DataTable dt = dbController.SelectFriends(ID);

            if (dt == null && ID == 0)
            {
                return RedirectToAction("MustSignIn");
            }
            else if (dt == null)
            {
                return RedirectToAction("NoFriends", "User");
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

        public ActionResult NoFriends()
        {
            return View();
        }

        public ActionResult AddFriend()
        {
            int ID = Convert.ToInt16(Session["ID"]);

            if (ID == 0)
            {
                return RedirectToAction("MustSignIn");
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //TODO add friend by UserName or from the friend profile
        public ActionResult AddFriend(string Email = "")
        {
            DBController db = new DBController();
            int FriendID = db.Select_UserID_By_Email(Email);

            int UserID = Convert.ToInt16(Session["ID"]);
            string UserName = Session["ID"].ToString();

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
                return RedirectToAction("ViewUser" ,"User" ,new {ID =FriendID});
            }
        }

        //Admin or User
        public string GetType(int id)
        {
            DBController db = new DBController();
            return Convert.ToString(db.SelectTypeById(id));
        }

        public ActionResult CanNot()
        {
            return View();
        }

        public ActionResult MustSignIn()
        {
            return View();
        }

        public ActionResult EmptyUsers()
        {
            return View();
        }

    }
}