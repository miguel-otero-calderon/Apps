using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Apps.Web.Models;
using Apps.Business;
using Apps.Entity;
using System.Transactions;

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
                using (TransactionScope ts = new TransactionScope())
                {
                    userBussines.Update(userEntity);
                    UpdateCompanies(userModel);
                    ts.Complete();
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }

            return View(userModel);
        }

        [HttpGet]
        public ActionResult GetCompanies(string codeUser)
        {
            var userModel = new UserModel() {CodeUser = codeUser };
            var companies = (new CompanyController()).GetCompanies(userModel);
            var subtitle = string.Empty;
            if(companies.Count == 1)
                subtitle = string.Format("1 empresa asociada.");
            else
                subtitle = string.Format("{0} empresas asociadas.",companies.Count);
            foreach (var company in companies)
            {
                company.CheckBox = true;
                company.ShortName = string.Format("{0} - {1}",company.CodeCompany,company.ShortName);
            }
            ViewBag.Subtitle = subtitle;
            return PartialView(companies);
        }

        [HttpGet]
        public ActionResult SetCompanies(string codeUser)
        {
            var userModel = new UserModel() { CodeUser = codeUser };
            var companyController = new CompanyController();            
            var companies = companyController.List();
            var companiesUser = companyController.GetCompanies(userModel);
            var subtitle = string.Empty;
            if (companiesUser.Count == 1)
                subtitle = string.Format("1 empresa asociada.");
            else
                subtitle = string.Format("{0} empresas asociadas.", companiesUser.Count);
            foreach (var company in companies)
            {
                if(companiesUser.Exists(item => item.CodeCompany == company.CodeCompany))
                    company.CheckBox = true;
                else
                    company.CheckBox = false;
                company.ShortName = string.Format("{0} - {1}", company.CodeCompany, company.ShortName);
            }
            ViewBag.Subtitle = subtitle;
            return PartialView(companies);
        }

        public void UpdateCompanies(UserModel userModel)
        {
            var userCompanyBussines = new BUserCompany();
            var userEntity = new EUser() { CodeUser = userModel.CodeUser };
            var Companies = new List<string>();
            if (!string.IsNullOrEmpty(userModel.CompaniesSplit))
            {
                Companies = userModel.CompaniesSplit.Split(
                    separator: new char[] { ',' },
                    options: StringSplitOptions.RemoveEmptyEntries).ToList();
            }

            if(Companies.Count == 0)
            {
                if(userModel.CompaniesModel.Count > 0)
                {
                    foreach (var item in userModel.CompaniesModel)
                    {
                        Companies.Add(item.CodeCompany);
                    }
                }
            }

            userEntity.Companies = Companies;

            userCompanyBussines.UpdateByUser(userEntity);
        }
    }
}