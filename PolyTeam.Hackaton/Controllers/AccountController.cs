using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using BusinessLogic.Domain;
using PolyTeam.Hackaton.Models;
using BusinessLogic.Core;
//using BusinessLogic.Domain;

namespace PolyTeam.Hackaton.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserProcessor userProcessor;
        private readonly ICryptoProvider cryptoProvider;

        public AccountController(IUserProcessor userProcessor, ICryptoProvider cryptoProvider)
        {
            this.userProcessor = userProcessor;
            this.cryptoProvider = cryptoProvider;
        }
        //
        // GET: /Account/LogOn

        /*public ActionResult LogOn()
        {
            return View();
        }*/

        //
        // POST: /Account/LogOn

        [HttpPost]
        public ActionResult LogOn(LogOnModel model)
        {
            if (ModelState.IsValid)
            {
                if (this.userProcessor.LogOn(model.UserName, model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                    return RedirectToAction("Index","Request");
                }
                else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "The user name or password provided is incorrect.");
                }

            // If we got this far, something failed, redisplay form
            return RedirectToAction("Index", "Home", model);
        }

        //
        // GET: /Account/LogOff

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Index");
        }

        //
        // GET: /Account/Register

        public ActionResult Register()
        {
            return View(new RegisterModel());
        }

        //
        // POST: /Account/Register

        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    LastName = model.LastName,
                    FirstName = model.FirstName,
                    SecondName = model.SecondName,
                    Login = model.Login,
                    Hash = cryptoProvider.EncryptString(model.Password),
                    RoleId = 2,
                    Street = model.UserStreet,
                    House = model.UserHouse,
                    Flat = Convert.ToInt32(model.UserFlat),
                    Party = model.UserParty,
                    PhoneNumber = model.UserPhoneNumber
                };
                if (this.userProcessor.CreateUser(user))
                {
                    this.userProcessor.LogOn(model.Login, model.Password);
                    var CurUser = this.userProcessor.GetUserByName(model.Login);
                    return this.RedirectToAction("Index", "Index");
                }
            }

            ModelState.AddModelError(string.Empty, "Wrong registration data");
            return this.View(model);
        }

        #region Status Codes
        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
        #endregion
    }
}
