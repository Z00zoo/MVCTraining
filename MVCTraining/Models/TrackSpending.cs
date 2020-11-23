using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCTraining.Models
{
    public class TrackSpending
    {
        public string Category { get; set; }
        public int Money { get; set; }
        public string Date { get; set; }
        public string Description { get; set; }
    }
}