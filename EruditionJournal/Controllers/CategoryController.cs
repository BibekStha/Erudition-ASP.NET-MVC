using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EruditionJournal.DAL;
using EruditionJournal.Models;
using System.Data.SqlClient;

namespace EruditionJournal.Controllers
{
    public class CategoryController : Controller
    {
        //Variable to hold our database
        private PublicationContext db = new PublicationContext();

        // GET: Category
        public ActionResult Index()
        {
            return View(db.Categories.ToList().OrderBy(s => s.CategoryName));
        }

        // GET: Category/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Category/Create
        [HttpPost]
        public ActionResult Create(string categoryName)
        {
            string query = "insert into Category (CategoryName)" +
                "Values (@name)";

            SqlParameter[] myparams = new SqlParameter[1];
            myparams[0] = new SqlParameter("@name", categoryName);

            db.Database.ExecuteSqlCommand(query, myparams);
            return RedirectToAction("Index");
        }

        // GET: Category/Detail/[id]
        public ActionResult Detail(int id)
        {
            string query = "select * from Category where CategoryId = @id";
            return View(db.Categories.SqlQuery(query, new SqlParameter("@id", id)).FirstOrDefault());
        }

        // GET: Category/Edit/[id]
        public ActionResult Edit(int? id)
        {
            Category category = db.Categories.Find(id);

            if (id == null || category == null)
            {
                return HttpNotFound();
            }

            return View(category);
        }

        // POST: Category/Edit/[id]
        [HttpPost]
        public ActionResult Edit(int? id, string CategoryName)
        {
            if (id == null || db.Categories.Find(id) == null)
            {
                return HttpNotFound();
            }

            string query = "update Category set CategoryName = @name where CategoryId = @id";

            SqlParameter[] myparams = new SqlParameter[2];
            myparams[0] = new SqlParameter("@name", CategoryName);
            myparams[1] = new SqlParameter("@id", id);

            db.Database.ExecuteSqlCommand(query, myparams);

            return RedirectToAction("Index");
        }

        // GET: Category/Delete/[id]
        public ActionResult Delete(int? id)
        {
            Category category = db.Categories.Find(id);

            if (id == null || category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Category/Del/[id]
        // This method actually deletes
        [HttpPost]
        public ActionResult Del(int? id)
        {
            Category category = db.Categories.Find(id);
            if (id == null || category == null)
            {
                return HttpNotFound();
            }

            string query = "delete from Category where CategoryId = " + id;
            db.Database.ExecuteSqlCommand(query);

            return RedirectToAction("Index");
        }
    }
}