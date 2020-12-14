using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCTraining.Models;
using MVCTraining.Service;
using MVCTraining.Repositories;
using MVCTraining.ViewModels;
using PagedList;
using NLog;
using MVCTraining.Enums;

namespace MVCTraining.Controllers
{
    public class Homework2Controller : Controller
    {
        private readonly TrackSpendingService _trackSpendingService;
        private static Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private readonly EnumList enumList = new EnumList();

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
                CategoryList = new SelectList(enumList.GetCategoryyyList(), "value", "name")
            };
            return View(model);
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        ////public ActionResult Index([Bind(Include = "Amounttt, Categoryyy, Dateee, Remarkkk")] AccountBook accountBook)
        ////上面寫法會不會通過寫在TrackSpendingViewModel2的自訂ValidationAttribute
        //public ActionResult Index(TrackSpendingViewModel2 viewModel2)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var accountBook = new AccountBook()
        //        {
        //            Amounttt = viewModel2.Amounttt,
        //            Categoryyy = viewModel2.Categoryyy,
        //            Dateee = viewModel2.Dateee,
        //            Remarkkk = viewModel2.Remarkkk
        //        };

        //        _trackSpendingService.Add(accountBook);
        //        _trackSpendingService.Save();

        //        return RedirectToAction("Index");
        //    }

        //    viewModel2.CategoryList = new SelectList(enumList.GetCategoryyyList(), "value", "name");

        //    return View(viewModel2);
        //}

        [HttpPost]
        [Authorize]
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

                return View("ForIndexChild", _trackSpendingService.GetAll());
            }

            return View("ForIndexChild");
        }

        [ChildActionOnly]
        public ActionResult ForIndexChild()
        {
            return View(_trackSpendingService.GetAll());
        }

        public ActionResult SSOLogin()
        {
            Uri uri = HongHwa.Settings.SsoWebDevelop;
            string url = uri.AbsoluteUri;

            return Redirect(url);
        }

        public ActionResult SSOLogout()
        {

            return Redirect(HongHwa.Settings.SsoWebDevelopLogout.AbsoluteUri);
        }

        
    }
}