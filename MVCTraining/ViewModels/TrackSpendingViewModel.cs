using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using MVCTraining.Models;

namespace MVCTraining.ViewModels
{
    public class TrackSpendingViewModel
    {
        //應該要將原本model內容放一樣的過來，這邊偷懶Controller或Service就要多寫
        public TrackSpending TrackSpending { get; set; }
        public List<TrackSpending> List { get; set; }
        public string ListJSON { get; set; }
    }
}