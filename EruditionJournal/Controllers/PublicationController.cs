using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EruditionJournal.Models;
using EruditionJournal.DAL;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Net;
using System.IO;
using EruditionJournal.Models.ViewModels;

namespace EruditionJournal.Controllers
{
    public class PublicationController : Controller
    {
        // Variable to hold our database
        private PublicationContext db = new PublicationContext();

        // GET: Publication
        public ActionResult Index()
        {
            return View(db.Publications.ToList());
        }

        // GET: Publication/Create
        public ActionResult Create()
        {
            PublicationEdit publicationEdit = new PublicationEdit
            {
                publishers = db.Publishers.ToList(),
                categories = db.Categories.ToList()
            };
            return View(publicationEdit);
        }

        // POST: Publication/Create
        [HttpPost]
        public ActionResult Create(string publicationTitle, int publicationPublisher,
            int publicationCategory, string publicationAbstract)
        {
            string query = "insert into Publication (PublicationTitle, PublicationAbstract, PublishedDate, HasManuscript, publisher_PublisherId, category_CategoryId)" +
                "Values (@title, @abstract, @date, @hasmanuscript, @publisher, @category)";

            int hasmanuscript = 0;

            SqlParameter[] myparams = new SqlParameter[6];
            myparams[0] = new SqlParameter("@title", publicationTitle);
            myparams[1] = new SqlParameter("@abstract", publicationAbstract);
            myparams[2] = new SqlParameter("@date", System.DateTime.Today);
            myparams[3] = new SqlParameter("@hasmanuscript", hasmanuscript);
            myparams[4] = new SqlParameter("@publisher", publicationPublisher);
            myparams[5] = new SqlParameter("@category", publicationCategory);

            db.Database.ExecuteSqlCommand(query, myparams);
            Debug.WriteLine(query);
            return RedirectToAction("Index");
        }

        // GET: Publication/Detail/[id]
        public ActionResult Detail(int? id)
        {
            Publication publication = db.Publications.Find(id);
            if (id == null || publication == null)
            {
                return HttpNotFound();
            }

            return View(publication);
        }

        // GET: Publication/Upload/[id]
        public ActionResult Upload(int? id)
        {
            Publication publication = db.Publications.Find(id);
            if (id == null || publication == null)
            {
                return HttpNotFound();
            }
            return View(publication);
        }

        //POST: Publication/Upload/[id]
        [HttpPost]
        public ActionResult Upload(int? id, HttpPostedFileBase manuscript)
        {
            Publication publication = db.Publications.Find(id);

            if (id == null || publication == null)
            {
                return HttpNotFound();
            }

            // Manuscript upload section
            var hasmanus = 0;
            try
            {
                if (manuscript.ContentLength > 0)
                {
                    var extension = Path.GetExtension(manuscript.FileName).Substring(1);
                    if (extension == "pdf")
                    {
                        // filename
                        string fn = id + "." + extension;

                        // file path
                        string path = Path.Combine(Server.MapPath("~/uploads/manuscripts"), fn);

                        // uploading the file in the server with name matching the id
                        manuscript.SaveAs(path);

                        hasmanus = 1;
                    }
                }

                if (hasmanus == 0)
                {
                    ViewBag.FileStatus = "The file extension is not supported.";
                    return View(publication);
                }
                ViewBag.FileStatus = "File was successfully uploaded.";
            } catch (Exception)
            {
                ViewBag.FileStatus = "Error occured while uploading file.";
            }
            

            string query = "update Publication set HasManuscript = 1 where PublicationId = " + id;
            db.Database.ExecuteSqlCommand(query);

            return RedirectToAction("Detail/" + id);
        }
    }
}

