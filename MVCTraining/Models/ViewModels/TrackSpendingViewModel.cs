using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCTraining.Models.ViewModels
{
    public class TrackSpendingViewModel
    {
        public TrackSpending TrackSpending { get; set; }
        public List<TrackSpending> List { get; set; }
        public string ListJSON { get; set; }
    }
}