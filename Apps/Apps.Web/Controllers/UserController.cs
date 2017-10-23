using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Apps.Web.Models;
using Apps.Business;
using Apps.Entity;

namespace Apps.Web.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View(); 
        }

        [HttpPost]
        public ActionResult Login(UserModel user)
        {
            if (ModelState.IsValid)
            {
                EUser eUser = user.GetUser();
                BUser bUser = new BUser();
                bool exit = bUser.Login(eUser);
                if (exit)
                {
                    FormsAuthentication.SetAuthCookie(eUser.CodeUser, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Credenciales incorrectas."); 
                }
            }
            return View(user);
        }
    }
}