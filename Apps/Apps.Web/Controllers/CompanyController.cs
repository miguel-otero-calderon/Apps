using Apps.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Apps.Entity;

namespace Apps.Web.Controllers
{
    [Authorize]
    public class CompanyController : Controller
    {
        HelperSession helperSession = new HelperSession();
        [HttpPost]
        public ActionResult Choose()
        {
            UserModel userModel = helperSession.User;
            int companiesCount = userModel.Companies.Count;

            if(companiesCount == 0)
                throw new Exception("El usuario no tiene empresas configuradas.");

            if (companiesCount == 1)
            {
                helperSession.Company = userModel.Companies.FirstOrDefault();
                return RedirectToAction("Index", "Home");
            }                
            else
                return View(userModel);
        }
    }
}