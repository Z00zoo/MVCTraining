using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCTraining.Filers
{
    public class AuthorizePlusAttribute : AuthorizeAttribute
    {
        public bool IsAdminOnly = false;
        public String RejectedToUrl { get; set; }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            //維持 ASP.NET MVC 原有的驗證
            base.OnAuthorization(filterContext);

            //支援 MVC5 新增的 AllowAnonymous
            var skipAuthorization =
                filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), inherit: true) ||
                filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute),
                    inherit: true);

            //有設定 AllowAnonymous 就跳過
            if (skipAuthorization)
            {
                return;
            }

            //非管理者進入回到首頁
            if (IsAdminOnly)
            {
                if (filterContext.HttpContext.User.Identity.IsAuthenticated)
                {
                    if (!filterContext.HttpContext.User.IsInRole("管理者"))
                    {
                        //filterContext.Result = new RedirectResult(RejectedToUrl);

                        HttpRequestBase httpRequestBase;

                        var view = new ViewResult();
                        view.ViewName = "~/Views/Account/ShowAlert.cshtml";
                        view.ViewBag.Msg = "限定管理者使用！";
                        view.ViewBag.Url = RejectedToUrl;

                        filterContext.Result = view;
                    }
                }
            }

        }
    }
}