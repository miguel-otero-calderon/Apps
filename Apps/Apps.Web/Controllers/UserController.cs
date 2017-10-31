using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Apps.Web.Models;
using Apps.Business;
using Apps.Entity;
using AutoMapper;

namespace Apps.Web.Controllers
{
    public class UserController : Controller
    {
        MapperConfiguration mapperConfiguration = new MapperConfiguration(
        cfg => {
            cfg.CreateMap<UserModel, EUser>();
            cfg.CreateMap<EUser, UserModel>();});

        public ActionResult Index()
        {
            BUser bUser = new BUser();
            List<EUser> list = new List<EUser>();
            List<UserModel> model = new List<UserModel>();
            UserModel userModel = new UserModel();

            list = bUser.List();
            foreach(var item in list)
            {
                userModel = mapperConfiguration.CreateMapper().Map<EUser, UserModel>(item);
                model.Add(userModel);
            }

            return View(model);
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View(); 
        }

        [HttpPost]
        public ActionResult Login(UserModel userModel)
        {
            EUser eUser = new EUser();
            BUser bUser = new BUser();
            bool exit = false;
            if (ModelState.IsValid)
            {                
                eUser = mapperConfiguration.CreateMapper().Map<UserModel, EUser>(userModel);
                exit = bUser.Login(eUser);
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
            return View(userModel);
        }

        [HttpGet]
        public ActionResult Insert()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Insert(UserModel userModel)
        {
            EUser eUser = new EUser();
            BUser bUser = new BUser();
            try
            {
                userModel.State = 1;
                if (ModelState.IsValid)
                {
                    eUser = mapperConfiguration.CreateMapper().Map<UserModel, EUser>(userModel);
                    bUser.Insert(eUser);
                    return RedirectToAction("List");
                }
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }

            return View();
        }
    }
}