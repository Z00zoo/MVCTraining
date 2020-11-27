using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCTraining.ViewModels
{
    public class TrackSpendingViewModel2
    {
        public Guid Id { get; set; }

        public int Categoryyy { get; set; }

        public int Amounttt { get; set; }

        public DateTime Dateee { get; set; }

        public string Remarkkk { get; set; }

        public SelectList CategoryList { get; set; }
    }
}