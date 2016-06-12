using SD.BusinessLayer.Builders;
using SD.Commons.Shared.Models;
using SD_Ambience.Controllers.Secured;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Threading.Tasks;
using SD.BusinessLayer.Logging;
using SD.Commons.Shared;
using SD.Data.Entities.Entities;
using SD.Security.Utils;
using Microsoft.AspNet.Identity.EntityFramework;
using SD.Security.Login;

namespace SD_Ambience.Controllers
{
    /// <summary>
    /// controllerclass UserController
    /// </summary>
    public class UserController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public UserController() { }

        public UserController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
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
        
        /// <summary>
        /// Sessions the expired.
        /// </summary>
        /// <returns></returns>
        public ActionResult SessionExpired()
        {
            return PartialView("_SessionExpired");
        }

        /// <summary>
        /// Returns a list of Users.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "SystemAdministrator")]
        public ActionResult ListView()
        {
            if (new UserBuilder().IsAdministrator(HttpContext.User.Identity.Name))
            {
                return View(new UserBuilder().GetList(UserRoles.All));
            }
            return View();
        }
        
        /// <summary>
        /// Gets the entity to delete.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "SystemAdministrator")]
        public ActionResult Delete(string userId)
        {
            if (!string.IsNullOrEmpty(userId))
            {
                return View(new UserBuilder().GetById(userId));
            }
            else
            {
                return View();
            }
        }
        
        /// <summary>
        /// Deletes the entity.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SystemAdministrator")]
        public ActionResult Delete(UserViewModel model)
        {
            var user = new UserBuilder().GetById(model.UserId);
            if (model != null && new UserBuilder().Delete(user.Id))
            {
                return RedirectToAction("ListView", "User");
            }
            else
            {
                return View();
            }
        }
        
        /// <summary>
        /// Gets the login page.
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        /// <summary>
        /// Login.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
             //&& new SDSecurity().Login(model.Email, model.Password)
            if (model != null)
            {
                var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
                switch (result)
                {
                    case SignInStatus.Success:
                        if (new UserBuilder().IsTeacher(model.Email))
                            new UserBuilder().InsertLastLoginTeacher(model.Email);
                        if (new UserBuilder().IsAdministrator(model.Email))
                            new UserBuilder().InsertLastLoginAdministrator(model.Email);
                        return RedirectToLocal(returnUrl);
                    case SignInStatus.LockedOut:
                        return View("Lockout");
                    case SignInStatus.RequiresVerification:
                        return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                    case SignInStatus.Failure:
                    default:
                        ModelState.AddModelError("", "Invalid login attempt.");
                        return View(model);
                }

            }
            else
            {
                ModelState.AddModelError("", "Invalid login attempt.");
                return View(model);
            }
        }

        /// <summary>
        /// Register.
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        /// <summary>
        /// Register post.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {   
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser() { UserName = model.Email, PasswordHash = UserManager.PasswordHasher.HashPassword(model.Password), Email = model.Email, PhoneNumber = model.PhoneNumber };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded && new TeacherBuilder().Insert(model, user, model.Firstname, model.Lastname))
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    return RedirectToAction("Menu", "Dashboard");
                                      
                }
                AddErrors(result);
            }
            return View(model);
        }

        /// <summary>
        /// RegisterAdministrator.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "SystemAdministrator")]
        public ActionResult RegisterAdministrator()
        {
            return View();
        }

        /// <summary>
        /// RegisterAdministrator post.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "SystemAdministrator")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RegisterAdministrator(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, PasswordHash = UserManager.PasswordHasher.HashPassword(model.Password),Email = model.Email, PhoneNumber = model.PhoneNumber };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded && new UserBuilder().InsertAdministrator(model, user.Id))
                {
                    return RedirectToAction("Menu", "Dashboard");
                }
                AddErrors(result);
            }
            return View(model);
        }

        /// <summary>
        /// Details.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Details(string email)
        {
            return View(new UserBuilder().GetAdministratorByEmail(email));
        }

        /// <summary>
        /// Signs out.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignOut()
        {
            try
            {
                AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                Session.Abandon();
                return RedirectToAction("Login");
            }
            catch (Exception ex)
            {
                Logger.Write(ex);
            }
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
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
            return RedirectToAction("Menu", "Dashboard");
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