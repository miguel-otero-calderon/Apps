using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using Apps.Web.SCI.Models;
using System.Configuration;

namespace Apps.Web.SCI.Controllers
{
    public class UtilitarioController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }        

        [HttpGet]
        public ActionResult UploadFile()
        {
            if (Request.IsAjaxRequest())
            {
                return PartialView("UploadFile");
            }
            else
            {
                return new HttpNotFoundResult();
            }
        }

        [HttpPost]
        public JsonResult UploadFile(HttpPostedFileBase file1, HttpPostedFileBase file2)
        {  
            Result result = null;
            Result resultShoppingCart = null;
            Result resultAuthorizeNet = null;
            Result resultValidate = null;
            try
            {
                if (UploadFileClient(file1) && UploadFileClient(file2))
                {
                    resultShoppingCart = ValidateShoppingCartFile(ShoppingCartFile:file1);

                    if (resultShoppingCart.Status)
                    {
                        resultAuthorizeNet = ValidateAuthorizeNetFile(AuthorizeNetFile: file2);
                        if (resultAuthorizeNet.Status)
                        {
                            resultValidate = Validate(resultShoppingCart.ShoppingCartList, resultAuthorizeNet.AuthorizeNetList);
                            result = resultValidate;
                        }
                        else
                        {
                            result = resultAuthorizeNet;
                            result.ShoppingCartList = resultShoppingCart.ShoppingCartList;
                        }
                    }
                    else
                        result = resultShoppingCart;                        

                    return Json(new
                        {
                        message = result.Message,
                        status = result.Status,
                        shopping_cart_list = result.ShoppingCartList,
                        authorize_net_list = result.AuthorizeNetList,
                        show_list = result.ShowList
                        }, JsonRequestBehavior.AllowGet);
                }
                else
                return Json(new {message = "Error no se pudo cargar los archivos!!",status = false}, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new {message = "Error no se pudo cargar los archivos!!",status = false}, JsonRequestBehavior.AllowGet);
            }
        }

        private Result ValidateAuthorizeNetFile(HttpPostedFileBase AuthorizeNetFile)
        {
            Result result = new Result();
            List<AuthorizeNet> AuthorizeNetList = AuthorizeNetReadFile(AuthorizeNetFile);
            List<AuthorizeNet> AuthorizeNetListIncorrect = AuthorizeNetList.Where(s => s.Status == false).ToList();
            result.AuthorizeNetList = AuthorizeNetList;
            result.ShowList = 2;

            if (AuthorizeNetListIncorrect.Count > 0)
            {
                result.Status = false;
                result.Message = "!!File AuthorizeNet Error!! " + AuthorizeNetListIncorrect.Count.ToString() + " rows.";
                foreach (AuthorizeNet item in AuthorizeNetListIncorrect)
                {
                    result.Message = result.Message + ";Row Incorrect: { " + item.Index.ToString() + " }";
                }
            }
            else
            {
                result.Status = true;
                result.Message = "!!File AuthorizeNet read!! " + AuthorizeNetList.Count.ToString() + " rows.";
            }
            return result;
        }

        private List<AuthorizeNet> AuthorizeNetReadFile(HttpPostedFileBase fileAuthorizeNet)
        {
            List<AuthorizeNet> AuthorizeNetList = new List<AuthorizeNet>();
            AuthorizeNet authorizeNet = new AuthorizeNet();
            string fileName = Path.GetFileName(fileAuthorizeNet.FileName);
            string folder = Server.MapPath("~/UploadedFiles");
            string path = Path.Combine(folder, fileName);

            if (System.IO.File.Exists(path))
            {
                int index = 0;
                string line;
                System.IO.StreamReader file = new System.IO.StreamReader(path);
                while ((line = file.ReadLine()) != null)
                {
                    if (index > 0)
                    {
                        authorizeNet = AuthorizeNetReadLine(line, index);
                        authorizeNet.Validate();
                        AuthorizeNetList.Add(authorizeNet);
                    }
                    index++;
                }
                file.Close();
            }
            return AuthorizeNetList;
        }

        private AuthorizeNet AuthorizeNetReadLine(string line, int index)
        {
            AuthorizeNet authorizeNet = new AuthorizeNet();
            authorizeNet.Index = index;
            authorizeNet.Status = false;
            try
            {
                line = line.Replace("\"", "");
                authorizeNet.Load_Fields(line);
                authorizeNet.Load_Transaction_Date(authorizeNet.Transaction_Date);
                authorizeNet.Load_Payment_Method(authorizeNet.Payment_Method);
                authorizeNet.Load_Credit_Card_Number(authorizeNet.Credit_Card_Number);
                authorizeNet.Load_Auth_Code(authorizeNet.Auth_Code);
                authorizeNet.Load_Transaction_ID(authorizeNet.Transaction_ID);
                return authorizeNet;
            }
            catch
            {
                authorizeNet.Error = "The line format incorrect.";
                return authorizeNet;
            }
        }

        private Result ValidateShoppingCartFile(HttpPostedFileBase ShoppingCartFile)
        {
            Result result = new Result();
            List<ShoppingCart> ShoppingCartList = ShoppingCartReadFile(ShoppingCartFile);
            List<ShoppingCart> ShoppingCartListIncorrect = ShoppingCartList.Where(s => s.Status == false).ToList();

            result.ShoppingCartList = ShoppingCartList;
            result.ShowList = 1;

            if (ShoppingCartListIncorrect.Count > 0)
            {
                result.Status = false;
                result.Message = "!!File ShoppingCart Error!! " + ShoppingCartListIncorrect.Count.ToString() + " rows.";
                foreach (ShoppingCart item in ShoppingCartListIncorrect)
                    result.Message = result.Message + ";Row Incorrect: { " + item.Index.ToString() + " }";
            }
            else
            {
                result.Status = true;
                result.Message = "!!File ShoppingCart read!! " + ShoppingCartList.Count.ToString() + " rows.";
            }
            
            return result;
        }

        protected bool UploadFileClient(HttpPostedFileBase file)
        {
            try
            {
                if (file.ContentLength > 0)
                {
                    string fileName = Path.GetFileName(file.FileName);
                    string folder = Server.MapPath("~/UploadedFiles");

                    if (!Directory.Exists(folder))
                        Directory.CreateDirectory(folder);

                    string path = Path.Combine(folder, fileName);

                    if (System.IO.File.Exists(path))
                        System.IO.File.Delete(path);

                    file.SaveAs(path);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        protected ShoppingCart ShoppingCartReadLine(string line, int index)
        {
            ShoppingCart shoppingCart = new ShoppingCart();
            shoppingCart.Index = index;
            shoppingCart.Status = false;
            try
            {
                char[] delimiters = new char[] { ';' };
                string[] values = line.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);

                if (values.Length != 4)
                {                    
                    shoppingCart.Error = "La cantidad de campos es diferente de cuatro.";
                    shoppingCart.Status = false;
                    return shoppingCart;
                }

                shoppingCart.Load_Transaction_Date(values[0]);
                shoppingCart.Load_Payment_Method(values[1]);
                shoppingCart.Load_Credit_Card_Number(values[1]);
                shoppingCart.Load_Auth_Code(values[2]);
                shoppingCart.Load_Transaction_ID(values[3]);
                return shoppingCart;
            }
            catch (Exception ex)
            {
                return shoppingCart;
            }
        }

        protected List<ShoppingCart> ShoppingCartReadFile(HttpPostedFileBase fileShoppingCart)
        {
            List<ShoppingCart> ShoppingCartList = new List<ShoppingCart>();
            ShoppingCart shoppingCart = new ShoppingCart();
            string fileName = Path.GetFileName(fileShoppingCart.FileName);
            string folder = Server.MapPath("~/UploadedFiles");
            string path = Path.Combine(folder, fileName);

            if (System.IO.File.Exists(path))
            {
                int index = 0;
                string line;
                System.IO.StreamReader file = new System.IO.StreamReader(path);
                while ((line = file.ReadLine()) != null)
                {
                    if(index > 0)
                    {
                        shoppingCart = ShoppingCartReadLine(line, index);
                        shoppingCart.Validate();
                        ShoppingCartList.Add(shoppingCart);
                    }
                    index++;
                }
                file.Close();
            }
            return ShoppingCartList;
        }

        protected Result Validate(List<ShoppingCart> ShoppingCartList , List<AuthorizeNet> AuthorizeNetList)
        {
            Result result = new Result();
            List<AuthorizeNet> ResultAuthorizeNet = new List<AuthorizeNet>();
            int ShoppingCartCount = ShoppingCartList.Count;
            ShoppingCart ShoppingCartItem = null;
            AuthorizeNet AuthorizeNetItem = null;

            for (int i = 0; i<= ShoppingCartCount - 1; i++)
            {
                ShoppingCartItem = ShoppingCartList[i];
                AuthorizeNetItem = AuthorizeNetList.Where(item => 
                        item.Key == ShoppingCartItem.Key && item.IndexShoppingCart == 0).FirstOrDefault();

                if(AuthorizeNetItem != null)
                {
                    AuthorizeNetItem.IndexShoppingCart = ShoppingCartItem.Index;
                    ShoppingCartItem.IndexAuthorizeNet = AuthorizeNetItem.Index;
                }
            }

            List<ShoppingCart> ShoppingCartListInvalidate = new List<ShoppingCart>();
            List<AuthorizeNet> AuthorizeNetListInvalidate = new List<AuthorizeNet>();

            ShoppingCartListInvalidate = ShoppingCartList.Where(item => item.IndexAuthorizeNet == 0).ToList();
            AuthorizeNetListInvalidate = AuthorizeNetList.Where(item => item.IndexShoppingCart == 0).ToList();

            result.ShoppingCartList = ShoppingCartList;
            result.AuthorizeNetList = AuthorizeNetList;

            if (ShoppingCartListInvalidate.Count > 0)
            {
                result.Message = "!!File ShoppingCart Not equivalent " + ShoppingCartListInvalidate.Count.ToString() + " Rows.!!";
                foreach (ShoppingCart item in ShoppingCartListInvalidate)
                    result.Message = result.Message + ";Row Incorrect: { " + item.Index.ToString() + " }";
                result.Status = false;
                result.ShowList = 1;
                return result;
            }

            if (AuthorizeNetListInvalidate.Count > 0)
            {
                result.Message = "!!File AuthorizeNet Not equivalent " + AuthorizeNetListInvalidate.Count.ToString() + " Rows.!!";
                foreach (AuthorizeNet item in AuthorizeNetListInvalidate)
                    result.Message = result.Message + ";Row Incorrect: { " + item.Index.ToString() + " }";
                result.Status = false;
                result.ShowList = 2;
                return result;
            }

            result.Message = "!!Files ShoppingCart and AuthorizeNet equivalents!!.Total " + AuthorizeNetList.Count.ToString() + " Rows";
            result.Status = true;
            result.ShowList = 2;
            return result;
        }
    }
}