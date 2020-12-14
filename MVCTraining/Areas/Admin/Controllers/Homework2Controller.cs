using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCTraining.Models;
using MVCTraining.Service;
using MVCTraining.Repositories;
using MVCTraining.ViewModels;
using NLog;
using MVCTraining.Enums;
using MVCTraining.Filers;
using System.Net;

namespace MVCTraining.Areas.Admin.Controllers
{
    [AuthorizePlus(IsAdminOnly=true, RejectedToUrl = "/Home")]
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

        // GET: Admin/Homework2
        public ActionResult Index()
        {
            var model = new TrackSpendingViewModel2()
            {
                CategoryList = new SelectList(enumList.GetCategoryyyList(), "value", "name")
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AjaxAction(TrackSpendingViewModel2 viewModel2)
        {
            if (ModelState.IsValid)
            {
                if (viewModel2.Id == Guid.Empty)
                {
                    //create
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
                else
                {
                    //edit
                    _trackSpendingService.Edit(viewModel2);
                    _trackSpendingService.Save();

                    return View("ForIndexChild", _trackSpendingService.GetAll());
                }
            }

            return View("ForIndexChild");
        }

        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var result = _trackSpendingService.GetSingle(id);
            result.CategoryList = new SelectList(enumList.GetCategoryyyList(), "value", "name");

            return View("Index", result);
        }

        [ChildActionOnly]
        public ActionResult ForIndexChild()
        {
            return View(_trackSpendingService.GetAll());
        }
    }
}