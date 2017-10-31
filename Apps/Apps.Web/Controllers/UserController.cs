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
        MapperConfiguration mapping = new MapperConfiguration(
            cfg => {
                cfg.CreateMap<UserModel, EUser>();
                cfg.CreateMap<EUser, UserModel>();
            });
        BUser bussinesLayer = new BUser();
        List<UserModel> usersModel = new List<UserModel>();

        public ActionResult List()
        {
            var usersEntity = bussinesLayer.List();
            foreach(var userEntity in usersEntity)
            {
                var userModel = mapping.CreateMapper().Map<EUser, UserModel>(userEntity);
                usersModel.Add(userModel);
            }

            return View(usersModel);
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View(); 
        }

        [HttpPost]
        public ActionResult Login(UserModel userModel)
        {           
            if (ModelState.IsValid)
            {                
                var userEntity = mapping.CreateMapper().Map<UserModel, EUser>(userModel);
                var exit = bussinesLayer.Login(userEntity);
                if (exit)
                {
                    FormsAuthentication.SetAuthCookie(userEntity.CodeUser, false);
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
            try
            {
                userModel.State = 1;
                if (ModelState.IsValid)
                {
                    var userEntity = mapping.CreateMapper().Map<UserModel, EUser>(userModel);
                    bussinesLayer.Insert(userEntity);
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