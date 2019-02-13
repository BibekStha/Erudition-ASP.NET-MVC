using System.Linq;
using System.Web.Mvc;
using EruditionJournal.Models;
using EruditionJournal.DAL;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Net;

namespace EruditionJournal.Controllers
{
    public class PublisherController : Controller
    {
        // Variable to hold our database
        private PublicationContext db = new PublicationContext();

        // GET: Publisher
        public ActionResult Index()
        {
            return View(db.Publishers.ToList().OrderByDescending(s => s.PublisherFName));
        }

        // GET: Publisher/Create
        public ActionResult Create()
        {
            return View();
        }

        //POST: Publisher/Create
        [HttpPost]
        public ActionResult Create(string PublisherFName, string PublisherLName, string PublisherDisplayName)
        {
            string query = "insert into Publisher (PublisherFName, PublisherLName, PublisherDisplayName)" +
                "values (@fname, @lname, @dispname)";

            SqlParameter[] myparams = new SqlParameter[3];
            myparams[0] = new SqlParameter("@fname", PublisherFName);
            myparams[1] = new SqlParameter("@lname", PublisherLName);
            myparams[2] = new SqlParameter("@dispname", PublisherDisplayName);

            db.Database.ExecuteSqlCommand(query, myparams);
            Debug.WriteLine(query);

            return RedirectToAction("Index");
        }

        //GET: Publisher/Detail/[id]
        public ActionResult Detail(int id)
        {
            string query = "select * from Publisher where PublisherId=@id";

            return View(db.Publishers.SqlQuery(query, new SqlParameter("@id", id)).FirstOrDefault());
        }

        //GET: Publisher/Edit/[id]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Publisher publisher = db.Publishers.Find(id);
            if (publisher == null)
            {
                return HttpNotFound();
            }
            return View(publisher);
        }

        //POST: Publisher/Edit/[id]
        [HttpPost]
        public ActionResult Edit(int? id, string PublisherFName, string PublisherLName, string PublisherDisplayName)
        {
            if (id == null || db.Publishers.Find(id) == null)
            {
                return HttpNotFound();
            }

            string query = "update Publisher set PublisherFName = @fname, " +
                "PublisherLName = @lname, PublisherDisplayName = @displayname where PublisherId = @id";
            SqlParameter[] myparams = new SqlParameter[4];
            myparams[0] = new SqlParameter("@fname", PublisherFName);
            myparams[1] = new SqlParameter("@lname", PublisherLName);
            myparams[2] = new SqlParameter("@displayname", PublisherDisplayName);
            myparams[3] = new SqlParameter("@id", id);

            db.Database.ExecuteSqlCommand(query, myparams);

            return RedirectToAction("Detail/" + id);
        }

        //GET: Publisher/Delete/[id]
        public ActionResult Delete(int? id)
        {
            Publisher publisher = db.Publishers.Find(id);
            if (id == null || publisher == null)
            {
                return HttpNotFound();
            }
            return View(publisher);
        }

        //POST: Publisher/Del/[id]
        //This method actually deletes
        [HttpPost]
        public ActionResult Del(int? id)
        {
            Publisher publisher = db.Publishers.Find(id);
            if (id == null || publisher == null)
            {
                return HttpNotFound();
            }

            string query = "delete from Publisher where PublisherId =" + id;
            db.Database.ExecuteSqlCommand(query);

            return RedirectToAction("Index");
        }
    }
}