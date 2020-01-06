using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using A4A.DataAccess;
using System.Data;
using A4A.Models;
using Microsoft.Ajax.Utilities;

namespace A4A.Controllers
{
    public class BlogController : Controller
    {
        // GET: Blog
        public ActionResult ViewAllBlogs(int id = 0, string UserName = "")
        {
            ViewBag.ID = id;
            ViewBag.UserName = UserName;

            DBController dbController = new DBController();
            DataTable dt = dbController.SelectBlogs();

            if (dt == null)
            {
                return RedirectToAction("EmptyBlogs", new {id, UserName});
            }

            List<BlogModel> list = new List<BlogModel>();
            for (int i = 0; i < dt.Rows.Count; ++i)
            {
                Models.BlogModel Blog = new BlogModel();
                Blog.BlogTitle = Convert.ToString(dt.Rows[i]["BlogTitle"]);
                Blog.BlogContent = Convert.ToString(dt.Rows[i]["BlogContent"]);
                Blog.BlogID = Convert.ToInt32(dt.Rows[i]["BlogID"]);
                Blog.BlogWriter = Convert.ToInt32(dt.Rows[i]["BlogWriter"]);
                if (Convert.ToString(dt.Rows[i]["GroupID"]) != "")
                    Blog.GroupID = Convert.ToInt32(dt.Rows[i]["GroupID"]);
                else
                    Blog.GroupID = 0;
                DataTable tmp_dt = dbController.SelectUserNameByID(Blog.BlogWriter);
                Blog.BlogWriterName = Convert.ToString(tmp_dt.Rows[0]["Fname"]) + Convert.ToString(tmp_dt.Rows[0]["Lname"]);
                list.Add(Blog);
            }
            return View(list);
        }
        public ActionResult ViewMyBlogs(int id = 0, string UserName = "")
        {
            if (id == 0 || id == null)
            {
                return RedirectToAction("MustSignIn");
            }

            DBController dbController = new DBController();
            DataTable dt = dbController.SelectMyBlogs(id);

            if (dt == null)
            {
                return RedirectToAction("EmptyBlogs", new {id, UserName});
            }

            List<BlogModel> list = new List<BlogModel>();
            for (int i = 0; i < dt.Rows.Count; ++i)
            {
                Models.BlogModel Blog = new BlogModel();
                Blog.BlogTitle = Convert.ToString(dt.Rows[i]["BlogTitle"]);
                Blog.BlogContent = Convert.ToString(dt.Rows[i]["BlogContent"]);
                Blog.BlogID = Convert.ToInt32(dt.Rows[i]["BlogID"]);
                Blog.BlogWriter = Convert.ToInt32(dt.Rows[i]["BlogWriter"]);
                if (Convert.ToString(dt.Rows[i]["GroupID"]) != "")
                    Blog.GroupID = Convert.ToInt32(dt.Rows[i]["GroupID"]);
                else
                    Blog.GroupID = 0;
                DataTable tmp_dt = dbController.SelectUserNameByID(Blog.BlogWriter);
                Blog.BlogWriterName = Convert.ToString(tmp_dt.Rows[0]["Fname"]) + Convert.ToString(tmp_dt.Rows[0]["Lname"]);
                list.Add(Blog);
            }


            ViewBag.ID = id;
            ViewBag.UserName = UserName;
            return View(list);
        }
        public ActionResult ViewBlog(int BlogID, int id = 0, string UserName = "")
        {
            DBController dbController = new DBController();
            DataTable dt = dbController.SelectABlogs(BlogID);

            Models.BlogModel Blog = new BlogModel();
            Blog.BlogTitle = Convert.ToString(dt.Rows[0]["BlogTitle"]);
            Blog.BlogContent = Convert.ToString(dt.Rows[0]["BlogContent"]);
            Blog.BlogID = Convert.ToInt32(dt.Rows[0]["BlogID"]);
            Blog.BlogWriter = Convert.ToInt32(dt.Rows[0]["BlogWriter"]);
            Blog.GroupID = Convert.ToInt32(dt.Rows[0]["GroupID"]);
            DataTable tmp_dt = dbController.SelectUserNameByID(Blog.BlogWriter);
            Blog.BlogWriterName = Convert.ToString(tmp_dt.Rows[0]["Fname"]) + Convert.ToString(tmp_dt.Rows[0]["Lname"]);

            ViewBag.ID = id;
            ViewBag.UserName = UserName;
            return View(Blog);
        }
        [HttpPost]
        public ActionResult InsertBlog(BlogModel Blog, int id = 0, string UserName = "")
        {
            DBController dbController = new DBController();
            int done = dbController.InsertBlog(Blog.BlogTitle, id, 1, Blog.BlogContent);
            return RedirectToAction("ViewAllBlogs", new { id, UserName });
        }

        public ActionResult InsertBlog(int id = 0, string UserName = "")
        {
            if (id == 0 || id == null)
            {
                return RedirectToAction("MustSignIn");
            }

            ViewBag.id = id;
            ViewBag.UserName = UserName;
            return View();

        }

        public ActionResult DeleteBlog(int BlogId, int id = 0, string UserName = "")
        {
            DBController db = new DBController();
            db.DeleteBlog(BlogId);

            ViewBag.Id = id;
            ViewBag.UserName = UserName;
            return RedirectToAction("ViewAllBlogs", new {id = id, UserName= UserName});
        }

        public ActionResult EmptyBlogs(int id = 0, string UserName = "")
        {
            ViewBag.id = id;
            ViewBag.UserName = UserName;
            return View();
        }

        public ActionResult MustSignIn()
        {
            return View();
        }
    }
}