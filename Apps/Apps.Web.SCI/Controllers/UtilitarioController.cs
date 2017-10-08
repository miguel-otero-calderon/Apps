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
        // GET: Utilitario
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
            bool status = false;
            Result ShoppingCartResult = null;
            try
            {
                if (UploadFileClient(file1) && UploadFileClient(file2))
                {
                    ShoppingCartResult = ValidateShoppingCartFile(ShoppingCartFile:file1);    
                    
                    if(ShoppingCartResult.Status)
                    {

                    }
                    else
                    {
                        return Json(new
                        {
                            message = "Archivos cargados correctamente!!",
                            message_file1 = ShoppingCartResult.MessageShoppingCart,
                            success = false
                        }, JsonRequestBehavior.AllowGet);
                    }

                    return Json(new
                    {
                        message = "Archivos cargados correctamente!!",
                        message_file1 = "",
                        success = status
                    }, JsonRequestBehavior.AllowGet);
                }
                return Json(new
                {
                    message = "Error no se pudo cargar los archivos!!",
                    success = false
                }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new
                {
                    message = "Error no se pudo cargar los archivos!!",
                    success = false
                }, JsonRequestBehavior.AllowGet);
            }
        }

        private Result ValidateShoppingCartFile(HttpPostedFileBase ShoppingCartFile)
        {
            Result result = new Result();
            List<ShoppingCart> ShoppingCartList = ShoppingCartReadFile(ShoppingCartFile);

            List<ShoppingCart> ShoppingCartListIncorrect = ShoppingCartList.Where(s => s.Status == false).ToList();

            if (ShoppingCartListIncorrect.Count > 0)
            {
                result.Status = false;
                result.MessageShoppingCart = "!!File ShoppingCart Error!! " + ShoppingCartListIncorrect.Count.ToString() + " rows.";
                foreach (ShoppingCart item in ShoppingCartListIncorrect)
                {
                    result.MessageShoppingCart = result.MessageShoppingCart + ";Row Incorrect: { " + item.Index.ToString() +" }" ;
                }
            }
            else
            {
                result.Status = true;
                result.MessageShoppingCart = "!!File ShoppingCart read!! " + ShoppingCartList.Count.ToString() + " rows.";
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
    }
}