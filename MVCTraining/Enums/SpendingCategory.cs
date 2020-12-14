using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCTraining.Enums
{
    public enum CategoryyyEnum
    {
        [Display(Name = "支出")]
        支出 = 1,
        [Display(Name = "收入")]
        收入 = 2
    }

    public class EnumList
    {
        public IEnumerable<dynamic> GetCategoryyyList()
        {
            var categoryList = Enum.GetValues(typeof(CategoryyyEnum)).Cast<CategoryyyEnum>()
                .Select(x => new
                {
                    name = x.ToString(),
                    value = (int)x
                }).ToList();

            return categoryList;
        }
    }
}