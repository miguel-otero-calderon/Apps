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

        public ActionResult List()
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
                    return RedirectToAction("Selected", "Company");
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
                    userEntity.Audit = new EAudit(CodeCompany: "0", CodeEntity: "", Code: "");
                    userEntity.Audit.UserRegister = helperSession.User.CodeUser;
                    userBussines.Insert(userEntity);
                    return RedirectToAction("List");
                }
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }

            return View();
        }

        [HttpGet]
        public ActionResult Delete()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Delete(UserModel userModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var userEntity = helperSession.mapping.CreateMapper().Map<UserModel, EUser>(userModel);
                    userEntity.Audit = new EAudit(CodeCompany: "0", CodeEntity: "", Code: "");
                    userEntity.Audit.UserRegister = helperSession.User.CodeUser;
                    userBussines.Delete(userEntity);
                    return RedirectToAction("List");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }

            return View();
        }
        
        protected UserModel Select(UserModel userModel)
        {
            var userCompanyBussines = new BUserCompany();
            var userEntity = helperSession.mapping.CreateMapper().Map<UserModel, EUser>(userModel);
            var companiesEntity = userCompanyBussines.SelectByUser(userEntity);
            userModel = helperSession.mapping.CreateMapper().Map<EUser, UserModel>(userEntity);
            foreach(var companyEntity in companiesEntity)
            {
                var companyModel = helperSession.mapping.CreateMapper().Map<ECompany, CompanyModel>(companyEntity);
                userModel.Companies.Add(companyModel);
            }            
            return userModel;
        }
    }
}