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
        public ActionResult ViewAllBlogs()
        {
            DBController dbController = new DBController();
            DataTable dt = dbController.SelectBlogs();

            if (dt == null)
            {
                return RedirectToAction("EmptyBlogs");
            }

            List<BlogModel> Blogs = new List<BlogModel>();

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

                Blogs.Add(Blog);
            }

            return View(Blogs);
        }

        public ActionResult ViewMyBlogs()
        {
            int ID = Convert.ToInt16(Session["ID"]);

            if (ID == 0)
            {
                return RedirectToAction("MustSignIn");
            }

            DBController dbController = new DBController();
            DataTable dt = dbController.SelectMyBlogs(ID);

            if (dt == null)
            {
                return RedirectToAction("EmptyBlogs");
            }

            List<BlogModel> Blogs = new List<BlogModel>();
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

                Blogs.Add(Blog);
            }

            return View(Blogs);
        }

        public ActionResult ViewBlog(int BlogID)
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

            return View(Blog);
        }

        [HttpPost]
        public ActionResult InsertBlog(BlogModel Blog)
        {
            int ID = Convert.ToInt16(Session["ID"]);

            DBController dbController = new DBController();
            int InsertionVerdict = dbController.InsertBlog(Blog.BlogTitle, ID, 1, Blog.BlogContent);

            return RedirectToAction("ViewAllBlogs");
        }

        public ActionResult InsertBlog()
        {
            int ID = Convert.ToInt16(Session["ID"]);

            if (ID == 0)
            {
                return RedirectToAction("MustSignIn");
            }

            return View();
        }

        public ActionResult DeleteBlog(int BlogId)
        {
            DBController db = new DBController();
            db.DeleteBlog(BlogId);

            return RedirectToAction("ViewAllBlogs");
        }

        public ActionResult EmptyBlogs()
        {
            return View();
        }

        public ActionResult MustSignIn()
        {
            return View();
        }
    }
}