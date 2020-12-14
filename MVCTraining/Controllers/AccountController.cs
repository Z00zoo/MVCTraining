using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using MVCTraining.Models;
using HongHwa;
using NLog;
using System.Security.Claims;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MVCTraining.Controllers
{
    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private ApplicationRoleManager _roleManager;
        private static Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager, ApplicationRoleManager roleManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
            RoleManager = roleManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public ApplicationRoleManager RoleManager
        {
            get 
            {
                return _roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }

        // GET: Account
        [AllowAnonymous]
        public ActionResult Login(string returnUrl, string verifykey)
        {
            //宣告
            HongHwa.Sso sso = new HongHwa.Sso();

            //傳入此網頁的System.Web.HttpContextBase型別
            sso.AllowCrossReferrer(this.HttpContext);

            if (string.IsNullOrWhiteSpace(verifykey))
            {
                Uri uri = HongHwa.Settings.SsoWebDevelop;
                string url = uri.AbsoluteUri;

                ViewBag.SSOUrl = url;

                return View("SSORedirect");
            }
            else
            {
                //設定取得Sso認證後的帳號
                string account;

                //呼叫Sso並取得認證結果
                //請視環境選用相對應的SSO Uri,此處範例為正式環境，故選用HongHwa.Settings.SsoWeb
                if (sso.Authorize(HongHwa.Settings.SsoWebDevelop, verifykey, out account))
                {
                    //認證成功
                    logger.Info($"sso.Authorize is true, account = {account}");

                    //有取得帳號名稱 前往View 把帳號Post然後做非同步處理
                    if (!string.IsNullOrWhiteSpace(account))
                    {
                        logger.Info("go to SSOLogin");

                        SSOViewModel model = new SSOViewModel
                        {
                            Id = account
                        };

                        return View("SSOLogin", model);
                    }
                    else
                    {
                        logger.Info("SSO fail");
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    logger.Info("SSO fail");
                    return RedirectToAction("Index", "Home");
                }
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(SSOViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index", "Home");
            }

            logger.Info($"SSO接回後Post進來 Id = {model.Id}");

            var user = new ApplicationUser { Id = model.Id, UserName = model.Id };

            //沒帳號直接登入會跳錯
            if (UserManager.FindById(model.Id) != null)
            {
                SignInManager.SignIn(user, isPersistent: false, rememberBrowser: false);

                logger.Info($"燈入成功");
            }

            if (UserManager.FindById(model.Id) == null)
            {
                var createResult = await UserManager.CreateAsync(user);

                foreach (var error in createResult.Errors)
                {
                    logger.Debug($"ERROR = {error}");
                }

                if (createResult.Succeeded)
                {
                    logger.Info($"無帳號 建立帳號成功");

                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    //角色存在判斷
                    var roleName = "一般使用者";

                    if (RoleManager.RoleExists(roleName) == false)
                    {
                        var role = new IdentityRole(roleName);
                        await RoleManager.CreateAsync(role);
                    }

                    //將使用者加入角色
                    await UserManager.AddToRoleAsync(user.Id, roleName);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    logger.Info($"無帳號 建立帳號失敗 嘗試登入");
                }
            }

            return RedirectToAction("Index", "Home");
        }


        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    // 如需如何進行帳戶確認及密碼重設的詳細資訊，請前往 https://go.microsoft.com/fwlink/?LinkID=320771
                    // 傳送包含此連結的電子郵件
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "確認您的帳戶", "請按一下此連結確認您的帳戶 <a href=\"" + callbackUrl + "\">這裏</a>");

                    return RedirectToAction("Index", "Home");
                }
                AddErrors(result);
            }

            // 如果執行到這裡，發生某項失敗，則重新顯示表單
            return View(model);
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return Redirect(HongHwa.Settings.SsoWebDevelopLogout.AbsoluteUri);
        }

        #region Helper
        // 新增外部登入時用來當做 XSRF 保護
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}