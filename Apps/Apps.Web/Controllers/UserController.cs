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
        
        [Authorize]
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
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View(); 
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(UserModel userModel)
        {
            if (!string.IsNullOrEmpty(userModel.CodeUser) && !string.IsNullOrEmpty(userModel.Password))
            {                
                var userEntity = helperSession.mapping.CreateMapper().Map<UserModel, EUser>(userModel);
                var exit = userBussines.Login(userEntity);
                if (exit)
                {
                    userModel = Select(userModel);
                    userModel.CompaniesModel = GetCompanies(userModel);

                    if (userModel.CompaniesModel.Count == 0)
                    {
                        ModelState.AddModelError("", "Credenciales correctas, pero el usuario no tiene empresas configuradas.");                        
                        return View(userModel);
                    }

                    helperSession.User = userModel;
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
        [AllowAnonymous]
        public ActionResult Insert()
        {
            var userModel = new UserModel();
            userModel.CompaniesModel = GetCompaniesUpdate(userModel);
            return View(userModel);
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Insert(UserModel userModel)
        {
            try
            {
                userModel.State = 1;
                var userEntity = helperSession.mapping.CreateMapper().Map<UserModel, EUser>(userModel);
                var codeCompany = "00";
                var userRegister = "Admin";                
                if (ModelState.IsValid)
                {
                    if (helperSession.Company != null)
                        codeCompany = helperSession.Company.CodeCompany;
                    if (helperSession.User != null)
                        userRegister = helperSession.User.CodeUser;
                    userEntity.Audit.CodeCompany = codeCompany;
                    userEntity.Audit.UserRegister = userRegister;
                    using (TransactionScope ts = new TransactionScope())
                    {
                        userBussines.Insert(userEntity);
                        UpdateCompanies(userModel);
                        ts.Complete();
                    }
                    return RedirectToAction("Index");
                }
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            //retornar las compañias ,para que vuelva a editar
            userModel.CompaniesModel = GetCompaniesUpdate(userModel.CompaniesSplit);
            return View(userModel);
        }

        [HttpGet]
        [Authorize]
        public ActionResult Delete(string codeUser)
        {
            var userModel = Select(new UserModel() { CodeUser = codeUser });
            userModel.CompaniesModel = GetCompanies(userModel);
            ViewBag.IsDelete = true;
            return View(userModel);
        }

        [HttpPost]
        [Authorize]
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
                using (TransactionScope ts = new TransactionScope())
                {
                    userBussines.Delete(userEntity);
                    ts.Complete();
                }                
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }

            ViewBag.IsDelete = true;
            userModel.CompaniesModel = GetCompanies(userModel);
            return View(userModel);
        }

        public UserModel Select(UserModel userModel)
        {            
            var userEntity = helperSession.mapping.CreateMapper().Map<UserModel, EUser>(userModel);
            userEntity = userBussines.Select(userEntity);
            userModel = helperSession.mapping.CreateMapper().Map<EUser, UserModel>(userEntity);
            return userModel;
        }

        [HttpGet]
        [Authorize]
        public ActionResult Update(string codeUser)
        {
            var userModel = Select(new UserModel() { CodeUser = codeUser });
            userModel.CompaniesModel = GetCompaniesUpdate(userModel);
            return View(userModel);
        }

        [HttpPost]
        [Authorize]
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

            userModel.CompaniesModel = GetCompaniesUpdate(userModel.CompaniesSplit);
            return View(userModel);
        }

        public List<CompanyModel> GetCompanies(UserModel userModel)
        {
            var companyController = new CompanyController();
            var companies = new List<CompanyModel>();
            companies = userModel.CompaniesModel;
            if (companies == null || companies.Count == 0)
                companies = companyController.GetCompanies(userModel);

            foreach (var company in companies)
            {
                company.CheckBox = true;
                company.ShortName = string.Format("{0} - {1}",company.CodeCompany,company.ShortName);
            }
            return companies;
        }

        public List<CompanyModel> GetCompaniesUpdate(UserModel userModel)
        {
            var companyController = new CompanyController();            
            var companies = companyController.List();
            var companiesUser = GetCompanies(userModel);
            foreach (var company in companies)
            {
                if(companiesUser.Exists(item => item.CodeCompany == company.CodeCompany))
                    company.CheckBox = true;
                else
                    company.CheckBox = false;
                company.ShortName = string.Format("{0} - {1}", company.CodeCompany, company.ShortName);
            }
            return companies;
        }

        public List<CompanyModel> GetCompaniesUpdate(string CompaniesSplit)
        {
            var companyController = new CompanyController();
            var companies = companyController.List();
            var companiesSplit = new List<string>();
            if(!string.IsNullOrEmpty(CompaniesSplit))
                companiesSplit = CompaniesSplit.Split(
                    separator: new char[] { ',' },
                    options: StringSplitOptions.RemoveEmptyEntries).ToList();

            foreach (var company in companies)
            {
                if (companiesSplit.Exists(item => item == company.CodeCompany))
                    company.CheckBox = true;
                else
                    company.CheckBox = false;
                company.ShortName = string.Format("{0} - {1}", company.CodeCompany, company.ShortName);
            }
            return companies;
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

        [HttpPost]
        [Authorize]
        public ActionResult Close()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}