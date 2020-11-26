using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCTraining.Models;

namespace MVCTraining.Controllers
{
    public class Homework2Controller : Controller
    {
        private TrackSpendingModel db = new TrackSpendingModel(); //建立連線

        // GET: Homework2
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "Id, Categoryyy, Amounttt, Dateee, Remarkkk")] AccountBook accountBook )
        {
            if (ModelState.IsValid)
            {
                accountBook.Id = Guid.NewGuid();
                db.AccountBook.Add(accountBook);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(accountBook);
        }

        public ActionResult ForIndexChild()
        {
            return View(db.AccountBook.OrderBy(x => x.Dateee).ToList());
        }
    }
}