using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCTraining.Filers;
using MVCTraining.Models;

namespace MVCTraining.ViewModels
{
    public class TrackSpendingViewModel2
    {
        public Guid Id { get; set; }

        [Required]
        public int Categoryyy { get; set; }

        [Required]
        [Range(1, Int32.MaxValue)]
        [RegularExpression("^\\d*$", ErrorMessage = "請輸入正整數")]
        public int Amounttt { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        [DateCheck(ErrorMessage = "日期不得大於今日")]
        public DateTime Dateee { get; set; }

        [Required]
        [StringLength(100)]
        public string Remarkkk { get; set; }

        public SelectList CategoryList { get; set; }

        public TrackSpendingViewModel2()
        {
            Dateee = DateTime.Today;
        }
    }
}