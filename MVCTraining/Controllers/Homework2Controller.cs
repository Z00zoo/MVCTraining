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
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(TrackSpendingViewModel trackSpendingViewModel)
        {
            if (ModelState.IsValid)
            {
                AccountBook accountBook = new AccountBook()
                {
                    Amounttt = trackSpendingViewModel.TrackSpending.Money,
                    Categoryyy = trackSpendingViewModel.TrackSpending.Category,
                    Dateee = trackSpendingViewModel.TrackSpending.Date,
                    Remarkkk = trackSpendingViewModel.TrackSpending.Description
                };
                _trackSpendingService.Add(accountBook);
                _trackSpendingService.Save();

                return RedirectToAction("Index");
            }
            return View(trackSpendingViewModel);
        }

        public ActionResult ForIndexChild()
        {
            return View(_trackSpendingService.GetAll());
        }
    }
}