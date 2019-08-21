using OnlineStore.Domain.Abstract;
using OnlineStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace OnlineStore.Controllers
{
   
    [Authorize]
    public class AccountController : Controller
    {
          IAuthentication authenetication;

        public AccountController(IAuthentication authenetications)
        {
            this.authenetication = authenetications;
        }
        // GET: Accou
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (authenetication.Authenticate(model.UserName,model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, false);
                    return Redirect(returnUrl ?? Url.Action("Index", "Admin"));
                }
                else
                {
                    ModelState.AddModelError("", "Incorrect Username Or password");
                    return View();
                }
            }
            else { return View(); }
           
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Admin");
        }
    }
}