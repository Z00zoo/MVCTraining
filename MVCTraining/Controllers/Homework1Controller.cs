using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCTraining.Models;
using MVCTraining.Models.ViewModels;
using Newtonsoft.Json;

namespace MVCTraining.Controllers
{
    public class Homework1Controller : Controller
    {
        // GET: Homework1
        public ActionResult Index()
        {
            //假資料
            var defaultList = new List<TrackSpending> 
            {
                new TrackSpending{ Category = "1" ,Money = 300, Date = "2016-01-01"},
                new TrackSpending{ Category = "1" ,Money = 16000, Date = "2016-01-02"},
                new TrackSpending{ Category = "1" ,Money = 8000, Date = "2016-01-03"}
            };

            var model = new TrackSpendingViewModel
            {
                List = defaultList,
                ListJSON = JsonConvert.SerializeObject(defaultList)
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult Index(TrackSpendingViewModel paraData)
        {
            var list = JsonConvert.DeserializeObject<List<TrackSpending>>(paraData.ListJSON);
            var track = new TrackSpending
            {
                Category = paraData.TrackSpending.Category,
                Money = paraData.TrackSpending.Money,
                Date = paraData.TrackSpending.Date,
                Description = paraData.TrackSpending.Description

            };
            list.Add(track);
            var model = new TrackSpendingViewModel
            {
                List = list,
                ListJSON = JsonConvert.SerializeObject(list)

            };
            return View(model);
        }
    }
}