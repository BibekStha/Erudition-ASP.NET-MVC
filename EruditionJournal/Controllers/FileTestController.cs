using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Web;
using System.Web.Mvc;

namespace EruditionJournal.Controllers
{
    public class FileTestController : Controller
    {
        // GET: FileTest
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(HttpPostedFileBase file)
        {
            Debug.WriteLine(file.ContentLength);
            return View();
        }
    }
}