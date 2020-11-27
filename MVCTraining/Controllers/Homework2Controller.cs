using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCTraining.Models;
using MVCTraining.Service;
using MVCTraining.Repositories;
using MVCTraining.ViewModels;

namespace MVCTraining.Controllers
{
    public class Homework2Controller : Controller
    {
        private readonly TrackSpendingService _trackSpendingService;

        public Homework2Controller()
        {
            var unitOfWork = new EFUnitOfWork();
            _trackSpendingService = new TrackSpendingService(unitOfWork);
        }

        // GET: Homework2
        public ActionResult Index()
        {
            var categoryList = new List<Category>()
            {
                new Category{ name = "支出", value = 1 },
                new Category{ name = "收入", value = 2 }
            };
            var resault = new TrackSpendingViewModel2()
            {
                CategoryList = new SelectList(categoryList, "value", "name")
            };
            return View(resault);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "Amounttt, Categoryyy, Dateee, Remarkkk")] AccountBook accountBook)
        {
            if (ModelState.IsValid)
            {
                _trackSpendingService.Add(accountBook);
                _trackSpendingService.Save();

                return RedirectToAction("Index");
            }
            var categoryList = new List<Category>()
            {
                new Category{ name = "支出", value = 1 },
                new Category{ name = "收入", value = 2 }
            };
            var resault = new TrackSpendingViewModel2()
            {
                Amounttt = accountBook.Amounttt,
                Categoryyy = accountBook.Categoryyy,
                Dateee = accountBook.Dateee,
                Remarkkk = accountBook.Remarkkk,
                CategoryList = new SelectList(categoryList, "value", "name")
            };
            return View(resault);
        }

        public ActionResult ForIndexChild()
        {
            return View(_trackSpendingService.GetAll());
        }
    }
}