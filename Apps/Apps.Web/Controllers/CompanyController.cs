using Apps.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Apps.Entity;
using Apps.Business;

namespace Apps.Web.Controllers
{
    [Authorize]
    public class CompanyController : Controller
    {
        HelperSession helperSession = new HelperSession();
        [HttpGet]
        public ActionResult Choose()
        {
            UserModel userModel = helperSession.User;
            int companiesCount = userModel.CompaniesModel.Count;

            if(companiesCount == 0)
                throw new Exception("El usuario no tiene empresas configuradas.");

            if (companiesCount == 1)
            {
                helperSession.Company = userModel.CompaniesModel[0];
                return RedirectToAction("Index", "Home");
            }                
            else
                return View(userModel);
        }
        public List<CompanyModel> GetCompanies(UserModel userModel)
        {
            var companiesModel = new List<CompanyModel>();
            var userCompanyBussines = new BUserCompany();
            var userEntity = helperSession.mapping.CreateMapper().Map<UserModel, EUser>(userModel);
            var companiesEntity = userCompanyBussines.SelectByUser(userEntity);
            userModel = helperSession.mapping.CreateMapper().Map<EUser, UserModel>(userEntity);
            foreach (var companyEntity in companiesEntity)
            {
                var companyModel = helperSession.mapping.CreateMapper().Map<ECompany, CompanyModel>(companyEntity);
                companiesModel.Add(companyModel);
            }
            return companiesModel;
        }

        public List<CompanyModel> List()
        {
            var companyBussines = new BCompany();
            var companiesModel = new List<CompanyModel>();            
            var companiesEntity = companyBussines.List();
            foreach (var companyEntity in companiesEntity)
            {
                var companyModel = helperSession.mapping.CreateMapper().Map<ECompany, CompanyModel>(companyEntity);
                companiesModel.Add(companyModel);
            }
            return companiesModel;
        }      
    }
}