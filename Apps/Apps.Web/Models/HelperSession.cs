using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Apps.Entity;
using Apps.Business;

namespace Apps.Web.Models
{
    public class HelperSession
    {
        public MapperConfiguration mapping
        {
            get
            {
                return new MapperConfiguration(
                cfg => {
                    cfg.CreateMap<UserModel, EUser>();
                    cfg.CreateMap<EUser, UserModel>();
                    cfg.CreateMap<CompanyModel, ECompany>();
                    cfg.CreateMap<ECompany, CompanyModel>();
                });
            }
        }
        public UserModel User
        {
            get
            {
                if (HttpContext.Current.Session["userSystem"] != null)
                    return ((UserModel)HttpContext.Current.Session["userSystem"]);
                else
                    return null;
            }
            set
            {
                HttpContext.Current.Session["userSystem"] = value;
            }
        }
        public CompanyModel Company
        {
            get
            {
                if (HttpContext.Current.Session["companySystem"] != null)
                    return ((CompanyModel)HttpContext.Current.Session["companySystem"]);
                else
                    return null;
            }
            set
            {
                HttpContext.Current.Session["companySystem"] = value;
            }
        }       
           
    }
}