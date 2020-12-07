using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCTraining.Enums
{
    public enum SpendingCategory
    {
        [Display(Name = "支出")]
        Expend = 1,
        [Display(Name = "收入")]
        Income = 2
    }
}