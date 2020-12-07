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
            var model = new TrackSpendingViewModel2()
            {
                CategoryList = new SelectList(GetCategoryyyList(), "value", "name")
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Index([Bind(Include = "Amounttt, Categoryyy, Dateee, Remarkkk")] AccountBook accountBook)
        //上面寫法會不會通過寫在TrackSpendingViewModel2的自訂ValidationAttribute
        public ActionResult Index(TrackSpendingViewModel2 viewModel2)
        {
            if (ModelState.IsValid)
            {
                var accountBook = new AccountBook()
                {
                    Amounttt = viewModel2.Amounttt,
                    Categoryyy = viewModel2.Categoryyy,
                    Dateee = viewModel2.Dateee,
                    Remarkkk = viewModel2.Remarkkk
                };

                _trackSpendingService.Add(accountBook);
                _trackSpendingService.Save();

                return RedirectToAction("Index");
            }

            viewModel2.CategoryList = new SelectList(GetCategoryyyList(), "value", "name");

            return View(viewModel2);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AjaxAction(TrackSpendingViewModel2 viewModel2)
        {
            if (ModelState.IsValid)
            {
                var accountBook = new AccountBook()
                {
                    Amounttt = viewModel2.Amounttt,
                    Categoryyy = viewModel2.Categoryyy,
                    Dateee = viewModel2.Dateee,
                    Remarkkk = viewModel2.Remarkkk
                };

                _trackSpendingService.Add(accountBook);
                _trackSpendingService.Save();

                return View("ForIndexChild", _trackSpendingService.GetAll().Take(20));
            }

            return View("ForIndexChild");
        }

        [ChildActionOnly]
        public ActionResult ForIndexChild()
        {
            return View(_trackSpendingService.GetAll().Take(20));
        }

        private List<Category> GetCategoryyyList()
        {
            var categoryList = new List<Category>()
            {
                new Category{ name = "請選擇" },
                new Category{ name = "支出", value = "1" },
                new Category{ name = "收入", value = "2" }
            };

            return categoryList;
        }
    }
}