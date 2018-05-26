using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using EShop.BLL.DTO;
using EShop.BLL.Interfaces;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using EShop.WEB.Models;
using EShop.DAL.Entities;
using EShop.Common;


namespace EShop.WEB.Controllers
{


    public class AccountController : Controller
    {
        // Standard asp.net classes to manage users.
        private readonly ApplicationUserManager _userManager;
        private readonly ApplicationSignInManager _signInManager;
        private readonly IAuthenticationManager _authenticationManager;

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager, IAuthenticationManager authenticationManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _authenticationManager = authenticationManager;
        }

        [AllowAnonymous]
        public ActionResult Register()
        {

            return View("_Register");
        }

        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterVM model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser {UserName=model.Email, Email = model.Email, FirstName = model.FirstName, LastName = model.LastName };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    // Comment the following line to prevent log in until the user is confirmed.
                    //await _signInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    var callbackUrl = await SendEmailConfirmationTokenAsync(user.Id, "Confirm your account");

                    //ViewBag.Message = "Check your email and confirm your account, you must be confirmed before you can log in.";
                    ViewBag.Message = "تم إنشاء حسابك بنجاح , وتم إرسال رسالة التحقق إلى بريدك الالكتروني";

                    return View("Info");

                    //return RedirectToAction("Index", "Home");
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View("_Register", model);
        }

        private async Task<string> SendEmailConfirmationTokenAsync(string userId, string subject)
        {
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(userId);
            // var callbackUrl = Url.Action(nameof(ConfirmEmail), "Account", new { userId = userId, code = code }, protocol: Request.Url.Scheme);
            var callbackUrl = Url.Action("ConfirmEmail", "Account",
   new { userId = userId, code = code }, protocol: Request.Url.Scheme);
            // var callbackUrl = "";
            string email = Utils.buildUserConfirmEmail(callbackUrl);
            
            await _userManager.SendEmailAsync(userId, subject,email);

            return callbackUrl;
        }

        // GET: /Account/ConfirmEmail
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            IdentityResult result;
            try
            {
                result = await _userManager.ConfirmEmailAsync(userId, code);
            }
            catch (InvalidOperationException ioe)
            {
                // ConfirmEmailAsync throws when the userId is not found.
                ViewBag.errorMessage = ioe.Message;
                return View("Error");
            }

            if (result.Succeeded)
            {
                return View();
            }
            // If we got this far, something failed.
            AddErrors(result);
            ViewBag.errorMessage = "ConfirmEmail failed";
            return View("Error");
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {

            ViewBag.ReturnUrl = returnUrl;

            // return View("_LoginPartial1");
            return View();
        }
        [AllowAnonymous]
        public ActionResult AdminLogin(string returnUrl)
        {

            ViewBag.ReturnUrl = returnUrl;

            // return View("_LoginPartial1");
            return View();
        }
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AdminLogin(LoginVm model, string returnUrl)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Uncomment to require the user to have a confirmed email before they can log on.
            var user = await _userManager.FindByNameAsync(model.Email);

            if (user != null)
            {
               
                if (!await _userManager.IsEmailConfirmedAsync(user.Id))
                {
                    string callbackUrl = await SendEmailConfirmationTokenAsync(user.Id, "Confirm your account-Resend");

                    ViewBag.ErrorMessage = "You must have a confirmed email to log on.";


                    return View("Error");

                }
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
            
            switch (result)
            {
                case SignInStatus.Success:
                {
                    var res = await _userManager.IsInRoleAsync(user.Id, "AdminRole");
                    if (res)
                    {
                        return RedirectToAction("Index", "ManageCategory");    
                    }
                    else
                    {
                      
                        ModelState.AddModelError("Email","يجب استخدام حساب المدير للدخول");
                        return View(model);
                    }
                    
                }

                case SignInStatus.LockedOut:
                   
                    return View("Lockout");
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("Password", "Invalid login attempt.");
                    return View(model);

            }
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginVm model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                //  return View(model);
                if (Request.IsAjaxRequest())
                { return Content("Invalid Login Attempts !"); }

                return View(model);


            }

            // Uncomment to require the user to have a confirmed email before they can log on.
            var user = await _userManager.FindByNameAsync(model.Email);
            if (user != null)
            {
                if (!await _userManager.IsEmailConfirmedAsync(user.Id))
                {
                    string callbackUrl = await SendEmailConfirmationTokenAsync(user.Id, "Confirm your account-Resend");

                    ViewBag.ErrorMessage = "You must have a confirmed email to log on.";

                    if (Request.IsAjaxRequest())
                    {
                        return Content("Invalid Login Attempts !");
                    }
                    else
                    {
                        return View("Error");
                    }

                }
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    if (User.IsInRole("AdminRole"))
                        return Content("يرجى تسجيل الدخول من صفحة المدير");
                    if (Request.IsAjaxRequest())
                        return Content(Url.Action("Index", "Home"));

                    return RedirectToLocal(returnUrl);

                case SignInStatus.LockedOut:
                    if (Request.IsAjaxRequest())
                        return Content("Your Account Locked");
                    return View("Lockout");
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");

                    if (Request.IsAjaxRequest())
                        return Content("Invaild Login Attempts");
                    return View(model);

            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            _authenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null || !(await _userManager.IsEmailConfirmedAsync(user.Id)))
                {
                    // Don't reveal that the user does not exist or is not confirmed.
                    return View("ForgotPasswordConfirmation");
                }

                string code = await _userManager.GeneratePasswordResetTokenAsync(user.Id);
                var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                await _userManager.SendEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");
                return RedirectToAction("ForgotPasswordConfirmation", "Account");
            }

            // If it reaches this, then something failed and we redisplay the form.
            return View(model);
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
        [AllowAnonymous]
        public ActionResult CheckExistingEmail(string email)
        {

            var user = _userManager.FindByEmail(email.ToLower());
            if (user == null)
                return Json(true, JsonRequestBehavior.AllowGet);

            return Json(false, JsonRequestBehavior.AllowGet);
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
            private const string XsrfKey = "CodePaste_$31!.2*#";
            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                //context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
                var owin = context.HttpContext.GetOwinContext();
                owin.Authentication.Challenge(properties, LoginProvider);
            }


        }
    }
}