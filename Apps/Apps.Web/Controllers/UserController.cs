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
        BUser userBussines = new BUser();        
        List<UserModel> usersModel = new List<UserModel>();
        HelperSession helperSession = new HelperSession();

        public ActionResult Index()
        {
            var usersEntity = userBussines.List();
            foreach(var userEntity in usersEntity)
            {
                var userModel = helperSession.mapping.CreateMapper().Map<EUser, UserModel>(userEntity);
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
                var userEntity = helperSession.mapping.CreateMapper().Map<UserModel, EUser>(userModel);
                var exit = userBussines.Login(userEntity);
                if (exit)
                {
                    helperSession.User = Select(userModel);
                    FormsAuthentication.SetAuthCookie(userEntity.CodeUser, false);
                    return RedirectToAction("Choose", "Company");
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
                    var userEntity = helperSession.mapping.CreateMapper().Map<UserModel, EUser>(userModel);
                    var codeCompany = "00";
                    var userRegister = "Admin";
                    if (helperSession.Company != null)
                        codeCompany = helperSession.Company.CodeCompany;
                    if (helperSession.User != null)
                        userRegister = helperSession.User.CodeUser;
                    userEntity.Audit.CodeCompany = codeCompany;
                    userEntity.Audit.UserRegister = userRegister;
                    userBussines.Insert(userEntity);
                    return RedirectToAction("Index");
                }
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }

            return View();
        }

        [HttpGet]
        public ActionResult Delete(string codeUser)
        {
            var userModel = Select(new UserModel() { CodeUser = codeUser });
            return View(userModel);
        }

        [HttpPost]
        public ActionResult Delete(UserModel userModel)
        {
            try
            {
                var userEntity = helperSession.mapping.CreateMapper().Map<UserModel, EUser>(userModel);
                var codeCompany = "00";
                var userRegister = "Admin";
                if (helperSession.Company != null)
                    codeCompany = helperSession.Company.CodeCompany;
                if (helperSession.User != null)
                    userRegister = helperSession.User.CodeUser;
                userEntity.Audit.CodeCompany = codeCompany;
                userEntity.Audit.UserRegister = userRegister;
                userBussines.Delete(userEntity);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }

            return View();
        }

        public UserModel Select(UserModel userModel)
        {            
            var userEntity = helperSession.mapping.CreateMapper().Map<UserModel, EUser>(userModel);
            userEntity = userBussines.Select(userEntity);
            userModel = helperSession.mapping.CreateMapper().Map<EUser, UserModel>(userEntity);
            return userModel;
        }

        [HttpGet]
        public ActionResult Update(string codeUser)
        {
            var userModel = Select(new UserModel() { CodeUser = codeUser });
            var companies = (new CompanyController()).GetCompanies(userModel);
            return View(userModel);
        }

        [HttpPost]
        public ActionResult Update(UserModel userModel)
        {
            try
            {
                var userEntity = helperSession.mapping.CreateMapper().Map<UserModel, EUser>(userModel);
                var codeCompany = "00";
                var userRegister = "Admin";
                if (helperSession.Company != null)
                    codeCompany = helperSession.Company.CodeCompany;
                if (helperSession.User != null)
                    userRegister = helperSession.User.CodeUser;
                userEntity.Audit.CodeCompany = codeCompany;
                userEntity.Audit.UserRegister = userRegister;
                userBussines.Update(userEntity);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }

            return View();
        }



    }
}