using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCTraining.Filers
{
    public sealed class DateCheckAttribute : ValidationAttribute, IClientValidatable
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            //不判斷必填
            if (value == null)
            {
                return ValidationResult.Success;
            }

            try
            {
                var input = DateTime.Parse(value.ToString());

                if (DateTime.Today < input.Date)
                {
                    return new ValidationResult("輸入日期不得大於今日");
                }
            }
            catch 
            {
                return new ValidationResult("日期格式錯誤");
            }

            return ValidationResult.Success;
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(
            ModelMetadata metadata, ControllerContext context)
        {
            ModelClientValidationRule rule = new ModelClientValidationRule
            {
                ValidationType = "datecheck",
                ErrorMessage = FormatErrorMessage(metadata.GetDisplayName())
            };
            yield return rule;
        }
    }
}